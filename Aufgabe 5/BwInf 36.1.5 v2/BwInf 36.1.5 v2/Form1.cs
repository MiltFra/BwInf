using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BwInf_36._1._5_v2
{
    public partial class Form1 : Form
    {
        List<cPawn> pawns = new List<cPawn>();
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(48, 42, 34);
            this.pictureBox1.Image = blankBoard();
            this.Show();
            
        }
        private void Task1()
        {
            int totalMoveCount = 0;
            List<cGame> allPositions = new List<cGame>();
            List<cPawn> pawns = new List<cPawn>() { new cPawn(0, 0), new cPawn(1, 0), new cPawn(2, 0), new cPawn(3, 0), new cPawn(4, 0), new cPawn(5, 0), new cPawn(6, 0), new cPawn(7, 0) };
            Random rnd = new Random();
            cRook rook = new cRook(rnd.Next(0, 7), rnd.Next(1, 7));
            cGame game = new cGame(rook, pawns);
            game.whiteMoves = 1;
            render(game);
            bool caught = false;
            while (caught == false)
            {
                List<cMove> whiteMoves = game.getOptimalWhiteMoves();
                if (whiteMoves.Count() == 0)
                {
                    break;
                }
                foreach (cMove move in whiteMoves)
                {
                    foreach (cPawn pawn in game.whiteSide)
                    {
                        if (Math.Abs(pawn.x - game.blackSide.x) + Math.Abs(pawn.y - game.blackSide.y) == 1)
                        {
                            caught = true;
                            
                            cMove finalMove = new cMove(pawn.x, pawn.y, game.blackSide.x, game.blackSide.y);
                            allPositions.Add(game);
                            Console.WriteLine("Move: [" + finalMove.originX.ToString() + ";" + finalMove.originY.ToString() + "] >> [" + finalMove.targetX.ToString() + ";" + finalMove.targetY.ToString() + "]");
                            totalMoveCount++;
                            break;
                        }
                        if (pawn.x == move.originX && pawn.y == move.originY)
                        {
                            if (move.targetX < 8 && move.targetY < 8)
                            {
                                pawn.move(move);
                            }
                            else
                            {
                                throw new Exception();
                            }
                            allPositions.Add(game);
                            totalMoveCount++;
                            break;
                        }
                    }
                    if (caught)
                    {
                        break;
                    }
                }
            }
            if (caught)
            {
                Console.WriteLine("THE ROOK WAS CAUGHT!");
                foreach (cGame position in allPositions)
                {
                    render(position);
                    System.Threading.Thread.Sleep(2000);
                }
            }
            else
            {
                Console.WriteLine("The rook couln't be caught ...");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Task1();
        }
        private void render(List<cPawn> pawns, cFigure figure)
        {
            Bitmap total = blankBoard();
            foreach (cPawn pawn in pawns)
            {
                for (int i = 0; i < 8100; i++)
                {
                    total.SetPixel(pawn.x * 100 + i / 90 + 5, pawn.y * 100 + i % 90 + 5, Color.FromArgb(255, 255, 255));
                }
            }
            for (int i = 0; i < 8100; i++)
            {
                total.SetPixel(figure.x * 100 + i / 90 + 5, figure.y * 100 + i % 90 + 5, Color.FromArgb(0, 0, 0));
            }
            this.pictureBox1.Image = total;
            this.Show();
        }
        private void render(cGame game)
        {
            render(game.whiteSide, game.blackSide);
        }
        
        private Bitmap blankBoard()
        {
            Bitmap total = new Bitmap(800, 800);
            for (int i = 0; i < 64; i++)
            {
                int x = i / 8;
                int y = i % 8;
                for (int j = 0; j < 10000; j++)
                {
                    if ((x + y) % 2 == 1)
                    {
                        total.SetPixel(x * 100 + j / 100, y * 100 + j % 100, Color.FromArgb(135, 82, 44));
                    }
                    else
                    {
                        total.SetPixel(x * 100 + j / 100, y * 100 + j % 100, Color.FromArgb(170, 146, 88));
                    }
                }
            }
            return total;
        }
    }
}
