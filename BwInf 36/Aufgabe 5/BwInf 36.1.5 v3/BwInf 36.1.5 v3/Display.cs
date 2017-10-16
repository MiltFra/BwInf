using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BwInf
{
    public partial class Display : Form
    {
        public Display()
        {
            this.SetDesktopLocation(0, 0);
            InitializeComponent();
            
        }
        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
        }
        public bool stop = false;
        private void Display_Load(object sender, EventArgs e)
        {
            
        }
    }
}
