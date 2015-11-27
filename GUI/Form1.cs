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

namespace GUI
{
    public partial class Form1 : Form
    {
        public delegate void ImportFest();
        public delegate void OptimizeThread();
        private ServiceImportClient importClient;
        private ServiceRouteClient routeClient;

        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //ProgressBar
            //Maximum er mængden af routes der bliver importet
            //Step er hvor mange routes den skal gennemgå af gangen
            //progressBar1.Maximum = 1000000;
            //progressBar1.Step = 1;

            //Thread til at import
            Thread t = new Thread(new ThreadStart(ImportThreadStart));
            t.Start();                    
            
 
        }
        //Delegate til at sørge for at det hele bliver i en Thread
        public void ImportThreadStart()
        {
            ImportFest fest = new ImportFest(ImportStart);
            this.BeginInvoke(fest);

        }
        public void ImportStart()
        {
            WCFServer.Initialize();
            WCFServer.StartServer();
            
            importClient = new ServiceImportClient();
            routeClient = new ServiceRouteClient();

            importClient.Import();

            Route[] routes = routeClient.GetRoutes();


            foreach (Route route in routes)
            {
                //Tilføjer Rows 
                this.dataGridView1.Rows.Add(
                    route.DefaultRoute.ID.ToString(),
                    route.Stops.Count.ToString(),
                    string.Format("{0}/{1}", 
                        route.GetLoadForTrailer(),
                        route.DefaultRoute.TrailerType
                        )
                    );
            }

            importClient.Close();
            routeClient.Close();
            WCFServer.StopServer();


        }


        public void button2_Click(object sender, EventArgs e)
        {
            //Skifter til Optmizetab når der trykkes på optimize knappen
            tabControl1.SelectedTab = tabPage2;


            //Thread t = new Thread(new ThreadStart(OptimizeThreadStart));
            //t.Start();

        }

        ////Delegate til at sørge for at det hele bliver i en Thread
        //public void OptimizeThreadStart()
        //{
        //    OptimizeThread optimize = new OptimizeThread(OptimizeStart);
        //    this.BeginInvoke(optimize);

        //}

        //public void OptimizeStart()
        //{
        
        //    OptimizeController.Instance.Optimize();


        //    foreach (Route route in RouteController.Instance.Routes)
        //    {
        //        //Tilføjer Rows 
        //        this.dataGridView2.Rows.Add(
        //            route.DefaultRoute.ID.ToString(),
        //            route.Stops.Count.ToString(),
        //            string.Format("{0}/{1}",
        //                route.GetLoadForTrailer(),
        //                route.DefaultRoute.TrailerType),
        //            route.DateForDeparture.TimeOfDay.Hours.ToString(),
        //            route.DefaultRoute.ExtraRoute.ToString()
        //            );
                
        //    }


        //}

        public void button3_Click(object sender, EventArgs e)
        {
            
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

    }
}
