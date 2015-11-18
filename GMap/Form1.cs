using System;
using System.IO;
using System.Windows.Forms;
using Control;
using GMap.NET.WindowsForms;
using Server;

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
            geoPosion.Clear();
            //mapController.GenerateMap(gmap.MapProvider, geoPosion);
            gmap.Overlays.Add(geoPosion);
        }
    }
}
