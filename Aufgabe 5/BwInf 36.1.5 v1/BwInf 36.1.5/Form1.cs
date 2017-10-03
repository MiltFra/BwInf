using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BwInf_36._1._5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cGrid.grid = new int[8, 8]
            {
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 }
            };
            renderGrid();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt_setPawn_Click(object sender, EventArgs e)
        {
            cGrid.setPawn(Convert.ToInt32(tb_setPawn_x.Text), Convert.ToInt32(tb_setPawn_y.Text));
            tb_setPawn_x.Text = "";
            tb_setPawn_y.Text = "";
            renderGrid();
        }
        private void bt_setRook_Click(object sender, EventArgs e)
        {
            cGrid.setRook(Convert.ToInt32(tb_setRook_x.Text), Convert.ToInt32(tb_setRook_y.Text));
            tb_setRook_x.Text = "";
            tb_setRook_y.Text = "";
            renderGrid();
        }
        private void chaseRookV1 ()
        {
            cGrid.grid = new int[8, 8]
            {
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 }
            };
            renderGrid();
            while (cGrid.checkCollision() == false)
            {

            }

        }
        private void renderGrid()
        {
            pb_Grid.Image = cGrid.render();
        }
    }
}
