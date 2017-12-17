using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BwInf
{
    // Designed to find a Path from Start to Target while respecting all the obstacles
    public class Pathfinder
    {
        // Constructor

        public Pathfinder(int[,] values, Point start, Point target)
        {
            this.Values = (int[,])values.Clone();
            this.Start = start;
            this.Target = target;
            UpdateDistances();
        }

        // Properties

        // - Main Array, used to save the Distances of every single Spot to the Target
        private int[,] varDistances;
        public int[,] Distances
        {
            get { return varDistances; }
            set { varDistances = value; }
        }

        // - Stores the Values of all the Spots
        private int[,] varValues;
        public int[,] Values
        {
            get { return varValues; }
            set { varValues = value; }
        }

        // - Startpoint for the Path
        private Point varStart;
        public Point Start
        {
            get { return varStart; }
            set { varStart = value; }
        }

        // - Endpoint for the Path
        private Point varTarget;
        public Point Target
        {
            get { return varTarget; }
            set { varTarget = value; }
        }

        // Methods

        // - When Start Coordinates are equal to the Target Coordinates
        public bool StartEqualsTarget ()
        {
            return Start == Target;
        }

        // - Fills the Distance Array with all the right values
        private void UpdateDistances()
        {
            ClearDistances();

            FillDistances();
        }

        // - Applies the correct Value to all the Neighbours of a Spot that haven't already been set
        private bool UpdateNeighbours(Point spot, int value)
        {
            bool successful = false;

            if (spot.y < 7) { successful = UpdateSpot(new Point(spot.y + 1, spot.x), value + 1) || successful; }

            if (spot.y > 0) { successful = UpdateSpot(new Point(spot.y - 1, spot.x), value + 1) || successful; }

            if (spot.x < 7) { successful = UpdateSpot(new Point(spot.y, spot.x + 1), value + 1) || successful; }

            if (spot.x > 0) { successful = UpdateSpot(new Point(spot.y, spot.x - 1), value + 1) || successful; }

            return successful;
        }

        // - Returns true if a value could be applied to a Spot, because it wasn't already set
        private bool UpdateSpot(Point spot, int value)
        {
            if (this.Distances[spot.y, spot.x] == -2)
            {
                this.Distances[spot.y, spot.x] = value;
                return true;
            }
            return false;
        }

        // - Sets all the Distances to the base values (-1 / -2)
        private void ClearDistances()
        {
            this.Distances = new int[8, 8];
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (this.Values[y, x] == 0)
                {
                    this.Distances[y, x] = -2;
                }
                else
                {
                    this.Distances[y, x] = -1;
                }
            }
        }

        // - Updates all the possible Values to the actual Distance
        private void FillDistances()
        {
            if (!Target.IsOutsideGrid())
            {
                this.Distances[Target.y, Target.x] = 0;
                int currentValue = 0;
                List<Point> furthest = new List<Point>();
                furthest.Add(new Point(Target.y, Target.x));
                while (furthest.Count() != 0)
                {
                    bool successful = false;
                    foreach (Point f in furthest)
                    {
                        if (UpdateNeighbours(f, currentValue))
                        {
                            successful = true;
                        }
                    }
                    if (!successful)
                    {
                        break;
                    }
                    currentValue++;
                    furthest = new List<Point>();
                    for (int i = 0; i < 64; i++)
                    {
                        if (this.Distances[i / 8, i % 8] == currentValue)
                        {
                            furthest.Add(new Point(i / 8, i % 8));
                        }
                    }
                }
            }
        }

        // - Uses the Distance Array to find a path for the given Start and Target
        public List<Point> FindPath()
        {
            List<Point> path = new List<Point>();
            path.Add(this.Start);
            Point spot = BestNeighbour(this.Start);
            if (this.Distances[spot.y, spot.x] < 0)
            {
                return new List<Point>();
            }
            for (int i = this.Distances[spot.y, spot.x]; i > 0; i--)
            {
                path.Add(spot);
                foreach (Point n in Neighbours(spot))
                {
                    if (this.Distances[n.y, n.x] == i - 1)
                    {
                        spot = n;
                        break;
                    }
                }
            }
            path.Add(this.Target);
            return path;
        }

        // - Returns all the Neighbours of a spot (respecting the borders)
        private List<Point> Neighbours(Point spot)
        {
            List<Point> neighbours = new List<Point>();
            if (spot.y < 7)
            {
                neighbours.Add(new Point(spot.y + 1, spot.x));
            }
            if (spot.y > 0)
            {
                neighbours.Add(new Point(spot.y - 1, spot.x));
            }
            if (spot.x < 7)
            {
                neighbours.Add(new Point(spot.y, spot.x + 1));
            }
            if (spot.x > 0)
            {
                neighbours.Add(new Point(spot.y, spot.x - 1));
            }
            return neighbours;
        }

        // - Returns the Best Neighbour based on Distance from the Distance Array
        private Point BestNeighbour(Point spot)
        {
            List<Point> neighbours = Neighbours(spot);
            Point best = neighbours[0];
            foreach (Point n in neighbours)
            {
                if (IsCloserToTarget(n, best))
                {
                    best = n;
                }
            }
            return best;
        }

        // - When point1 is better than point2 
        private bool IsCloserToTarget(Point point1, Point point2)
        {
            return this.Distances[point1.y, point1.x] < this.Distances[point2.y, point2.x] && this.Distances[point1.y, point1.x] > 0;
        }
    }
}
