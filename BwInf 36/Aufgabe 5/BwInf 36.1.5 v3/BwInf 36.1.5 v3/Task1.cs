using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Task1 : Grid
    {
        // Constructor

        public Task1(Form1 activeForm, int pawnCount, int delay) : base(activeForm)
        {
            int[,] values = new int[8, 8];
            Random rnd = new Random();
            Point rook = new Point(rnd.Next(2, 7), rnd.Next(0, 7));
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (y == rook.y && x == rook.x)
                {
                    values[y, x] = 2;
                }
                else if (y == 0)
                {
                    values[y, x] = 1;
                }
                else
                {
                    values[y, x] = 0;
                }
            }
            this.Values = (int[,])values.Clone();
            this.delay = delay;
            NextMove();
        }

        // Properties

        public int delay = 0;
        public List<Move> Moves = new List<Move>();

        private string varNextTurn = "white";
        private string NextTurn
        {
            get { return varNextTurn; }
            set { varNextTurn = value; }
        }




        // Methods 

        private Move BestBlackMove()
        {
            List<Point> idealPositions = IdealBlackPositions();
            int best = 0;
            for (int i = 0; i < PossibleBlackMoves.Count(); i++)
            {
                if (PossibleBlackMoves[i].Target.y >= PossibleBlackMoves[best].Target.y)
                {
                    best = i;
                }
                foreach (Point id in idealPositions)
                {
                    if (PossibleBlackMoves[i].Target.y == id.y && PossibleBlackMoves[i].Target.x == id.x)
                    {
                        return PossibleBlackMoves[i];
                    }
                }
            }
            return PossibleBlackMoves[best];
        }


        private List<Point> IdealBlackPositions()
        {
            List<Point> total = new List<Point>();
            int[] emptyInX = new int[8];
            int[] emptyInY = new int[8];
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (this.Values[y, x] == 0)
                {
                    emptyInX[x]++;
                    emptyInY[y]++;
                }
            }
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (emptyInY[y] == 8 && emptyInX[x] == 8)
                {
                    total.Add(new Point(y, x));
                }

            }
            return total;
        }


        private Move BestWhiteMove()
        {
            int last = 0;
            List<Point> whitePositions = WhitePositions;
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                if (whitePositions[last].y > whitePositions[i].y)
                {
                    last = i;
                }
            }
            return new Move(whitePositions[last], new Point(whitePositions[last].y + 1, whitePositions[last].x));
        }


        private void NextMove()
        {
            if (NextTurn == "white")
            {
                NextTurn = "black";
                Console.WriteLine(this.Move(BestWhiteMove()).move);
            }
            else
            {
                NextTurn = "white";
                Console.WriteLine(this.Move(BestBlackMove()).move);
            }
            if (!this.GameOver)
            {
                System.Threading.Thread.Sleep(delay);
                NextMove();
            }
        }
    }
}
