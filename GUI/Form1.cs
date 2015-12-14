using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.IO;
using WCFService;
using Server;
using GUI.ServiceRoute;
using Control;
using GUI.ServiceOptimize;
using System.ServiceModel;
using GUI.ServiceExport;
using GUI.ServiceMap;
using GMap.NET.WindowsForms;
using GMap.NET;
using WCFWrapper;

namespace GUI
{
    public partial class Form1 : Form
    {
        public delegate void UpdateDelegate(List<Route> routes);    
        public delegate void OptimizeThreadDelegate();
        public delegate int GetProgressDelegate();
        public delegate string GetStatusDelegate();
        public delegate void UpdateLabelDelegate(string text);
        private ServiceMapClient mapClient;
        private ServiceOptimizeClient optimizeClient;
        private ServiceRouteClient routeClient;
        private ServiceExportClient exportClient;
        private GMapOverlay geoPosion;
        private List<Route> routes;
        private bool working;
        private bool error;

        public Form1()
        {
            InitializeComponent();
            
            //Map
            gMapControl1.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            
            //TODO: Look at more precise then denmark.
            gMapControl1.SetPositionByKeywords("Denmark");

            geoPosion = new GMapOverlay("GeoPosion");

            //Clients
            mapClient = new ServiceMapClient();
            routeClient = new ServiceRouteClient();
            optimizeClient = new ServiceOptimizeClient();
            exportClient = new ServiceExportClient();
            
            //Updates Labels
            error = false;
            label1.Text = "";

            //Starts Timer
            working = false;
            timer1.Interval = 100; //Miliseconds
            timer1.Start();

            //Progress Bar
            progressBar1.Maximum = 100;
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            //Changes tab page.
            tabControl1.SelectedTab = tabPage1;

            error = false;
            label1.Text = "Importing...";
            await Task.Run(() => Import());
            if (!error)
            {
                label1.Text = "Import Complete";
            }
        }

        public void Import()
        {
            try
            {
                routes = routeClient.GetRoutes().ToList<Route>();
                UpdateDelegate update = new UpdateDelegate(UpdateImportTable);
                this.BeginInvoke(update, routes);
            }
            catch (EndpointNotFoundException)
            {
                error = true;
                UpdateLabelDelegate updateLabel = new UpdateLabelDelegate(UpdateLabel);
                this.BeginInvoke(updateLabel, "Server is not Running.");
            }
        }

        public void UpdateImportTable(List<Route> routes)
        {
            //Clears old data.
            this.dataGridView1.Rows.Clear();
            
            //Adds new data.
            foreach (Route route in routes)
            {
                this.dataGridView1.Rows.Add(
                    route.DefaultRoute.ID.ToString(),
                    route.Stops.Count.ToString(),
                    string.Format("{0}/{1}",
                        route.GetLoadForTrailer(),
                        route.DefaultRoute.TrailerType
                        )
                    );
            }

            //Updates Map List
            listBox1.Items.Clear();
            foreach (Route route in routes)
            {
                long id = route.ID != 0 ? route.ID : route.DefaultRoute.ID;
                listBox1.Items.Add(id);
            }
        }


        public async void button2_Click(object sender, EventArgs e)
        {
            //Skifter til Optmizetab når der trykkes på optimize knappen
            tabControl1.SelectedTab = tabPage2;

            working = true;
            error = false;
            label1.Text = "Optimizing...";
            await Task.Run(() => Optimize());
            if (!error)
            {
                label1.Text = "Optimize Complete";
            }
            working = false;
        }

        public void Optimize()
        {
            try
            {
                optimizeClient.Optimize();
                routes = routeClient.GetRoutes().ToList<Route>();
                UpdateDelegate update = new UpdateDelegate(UpdateOptimizeTable);
                this.BeginInvoke(update, routes);
            }
            catch (FaultException<ExceptionNoRoutes>)
            {
                error = true;
                UpdateLabelDelegate updateLabel = new UpdateLabelDelegate(UpdateLabel);
                this.BeginInvoke(updateLabel, "No routes have been imported.");
            }
            catch (EndpointNotFoundException)
            {
                error = true;
                UpdateLabelDelegate updateLabel = new UpdateLabelDelegate(UpdateLabel);
                this.BeginInvoke(updateLabel, "Server is not Running.");
            }
        }

        public void UpdateLabel(string text)
        {
            label1.Text = text;
        }

        public void UpdateOptimizeTable(List<Route> routes)
        {
            //Clears old data.
            this.dataGridView2.Rows.Clear();

            //Adds new data.
            foreach (Route route in routes)
            {
                this.dataGridView2.Rows.Add(
                    route.DefaultRoute.ID.ToString(),
                    route.Stops.Count.ToString(),
                    string.Format("{0}/{1}",
                        route.GetLoadForTrailer(),
                        route.DefaultRoute.TrailerType),
                    route.DateForDeparture.TimeOfDay.Hours.ToString(),
                    route.DefaultRoute.ExtraRoute.ToString()
                    );
            }
        }

        public async void button3_Click(object sender, EventArgs e)
        {
            error = false;
            label1.Text = "Exporting...";
            await Task.Run(() => Export());
            if (!error)
            {
                label1.Text = "Export Complete";
            }

        }

        public void Export()
        {
            try
            {
                exportClient.Export();
            }
            catch (EndpointNotFoundException)
            {
                error = true;
                UpdateLabelDelegate updateLabel = new UpdateLabelDelegate(UpdateLabel);
                this.BeginInvoke(updateLabel, "Server is not Running.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            long id = (long)listBox1.SelectedItem;
            Route r = routes.First(route =>
            {
                if (route.ID == id || route.DefaultRoute.ID == id)
                {
                    return true;
                }
                return false;
            });

            gMapControl1.Overlays.Clear();

            MapRouteWrapper wrapper = mapClient.GetRoadMap(r);

            if (wrapper != null)
            {
                List<MapRoute> road = wrapper.Unwrap();

                geoPosion = new GMapOverlay("Route: " + id);

                for (int i = 0; i < road.Count; i++)
                {
                    GMapRoute gMapRoute = new GMapRoute(road[0].Points, (id + 1) + ". Stop");
                    geoPosion.Routes.Add(gMapRoute);
                    gMapControl1.UpdateRouteLocalPosition(gMapRoute);
                }
                gMapControl1.Overlays.Add(geoPosion);
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (working)
            {
                //Starts Tasks
                var t1 = Task.Run(() => GetProgress());
                var t2 = Task.Run(() => GetStatus());

                //Waits for them to finish.
                await t1;
                await t2;

                //Updates GUI
                progressBar1.Value = t1.Result;
                label1.Text = t2.Result;
            }
        }

        public int GetProgress()
        {
            int progress = 0;

            try
            {
                progress = optimizeClient.GetProgress();
            }
            catch (EndpointNotFoundException)
            {
                UpdateLabelDelegate updateLabel = new UpdateLabelDelegate(UpdateLabel);
                this.BeginInvoke(updateLabel, "Server is not Running.");
            }

            return progress;
        }

        public string GetStatus()
        {
            string status = "";
            
            try
            {
                status = optimizeClient.GetStatus();
            }
            catch (EndpointNotFoundException)
            {
                UpdateLabelDelegate updateLabel = new UpdateLabelDelegate(UpdateLabel);
                this.BeginInvoke(updateLabel, "Server is not Running.");
                status = "Server is not Running.";
            }

            return status;
        }

    }
}
