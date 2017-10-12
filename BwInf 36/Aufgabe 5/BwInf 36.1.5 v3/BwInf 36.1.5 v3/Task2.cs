using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Task2 : Grid
    {
        public Task2(Form1 activeForm, int pawnCount, int delay) : base(activeForm)
        {
            int[,] values = new int[8, 8];
            Random rnd = new Random();
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (pawnCount <= 0)
                {
                    values[y, x] = 0;
                }
                else if (y == 0)
                {
                    values[y, x] = 1;
                    pawnCount--;
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
        public List<Move> Moves = new List<Move>();
        private string NextTurn = "black";

        private (int y, int x) BlackPosition()
        {
            for (int i = 0; i < 64; i++)
            {
                if (this.Values[i / 8, i % 8] == 2)
                {
                    return (i / 8, i % 8);
                }
            }
            return (-1, -1);
        }
        private Move BestBlackMove()
        {
            List<(int y, int x)> idealPositions = IdealBlackPositions();
            (int y, int x) blackPosition = BlackPosition();
            List<Move> possibleMoves = PossibleBlackMoves();
            if (blackPosition.y < 0)
            {
                if (idealPositions.Count() == 0)
                {
                    Random rnd = new Random();
                    return new Move((-1, -1), (rnd.Next(2, 7), rnd.Next(0, 7)));
                }
                return new Move((-1, -1), idealPositions[0]);
            }
            foreach (Move move in possibleMoves)
            {
                foreach ((int y, int x) position in idealPositions)
                {
                    if (move.Target.y == position.y && move.Target.x == position.x)
                    {
                        return move;
                    }
                }
            }
            return possibleMoves[0];
        }
        private List<Move> PossibleBlackMoves()
        {
            (int y, int x) blackPosition = BlackPosition();
            List<Move> possibleMoves = new List<Move>();
            if (blackPosition.y < 0)
            {
                return possibleMoves;
            }
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
            for (int i = blackPosition.y; i > -1; i--)
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
                    possibleMoves.Add(new Move(blackPosition, (blackPosition.y, i)));
                }
            }
            for (int i = blackPosition.x; i > -1; i--)
            {
                if (this.Values[blackPosition.y, i] == 1)
                {
                    break;
                }
                else
                {
                    possibleMoves.Add(new Move(blackPosition, (blackPosition.y, i)));
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
                if (this.Values[y, x] != 1)
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
                    if (Values[y, Math.Min(x + 1, 7)] != 1 && Values[y, Math.Max(x - 1, 0)] != 1 && Values[Math.Min(y + 1, 7), x] != 1 && Values[Math.Max(y - 1, 0), x] != 1)
                    {
                        total.Add((y, x));
                    }
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
            List<(int y, int x)> idealPositions = IdealBlackPositions();
            List<Move> possibleMoves = PossibleWhiteMoves();
            (int y, int x) blackPosition = BlackPosition();
            foreach ((int y, int x) position in idealPositions)
            {
                if (position.y == blackPosition.y && position.x == blackPosition.x)
                {
                    foreach (Move move in possibleMoves)
                    {
                        if ((move.Target.y == position.y || move.Target.x == position.x) && this.Values[move.Target.y, move.Target.x] != 1)
                        {
                            return move;
                        }
                    }
                }
            }
            int last = -1;
            int first = -1;
            List<(int y, int x)> whitePositions = WhitePositions();
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                bool lastMovePossible = false;
                bool firstMovePossible = false;
                //checking wether i is candidate for first / last based on move possibilities
                foreach (Move move in possibleMoves)
                {
                    if (move == new Move(whitePositions[i], (whitePositions[i].y - 1, whitePositions[i].x)))
                    {
                        firstMovePossible = true;
                    }
                    if (move == new Move(whitePositions[i], (whitePositions[i].y + 1, whitePositions[i].x)))
                    {
                        lastMovePossible = true;
                    }
                    if (firstMovePossible && lastMovePossible)
                    {
                        break;
                    }
                }
                if (!lastMovePossible) { }
                else if (last < 0 || last > 7)
                {
                    last = i;
                }
                else if (whitePositions[last].y < whitePositions[i].y)
                {
                    last = i;
                }
                if (!firstMovePossible) { }
                else if (first < 0 || first > 7)
                {
                    last = i;
                }
                else if (whitePositions[first].y > whitePositions[i].y)
                {
                    first = i;
                }
            }
            // in front of all pawns
            if (first < 0 || first > 7) { }
            else if (whitePositions[first].y > blackPosition.y)
            {
                return new Move(whitePositions[first], (whitePositions[first].y - 1, whitePositions[first].x));
            }
            // after all pawns
            if (last < 0 || first > 7) { }
            else if (whitePositions[last].y < blackPosition.y)
            {
                return new Move(whitePositions[last], (whitePositions[last].y + 1, whitePositions[last].x));
            }
            // in between the pawns
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                Move currentCandidate = new Move(whitePositions[i], (whitePositions[i].y + 1 * Math.Sign(blackPosition.y - whitePositions[i].y), whitePositions[i].x));
                foreach(Move move in possibleMoves)
                {
                    if (move == currentCandidate)
                    {
                        return move;
                    }
                }
            }
            return possibleMoves[0];
        }
        private List<Move> PossibleWhiteMoves()
        {
            List<(int y, int x)> whitePositions = WhitePositions();
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                int y = whitePositions[i].y;
                int x = whitePositions[i].x;
                if (y == 0)
                {
                    possibleMoves.Add(new Move(whitePositions[i], (y + 1, x)));
                }
                else if (y == 7)
                {
                    possibleMoves.Add(new Move(whitePositions[i], (y - 1, x)));
                }
                else
                {
                    possibleMoves.Add(new Move(whitePositions[i], (y + 1, x)));
                    possibleMoves.Add(new Move(whitePositions[i], (y - 1, x)));
                }
                if (x == 0)
                {
                    possibleMoves.Add(new Move(whitePositions[i], (y, x + 1)));
                }
                else if (x == 7)
                {
                    possibleMoves.Add(new Move(whitePositions[i], (y, x - 1)));

                }
                else
                {
                    possibleMoves.Add(new Move(whitePositions[i], (y, x + 1)));
                    possibleMoves.Add(new Move(whitePositions[i], (y, x - 1)));
                }
            }
            return possibleMoves;
        }

        private int moves = 0;
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
                moves++;
                NextMove();
            }
        }
    }
}
