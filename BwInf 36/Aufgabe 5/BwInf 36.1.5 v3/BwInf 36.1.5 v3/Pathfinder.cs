using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BwInf
{
    public class Pathfinder
    {
        public Pathfinder(int[,] values,  Point start,  Point target)
        {
            this.Values = (int[,])values.Clone();
            this.Start = start;
            this.Target = target;
            UpdateDistances();
        }

        private int[,] varDistances;
        public int[,] Distances
        {
            get { return varDistances; }
            set { varDistances = value; }
        }

        private int[,] varValues;
        public int[,] Values
        {
            get { return varValues; }
            set { varValues = value; }
        }

        private  Point varStart;
        public  Point Start
        {
            get { return varStart; }
            set { varStart = value; }
        }

        private  Point varTarget;
        public  Point Target
        {
            get { return varTarget; }
            set { varTarget = value; }
        }

        private void UpdateDistances()
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
            this.Distances[Target.y, Target.x] = 0;
            int currentValue = 0;
            List< Point> furthest = new List< Point>();
            furthest.Add(new Point(Target.y, Target.x));
            while (furthest.Count() != 0)
            {
                bool successful = false;
                foreach ( Point f in furthest)
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
                furthest = new List< Point>();
                for (int i = 0; i < 64; i++)
                {
                    if (this.Distances[i / 8, i % 8] == currentValue)
                    {
                        furthest.Add(new Point(i / 8, i % 8));
                    }
                }               
            }
        }
        private bool UpdateNeighbours( Point spot, int value)
        {
            bool successful = false;
            if (spot.y < 7)
            {
                if (this.Distances[spot.y + 1, spot.x] == -2)
                {
                    this.Distances[spot.y + 1, spot.x] = value + 1;
                    successful = true;
                }
            }
            if (spot.y > 0)
            {
                if (this.Distances[spot.y - 1, spot.x] == -2)
                {
                    this.Distances[spot.y - 1, spot.x] = value + 1;
                    successful = true;
                }
            }
            if (spot.x < 7)
            {
                if (this.Distances[spot.y, spot.x + 1] == -2)
                {
                    successful = true;
                    this.Distances[spot.y, spot.x + 1] = value + 1;
                }
            }
            if (spot.x > 0)
            {
                if (this.Distances[spot.y, spot.x - 1] == -2)
                {
                    successful = true;
                    this.Distances[spot.y, spot.x - 1] = value + 1;
                }
            }
            return successful;
        }

        public List< Point> findPath()
        {
            List< Point> path = new List< Point>();
            path.Add(this.Start);
             Point spot = getBestNeighbour(this.Start);
            if (this.Distances[spot.y, spot.x] < 0)
            {
                return new List< Point>();
            }
            for (int i = this.Distances[spot.y, spot.x]; i > 0; i--)
            {
                path.Add(spot);
                foreach ( Point n in Neighbours(spot))
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
        private List< Point> Neighbours( Point spot)
        {
            List< Point> neighbours = new List< Point>();
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
        private  Point getBestNeighbour(Point spot)
        {
            List< Point> neighbours = Neighbours(spot);
             Point best = neighbours[0];
            foreach( Point n in neighbours)
            {
                if (this.Distances[n.y, n.x] < this.Distances[best.y, best.x] && this.Distances[n.y, n.x] > 0)
                {
                    best = n;
                }
            }
            return best;
        }
    }
}
