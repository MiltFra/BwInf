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
    public partial class Form1 : Form
    {
        private bool varRunning = false;
        private bool Running
        {
            get { return varRunning; }
            set
            {
                varRunning = value;
                bt_Task1.Enabled = !value;
                bt_Task2.Enabled = !value;
                bt_Task3.Enabled = !value;
                bt_Task4.Enabled = !value;
                bt_Test.Enabled = !value;
            }
        }



        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void bt_Task1_Click(object sender, EventArgs e)
        {
            Display display = new Display();
            display.Show();
            Running = true;
            Console.WriteLine("Starte Aufgabe 1 [8 Bauern, 1 Turm]");
            int delay = 0;
            try
            {
                delay = Convert.ToInt32(tb_delay.Text);
            }
            catch
            {
                delay = 10;
            }
            Task1 task = new Task1(display, 8, delay);
            display.Dispose();
            Running = false;
        }
        private void bt_Task2_Click(object sender, EventArgs e)
        {
            Display display = new Display();
            display.Show();
            Running = true;
            Console.WriteLine("Starte Aufgabe 2 [7 Bauern, 1 Turm]");
            int delay = 0;
            try
            {
                delay = Convert.ToInt32(tb_delay.Text);
            }
            catch
            {
                delay = 10;
            }
            Task2 task = new Task2(display, 7, delay);
            display.Dispose();
            Running = false;
        }

        private void bt_Stop_Click(object sender, EventArgs e)
        {
            //Display.stop = true;
        }

        private void bt_Task3_Click(object sender, EventArgs e)
        {
            Display display = new Display();
            display.Show();
            Running = true;
            Console.WriteLine("Starte Aufgabe 2 [7 Bauern, 1 Turm]");
            int delay = 0;
            try
            {
                delay = Convert.ToInt32(tb_delay.Text);
            }
            catch
            {
                delay = 10;
            }
            Task3 task = new Task3(display, 7, delay, 7, 2);
            Running = false;
        }

        private void bt_Test_Click(object sender, EventArgs e)
        {
            Pathfinder pf = new Pathfinder(
                new int[8, 8] {
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 } },
                (0, 0), (7, 7));
            List<(int y, int x)> path = pf.findPath();
        }
    }
}
