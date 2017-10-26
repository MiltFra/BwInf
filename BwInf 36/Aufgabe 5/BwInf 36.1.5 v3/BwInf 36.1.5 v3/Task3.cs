﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Task3 : Grid
    {
        public Task3(Display activeForm, int pawnCount, int delay, int k, int l) : base(activeForm)
        {
            int[,] values = new int[8, 8];
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (x % 2 == 0 + y % 2 && k > 0)
                {
                    values[y, x] = 1;
                    k--;
                }
                else
                {
                    values[y, x] = 0;
                }
            }
            this.Values = values;
            this.delay = delay;
            this.k = k;
            this.l = l;
            NextMove(0);
        }
        public int k { get; set; }
        public int l { get; set; }
        public int delay { get; set; }
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
            (int y, int x) blackPosition = BlackPosition();
            List<Move> possibleMoves = PossibleBlackMoves();
            List<(int y, int x)> idealPositions = IdealBlackPositions();
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
            List<(int y, int x)> halfIdealPositions = HalfIdealBlackPositions();
            foreach (Move move in possibleMoves)
            {
                foreach ((int y, int x) position in halfIdealPositions)
                {
                    if (move.Target.y == position.y && move.Target.x == position.x)
                    {
                        return move;
                    }
                }
            }
            (int index, int distance) best = (0, 0);
            List<(int y, int x)> whitePositions = WhitePositions();
            for (int i = 0; i < possibleMoves.Count(); i++)
            {
                int shortest = -1;
                foreach ((int y, int x) pos in whitePositions)
                {
                    int currentDist = this.distance(pos, possibleMoves[i].Target);
                    if (shortest < 0)
                    {
                        shortest = currentDist;
                    }
                    else if (currentDist < shortest)
                    {
                        shortest = currentDist;
                    }
                }
                if (shortest < 0)
                {
                    best = (i, shortest);
                }
                if (shortest > best.distance)
                {
                    best = (i, shortest);
                }
            }
            return possibleMoves[best.index];
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
        private List<(int y, int x)> HalfIdealBlackPositions()
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
            for (int i = 0; i < 8; i++)
            {
                if (emptyInX[i] == 8)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        total.Add((j, i));
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (emptyInY[i] == 8)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (emptyInX[j] < 8)
                        {
                            total.Add((j, i));
                        }
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
        private (int index, int distance) BiggestDistance(List<(int y, int x)> whitePositions, (int y, int x) blackPosition)
        {
            (int index, int distance) furthest = (-1, -1);
            for (int i = 0; i < whitePositions.Count(); i++)
            {
                int currentDistance = this.distance(blackPosition, whitePositions[i]);
                if (furthest.distance < currentDistance || furthest.index < 0)
                {
                    furthest = (i, currentDistance);
                }
            }
            return furthest;
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
                            if (stillPossible(move))
                            {
                                return move;
                            }
                        }
                    }
                }
            }
            List<(int y, int x)> whitePositions = WhitePositions();
            while (whitePositions.Count() > 0)
            {
                (int index, int distance) furthest = BiggestDistance(whitePositions, blackPosition);
                (bool possible, List<Move> steps) path = BestPath(whitePositions[furthest.index], blackPosition);
                if (path.possible && stillPossible(path.steps[0]))
                {
                    return path.steps[0];
                }
                whitePositions.RemoveAt(furthest.index);
            }
            return possibleMoves[0];

            //int first = -1;
            //for (int i = 0; i < whitePositions.Count(); i++)
            //{
            //    bool lastMovePossible = false;
            //    bool firstMovePossible = false;
            //    //checking wether i is candidate for first / last based on move possibilities
            //    foreach (Move move in possibleMoves)
            //    {
            //        if (move == new Move(whitePositions[i], (whitePositions[i].y - 1, whitePositions[i].x)))
            //        {
            //            firstMovePossible = true;
            //        }
            //        if (move == new Move(whitePositions[i], (whitePositions[i].y + 1, whitePositions[i].x)))
            //        {
            //            lastMovePossible = true;
            //        }
            //        if (firstMovePossible && lastMovePossible)
            //        {
            //            break;
            //        }
            //    }
            //    if (!lastMovePossible) { }
            //    else if (last < 0 || last > 7)
            //    {
            //        last = i;
            //    }
            //    else if (whitePositions[last].y < whitePositions[i].y)
            //    {
            //        last = i;
            //    }
            //    if (!firstMovePossible) { }
            //    else if (first < 0 || first > 7)
            //    {
            //        last = i;
            //    }
            //    else if (whitePositions[first].y > whitePositions[i].y)
            //    {
            //        first = i;
            //    }
            //}
            //// in front of all pawns
            //if (first < 0 || first > 7) { }
            //else if (whitePositions[first].y > blackPosition.y)
            //{
            //    return new Move(whitePositions[first], (whitePositions[first].y - 1, whitePositions[first].x));
            //}
            //// after all pawns
            //if (last < 0 || first > 7) { }
            //else if (whitePositions[last].y < blackPosition.y)
            //{
            //    return new Move(whitePositions[last], (whitePositions[last].y + 1, whitePositions[last].x));
            //}
            //// in between the pawns
            //for (int i = 0; i < whitePositions.Count(); i++)
            //{
            //    Move currentCandidate = new Move(whitePositions[i], (whitePositions[i].y + 1 * Math.Sign(blackPosition.y - whitePositions[i].y), whitePositions[i].x));
            //    foreach (Move move in possibleMoves)
            //    {
            //        if (move == currentCandidate)
            //        {
            //            return move;
            //        }
            //    }
            //}

            //int possible = 0;
            //for (int i = 0; i < possibleMoves.Count(); i++)
            //{
            //    if (stillPossible(possibleMoves[i]))
            //    {
            //        possible = i;
            //        break;
            //    }
            //}
            //return possibleMoves[possible];
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
        private bool stillPossible(Move move)
        {
            foreach ((int y, int x) m in this.moved)
            {
                if (move.Start.x == m.x && move.Start.y == m.y)
                {
                    return false;
                }
            }
            return true;
        }
        private (bool possible, List<Move> steps)[,] foundPaths { get; set; }
        private bool[,] inCheck { get; set; }
        private (bool possible, List<Move> steps) BestPath((int y, int x) start, (int y, int x) target)
        {
            if (start.y == target.y && start.x == target.x)
            {
                List<Move> single = new List<BwInf.Move>();
                single.Add(new BwInf.Move(start, target));
                return (true, single);
            }
            Pathfinder varPathfinder = new Pathfinder(this.Values, start, target);
            List<(int y, int x)> path = varPathfinder.findPath();
            if (path.Count() == 0)
            {
                return (false, new List<Move>());
            }
            List<Move> total = new List<Move>();
            for (int i = 0; i < path.Count() - 1; i++)
            {
                Move currentStep = new BwInf.Move(path[i], path[i + 1]);
                total.Add(currentStep);
            }
            return (true, total);
        }
        private (bool possible, List<Move> steps) findBestPath((int y, int x) start, (int y, int x) target)
        {
            this.setActive(target);
            inCheck[start.y, start.x] = true;
            try
            {
                if (foundPaths[start.y, start.x].steps.Count() != 0)
                {
                    return foundPaths[start.y, start.x];
                }
            }
            catch (ArgumentNullException) { }
            if (this.distance(start, target) < 2)
            {
                List<Move> steps = new List<BwInf.Move>();
                steps.Add(new BwInf.Move(start, target));
                foundPaths[start.y, start.x] = (true, steps);
                return (true, steps);
            }

            (bool possible, List<Move> steps) shortest = (false, new List<Move>());
            if (start.y + 1 <= 7)
            {
                if (!inCheck[start.y + 1, start.x])
                {
                    (bool possible, List<Move> steps) current = findBestPath((start.y + 1, start.x), target);
                    Move oneStep = new BwInf.Move(start, (start.y + 1, start.x));
                    if ((current.possible && current.steps.Count() < shortest.steps.Count()) || !shortest.possible)
                    {
                        shortest = (true, new List<Move>());
                        shortest.steps.Add(oneStep);
                        shortest.steps.AddRange(current.steps);
                    }
                }
            }
            if (start.y - 1 >= 0)
            {
                if (!inCheck[start.y - 1, start.x])
                {
                    (bool possible, List<Move> steps) current = findBestPath((start.y - 1, start.x), target);
                    Move oneStep = new BwInf.Move(start, (start.y - 1, start.x));
                    if ((current.possible && current.steps.Count() < shortest.steps.Count()) || !shortest.possible)
                    {
                        shortest = (true, new List<Move>());
                        shortest.steps.Add(oneStep);
                        shortest.steps.AddRange(current.steps);
                    }
                }
            }
            if (start.x + 1 <= 7)
            {
                if (!inCheck[start.y, start.x + 1])
                {
                    (bool possible, List<Move> steps) current = findBestPath((start.y, start.x + 1), target);
                    Move oneStep = new BwInf.Move(start, (start.y, start.x + 1));
                    if ((current.possible && current.steps.Count() < shortest.steps.Count()) || !shortest.possible)
                    {
                        shortest = (true, new List<Move>());
                        shortest.steps.Add(oneStep);
                        shortest.steps.AddRange(current.steps);
                    }
                }
            }
            if (start.x - 1 >= 0)
            {
                if (!inCheck[start.y, start.x - 1])
                {
                    (bool possible, List<Move> steps) current = findBestPath((start.y, start.x - 1), target);
                    Move oneStep = new BwInf.Move(start, (start.y, start.x - 1));
                    if ((current.possible && current.steps.Count() < shortest.steps.Count()) || !shortest.possible)
                    {
                        shortest = (true, new List<Move>());
                        shortest.steps.Add(oneStep);
                        shortest.steps.AddRange(current.steps);
                    }
                }
            }
            foundPaths[start.y, start.x] = shortest;
            inCheck[start.y, start.x] = false;
            this.setActive((-1, -1));
            return shortest;

        }
        private int moves = 0;
        public List<(int y, int x)> moved = new List<(int y, int x)>();
        private void NextMove(int l)
        {
            if (l > 0)
            {
                l--;
                Move bestWhiteMove = BestWhiteMove();
                Console.WriteLine(this.Move(bestWhiteMove).move);
                moved.Add(bestWhiteMove.Target);
            }
            else
            {
                l = this.l;
                Console.WriteLine(this.Move(BestBlackMove()).move);
                moved = new List<(int y, int x)>();
            }
            if (!this.GameOver)
            {
                System.Threading.Thread.Sleep(delay);
                moves++;
                NextMove(l);
            }
        }
    }
}