using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control; 

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //ProgressBar
            //Maximum er mængden af routes der bliver importet
            //Step er hvor mange routes den skal gennemgå af gangen
            progressBar1.Maximum = 1000000;
            progressBar1.Step = 1;

            //Import
            ImportController.Instance.ImportRoutes();        
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

    }
}
