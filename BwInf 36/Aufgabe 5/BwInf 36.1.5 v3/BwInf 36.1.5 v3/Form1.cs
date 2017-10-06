using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BwInf_36._1._5_v3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Grid grid { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
                        
        }
        private void bt_Task1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starte Aufgabe 1 [8 Bauern, 1 Turm]");
            Task1 task = new Task1(this, 8, 50);
        }
    }
    
}
