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
using Control;
using Server;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            //ProgressBar
            //Maximum er mængden af routes der bliver importet
            //Step er hvor mange routes den skal gennemgå af gangen
            //progressBar1.Maximum = 1000000;
            //progressBar1.Step = 1;

            //Import
              
            //dataGridView1.Columns.Add(new DataGridViewColumn);

            Thread t = new Thread(ImportController.Instance.ImportRoutes);
            t.Start();
            
            
            
            
 
        }

        public void button2_Click(object sender, EventArgs e)
        {
            OptimizeController.Instance.Optimize();
        }

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

    }
}
