using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Task1 : Grid
    {
        public Task1(Display activeForm, int pawnCount, int delay) : base(activeForm)
        {
            int[,] values = new int[8, 8];
            Random rnd = new Random();
            (int y, int x) rook = (rnd.Next(2, 7), rnd.Next(0, 7));
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

        public int delay = 0;
        public List<Move> Moves = new List< Move>();
        private string NextTurn = "white";

        private (int y, int x) BlackPosition()
        {
            for (int i = 0; i < 64; i++)
            {
                if (this.Values[i / 8, i % 8] == 2)
                {
                    return (i / 8, i % 8);
                }
            }
            throw new Exception();
        }
        private Move BestBlackMove()
        {
            List<(int y, int x)> idealPositions = IdealBlackPositions();
            (int y, int x) blackPosition = BlackPosition();
            List<Move> possibleMoves = PossibleBlackMoves();
            int best = 0;
            for (int i = 0; i < possibleMoves.Count(); i++)
            {
                if (possibleMoves[i].Target.y >= possibleMoves[best].Target.y)
                {
                    best = i;
                }
                foreach ((int y, int x) id in idealPositions)
                {
                    if (possibleMoves[i].Target.y == id.y && possibleMoves[i].Target.x == id.x)
                    {
                        return possibleMoves[i];
                    }
                }
            }
            return possibleMoves[best];
        }        
        private List<Move> PossibleBlackMoves()
        {
            (int y, int x) blackPosition = BlackPosition();
            List<Move> possibleMoves = new List<Move>();
            for (int i = blackPosition.y; i < 8; i++)
            {
                if (this.Values[i, blackPosition.x] == 1)
                {
                    break;
                }
                else
                {
                    possibleMoves.Add(new Move(blackPosition, (i, blackPosition.x)));
                }
            }
            for (int i = blackPosition.y; i > 8; i--)
            {
                if (this.Values[i, blackPosition.x] == 1)
                {
                    break;
                }
                else
                {
                    possibleMoves.Add(new Move(blackPosition, (i, blackPosition.x)));
                }
            }
            for (int i = blackPosition.x; i < 8; i++)
            {
                if (this.Values[blackPosition.y, i] == 1)
                {
                    break;
                }
                else
                {
                    possibleMoves.Add(new  Move(blackPosition, (blackPosition.y, i)));
                }
            }
            for (int i = blackPosition.x; i > 8; i--)
            {
                if (this.Values[blackPosition.y, i] == 1)
                {
                    break;
                }
                else
                {
                    possibleMoves.Add(new  Move(blackPosition, (blackPosition.y, i)));
                }
            }
            return possibleMoves;
        }
        private List<(int y, int x)> IdealBlackPositions()
        {
            List<(int y, int x)> total = new List<(int y, int x)>();
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
                    total.Add((y, x));
                }

            }
            return total;
        }

        private List<(int y, int x)> WhitePositions()
        {
            List<(int y, int x)> total = new List<(int y, int x)>();
            for (int i = 0; i < 64; i++)
            {
                if (this.Values[i / 8, i % 8] == 1)
                {
                    total.Add((i / 8, i % 8));
                }
            }
            return total;
        }
        private Move BestWhiteMove()
        {
            int last = 0;
            List<(int y, int x)> whitePositions = WhitePositions();
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                if (whitePositions[last].y > whitePositions[i].y)
                {
                    last = i;
                }
            }
            return new Move(whitePositions[last], (whitePositions[last].y + 1, whitePositions[last].x));
        }
        private List<Move> PossibleWhiteMove()
        {
            List<(int y, int x)> whitePositions = WhitePositions();
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (y == 0)
                {
                    possibleMoves.Add(new  Move(whitePositions[i], (y + 1, x)));
                }
                else if (y == 7)
                {
                    possibleMoves.Add(new  Move(whitePositions[i], (y - 1, x)));
                }
                else
                {
                    possibleMoves.Add(new  Move(whitePositions[i], (y + 1, x)));
                    possibleMoves.Add(new  Move(whitePositions[i], (y - 1, x)));
                }
                if (x == 0)
                {
                    possibleMoves.Add(new  Move(whitePositions[i], (y, x + 1)));
                }
                else if (x == 7)
                {
                    possibleMoves.Add(new  Move(whitePositions[i], (y, x - 1)));

                }
                else
                {
                    possibleMoves.Add(new  Move(whitePositions[i], (y, x + 1)));
                    possibleMoves.Add(new  Move(whitePositions[i], (y, x - 1)));
                }
            }
            return possibleMoves;
        }

        private void NextMove()
        {
            if (!this.ActiveForm.stop)
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
            else
            {
                this.ActiveForm.stop = false;
            }
        }
    }
}
