using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Control;
using Database;
using GMap.NET;
using GMap.NET.WindowsForms;
using Model;

namespace GMap
{
    public partial class Form1 : Form
    {
        DBConnection dbConnection = DBConnection.Instance;
        MapController mapController = MapController.Instance;
        GMapOverlay geoPosion = new GMapOverlay("GeoPosion");
        public Form1()
        {
            InitializeComponent();

            dbConnection.Host = "localhost";
            dbConnection.DB = "TestArla";
            dbConnection.User = File.ReadAllText("Config/user.txt");
            dbConnection.Pass = File.ReadAllText("Config/pass.txt");
            dbConnection.Connect();


            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("Denmark");

            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528), GMarkerGoogleType.green);
            //markersOverlay.Markers.Add(marker);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Dictionary<Route, String> test = new Dictionary<Route, string>();
            //Route route = new Route(new DefaultRoute(31, 51, false), DateTime.Today);
            //test[route] = "Det virker";

            //route.ID = 1337;

            ////label1.Text = test[route];

            //geoPosion.Clear();
            ////mapController.GenerateMap(gmap.MapProvider, geoPosion);
            //gmap.Overlays.Add(geoPosion);

            ImportController.Instance.ImportRoutes();
            listBox1.Items.Clear();
            foreach (Route route in RouteController.Instance.Routes.ToList())
            {
                long id = route.ID == null ? route.ID : route.DefaultRoute.ID;
                listBox1.Items.Add(id);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            long id = (long) listBox1.SelectedItem;

            label1.Text = id.ToString();

            Route r = RouteController.Instance.Routes.First(route =>
            {
                if (route.ID == id || route.DefaultRoute.ID == id)
                {
                    return true;
                }
                return false;
            });

            gmap.Overlays.Clear();
            geoPosion = new GMapOverlay("Route: " + id);
            List<MapRoute> roud = MapController.CalcRoute(r);
            for (int i = 0; i < roud.Count; i++)
            {
                GMapRoute gMapRoute = new GMapRoute(roud[0].Points, (id + 1) + ". Stop");
                geoPosion.Routes.Add(gMapRoute);
                gmap.UpdateRouteLocalPosition(gMapRoute);
            }
            gmap.Overlays.Add(geoPosion);

            gmap.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
