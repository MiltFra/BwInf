using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Task3 : Grid
    {
        // Constructor

        public Task3(Form1 activeForm, int pawnCount, int delay, int k, int l) : base(activeForm)
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
            this.Delay = delay;
            this.K = k;
            this.L = l;
            this.Values = values;
            NextMove(0);
        }

        // Properties

        // - Count of Pawns
        public int K { get; set; }

        // - Count of Moves of Pawns
        public int L { get; set; }

        // - The Delay between the Moves
        public int Delay { get; set; }

        // - Updated every time a new Turn starts
        private int varTurns = 0;
        public int Turns
        {
            get { return varTurns; }
            set { varTurns = value; }
        }

        // - The best Move for the Enemy
        private Move varBestBlackMove;
        public Move BestBlackMove
        {
            get { return varBestBlackMove; }
            set { varBestBlackMove = value; }
        }

        // - The best Move for the White Figures
        private Move varBestWhiteMove;
        public Move BestWhiteMove
        {
            get { return varBestWhiteMove; }
            set { varBestWhiteMove = value; }
        }


        // - Stores all the Moves used 'til the Game is Over
        public List<Move> Moves = new List<Move>();

        // - Stores all the Positions in the Grid that aren't available for Move anymore, resets every Turn
        private List<Point> MovedThisTurn = new List<Point>();

        // Methods

        // - Updates the current Properties
        protected override void Update()
        {
            base.Update();
            UpdateBestBlackMove();
            UpdateBestWhiteMove();
        }

        // - Finds the best Black Move
        private bool UpdateBestBlackMove()
        {
            if (CheckFirstMove()) { return true; }

            if (CheckIdealPosition()) { return true; }

            if (CheckHalfIdealPositions()) { return true; }

            if (CheckMostDistantMove()) { return true; }

            throw new Exception();
        }

        // - Is true if a First Move could be applied
        private bool CheckFirstMove()
        {
            if (BlackPosition.y < 0)
            {
                if (IdealPositions.Count() == 0)
                {
                    Random rnd = new Random();
                    BestBlackMove = new Move(new Point(-1, -1), new Point(rnd.Next(2, 7), rnd.Next(0, 7)));
                }
                else
                {
                    BestBlackMove = new Move(new Point(-1, -1), IdealPositions[0]);
                }
                return true;
            }
            return false;
        }

        // - Is true if an Ideal Position could be reached
        private bool CheckIdealPosition()
        {
            foreach (Move move in PossibleBlackMoves)
            {
                foreach (Point position in IdealPositions)
                {
                    if (move.Target.y == position.y && move.Target.x == position.x)
                    {
                        BestBlackMove = move;
                        return true;
                    }
                }
            }
            return false;
        }

        // - Is true if a Half Ideal Position could be reached
        private bool CheckHalfIdealPositions()
        {
            foreach (Move move in PossibleBlackMoves)
            {
                foreach (Point position in HalfIdealPositions)
                {
                    if (move.Target.y == position.y && move.Target.x == position.x)
                    {
                        BestBlackMove = move;
                        return true;
                    }
                }
            }
            return false;
        }

        // - Is true if the Move with the longest minimal Distance could be applied
        private bool CheckMostDistantMove()
        {
            (int index, int distance) best = (0, 0);
            for (int i = 0; i < PossibleBlackMoves.Count(); i++)
            {
                int shortest = -1;

                foreach (Point pos in WhitePositions)
                {
                    int currentDist = this.Distance(pos, PossibleBlackMoves[i].Target);
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
            if (best.index < PossibleBlackMoves.Count())
            {
                BestBlackMove = PossibleBlackMoves[best.index];
                return true;
            };
            return false;
        }

        // - Finds the best White Move
        private bool UpdateBestWhiteMove()
        {
            if (BlackPosition.IsOutsideGrid()) { return false; }

            if (GameEndingMovePossible()) { return true; }

            if (BlockCurrentIdealPossible()) { return true; }

            if (FurthestStepPossible()) { return true; }

            return false;
        }

        // - Returns true if a Move could be set that ends the game
        private bool GameEndingMovePossible()
        {
            foreach (Move move in PossibleWhiteMoves)
            {
                if (CanEnd(move) && stillPossible(move))
                {
                    BestWhiteMove = move;
                    return true;
                }
            }

            return false;
        }

        // - Returns true if a Move could be set that Blocks the Current Position in case it's Ideal
        private bool BlockCurrentIdealPossible()
        {
            if (BlackHasIdealPosition())
            {
                List<Move> candidates = CandidatesToBlockIdeal();
                if (candidates.Count() > 0)
                {
                    (int[] y, int[] x) canBlock = CanBlock();
                    Move best = candidates[0];
                    foreach (Move move in candidates) { best = BetterCandidateToBlockIdeal(best, move, canBlock); }
                    BestWhiteMove = best;
                    return true;
                }
            }
            return false;
        }

        // - Returns true if a Move could be set that is a First Step of a Path from a White Position to the Enemy
        private bool FurthestStepPossible()
        {
            List<Move> firstSteps = FirstSteps();
            int before = EmptyRowCount();
            foreach (Move m in firstSteps)
            {
                int[,] valuesAfter = (int[,])this.Values.Clone();
                valuesAfter[m.Start.y, m.Start.x] = 0;
                valuesAfter[m.Target.y, m.Target.x] = 1;
                int after = EmptyRowCount(valuesAfter);
                if (after > before)
                {
                    BestWhiteMove = m;
                    return true;
                }
            }
            if (firstSteps.Count() > 0)
            {
                BestWhiteMove = firstSteps[0];
                return true;
            }
            return false;
        }

        // - Returns all the Possible Moves that end either on the Y or the X Coordinate of the Black Position
        private List<Move> CandidatesToBlockIdeal()
        {
            List<Move> candidates = new List<Move>();
            foreach (Move move in PossibleWhiteMoves)
            {
                if ((move.Target.y == BlackPosition.y || move.Target.x == BlackPosition.x) && this.Values[move.Target.y, move.Target.x] != 1)
                {
                    if (stillPossible(move))
                    {
                        candidates.Add(move);
                    }
                }
            }
            return candidates;
        }

        // - Returns the better of the two given Moves
        private Move BetterCandidateToBlockIdeal(Move move1, Move move2, (int[] y, int[] x) canBlock)
        {
            if (move2.Details.direction == "horizontal")
            {
                if (canBlock.x[move2.Target.x] < canBlock.x[move1.Target.x])
                {
                    return move2;
                }
            }
            else if (move2.Details.direction == "vertical")
            {
                if (canBlock.y[move2.Target.y] < canBlock.y[move1.Target.y])
                {
                    return move2;
                }
            }
            return move1;
        }

        // - Returns all the FirstSteps of the Paths for every WhitePosition
        private List<Move> FirstSteps()
        {
            List<Move> firstSteps = new List<Move>();
            List<Point> tempPositions = WhitePositions;
            while (tempPositions.Count() > 0)
            {
                (int index, int distance) furthest = BiggestDistance();
                (bool possible, List<Move> steps) path = BestPath(WhitePositions[furthest.index], BlackPosition);
                if (path.possible && stillPossible(path.steps[0]))
                {
                    firstSteps.Add(path.steps[0]);
                }
                tempPositions.RemoveAt(furthest.index);
            }
            return firstSteps;
        }

        private (int index, int distance) BiggestDistance()
        {
            (int index, int distance) furthest = (-1, -1);
            for (int i = 0; i < WhitePositions.Count(); i++)
            {
                int currentDistance = this.Distance(BlackPosition, WhitePositions[i]);
                if (furthest.distance < currentDistance || furthest.index < 0)
                {
                    furthest = (i, currentDistance);
                }
            }
            return furthest;
        }

        private int EmptyRowCount()
        {
            (int[] y, int[] x) empty = Empty();
            int total = 0;
            for (int i = 0; i < 8; i++)
            {
                if (empty.y[i] == 0) { total++; }
            }
            return total;
        }
        private int EmptyRowCount(int[,] values)
        {
            (int[] y, int[] x) empty = Empty(values);
            int total = 0;
            for (int i = 0; i < 8; i++)
            {
                if (empty.y[i] == 0) { total++; }
            }
            return total;
        }

        // - Calculates how many Pawns are in Position to Block a certrain row
        private (int[] y, int[] x) CanBlock()
        {
            (int[] y, int[] x) total = (new int[8], new int[8]);
            foreach (Point position in WhitePositions)
            {
                try { total.y[position.y + 1]++; } catch (Exception) { }
                try { total.y[position.y - 1]++; } catch (Exception) { }
                try { total.x[position.x + 1]++; } catch (Exception) { }
                try { total.x[position.x - 1]++; } catch (Exception) { }
            }
            return total;
        }

        // - Returns the best Path from a Point to another using the Pathfinder class
        private (bool possible, List<Move> steps) BestPath(Point start, Point target)
        {
            if (start.y == target.y && start.x == target.x)
            {
                List<Move> single = new List<Move>();
                single.Add(new Move(start, target));
                return (true, single);
            }
            Pathfinder varPathfinder = new Pathfinder(this.Values, start, target);
            List<Point> path = varPathfinder.FindPath();
            if (path.Count() == 0)
            {
                return (false, new List<Move>());
            }
            List<Move> total = new List<Move>();
            for (int i = 0; i < path.Count() - 1; i++)
            {
                Move currentStep = new Move(path[i], path[i + 1]);
                total.Add(currentStep);
            }
            return (true, total);
        }

        // - Calculates the Next Move
        private void NextMove(int l)
        {
            if (l > 0)
            {
                l--;
                MovedThisTurn.Add(BestWhiteMove.Target);
                Console.WriteLine(this.Move(BestWhiteMove).move);                
            }
            else
            {
                l = this.L;
                Console.WriteLine(this.Move(BestBlackMove).move);
                MovedThisTurn = new List<Point>();
                Turns++;
            }

            if (!this.GameOver)
            {
                System.Threading.Thread.Sleep(Delay);
                NextMove(l);
            }
        }

        // - When a PawnMove reaches the Black Figure
        private bool CanEnd(Move move)
        {
            return move.Target.y == BlackPosition.y && move.Target.x == BlackPosition.x;
        }

        // - As long as the particular Figure hasn't been moved this Turn
        private bool stillPossible(Move move)
        {
            foreach (Point m in this.MovedThisTurn)
            {
                if (move.Start.Equals(m))
                {
                    return false;
                }
            }
            return true;
        }

        // - True if current Black Position is identical to an Ideal Position
        private bool BlackHasIdealPosition()
        {
            foreach (Point position in IdealPositions)
            {
                if (position.Equals(BlackPosition)) return true;
            }
            return false;
        }

    }
}
