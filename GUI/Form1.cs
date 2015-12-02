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
using GUI.ServiceImport;
using GUI.ServiceRoute;
using Control;
using GUI.ServiceOptimize;
using System.ServiceModel;
using GUI.ServiceExport;

namespace GUI
{
    public partial class Form1 : Form
    {
        public delegate void UpdateDelegate(List<Route> routes);    
        public delegate void OptimizeThreadDelegate();
        public delegate int GetProgressDelegate();
        public delegate string GetStatusDelegate();
        private ServiceImportClient importClient;
        private ServiceOptimizeClient optimizeClient;
        private ServiceRouteClient routeClient;
        private ServiceExportClient exportClient;
        private bool working;

        public Form1()
        {
            InitializeComponent();
            
            //Starts the Server. Methods for testing.
            //TODO: Implement seperate server from client.
            WCFServer.Initialize();
            WCFServer.StartServer();
            importClient = new ServiceImportClient();
            routeClient = new ServiceRouteClient();
            optimizeClient = new ServiceOptimizeClient();
            exportClient = new ServiceExportClient();
            
            //Updates Labels
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
            //ProgressBar
            //Maximum er mængden af routes der bliver importet
            //Step er hvor mange routes den skal gennemgå af gangen
            //progressBar1.Maximum = 100;
            //progressBar1.Step = 1;

            //Changes tab page.
            tabControl1.SelectedTab = tabPage1;

            //Label = "Working...";
            label1.Text = "Importing...";
            await Task.Run(() => Import());
            label1.Text = "Import Complete";
            //label = "Done";


            ////Thread til at import
            //Thread t = new Thread(new ThreadStart(ImportThreadStart));
            //t.Start();                    
            
 
        }


        public void Import()
        {
            importClient.ImportFromArla();
            List<Route> routes = routeClient.GetRoutes().ToList<Route>();
            UpdateDelegate update = new UpdateDelegate(UpdateImportTable);
            this.BeginInvoke(update, routes);
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
        }


        public async void button2_Click(object sender, EventArgs e)
        {
            //Skifter til Optmizetab når der trykkes på optimize knappen
            tabControl1.SelectedTab = tabPage2;

            working = true;
            label1.Text = "Optimizing...";
            await Task.Run(() => Optimize());
            label1.Text = "Optimize Complete";
            working = false;
        }

        public void Optimize()
        {
            try
            {
                optimizeClient.Optimize();
                List<Route> routes = routeClient.GetRoutes().ToList<Route>();
                UpdateDelegate update = new UpdateDelegate(UpdateOptimizeTable);
                this.BeginInvoke(update, routes);
            }
            catch (FaultException<ExceptionNoRoutes>)
            {
                label1.Text = "No routes have been imported.";   
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            //EXPORT
            label1.Text = "Exporting...";
            await Task.Run(() => Export());
            label1.Text = "Export Complete";

        }

        public void Export()
        {
            exportClient.Export();
        }

        public void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        public void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public void tabPage3_Click(object sender, EventArgs e)
        {

        }

        public void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            return optimizeClient.GetProgress();
        }

        public string GetStatus()
        {
            return optimizeClient.GetStatus();
        }

    }
}
