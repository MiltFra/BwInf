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
        public Pathfinder(int[,] values, (int y, int x) start, (int y, int x) target)
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

        private (int y, int x) varStart;
        public (int y, int x) Start
        {
            get { return varStart; }
            set { varStart = value; }
        }

        private (int y, int x) varTarget;
        public (int y, int x) Target
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
            List<(int y, int x)> furthest = new List<(int y, int x)>();
            furthest.Add((Target.y, Target.x));
            while (furthest.Count() != 0)
            {
                bool successful = false;
                foreach ((int y, int x) f in furthest)
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
                furthest = new List<(int y, int x)>();
                for (int i = 0; i < 64; i++)
                {
                    if (this.Distances[i / 8, i % 8] == currentValue)
                    {
                        furthest.Add((i / 8, i % 8));
                    }
                }               
            }
        }
        private bool UpdateNeighbours((int y, int x) spot, int value)
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

        public List<(int y, int x)> findPath()
        {
            List<(int y, int x)> path = new List<(int y, int x)>();
            path.Add(this.Start);
            (int y, int x) spot = getBestNeighbour(this.Start);
            if (this.Distances[spot.y, spot.x] < 0)
            {
                return new List<(int y, int x)>();
            }
            for (int i = this.Distances[spot.y, spot.x]; i > 0; i--)
            {
                path.Add(spot);
                foreach ((int y, int x) n in Neighbours(spot))
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
        private List<(int y, int x)> Neighbours((int y, int x) spot)
        {
            List<(int y, int x)> neighbours = new List<(int y, int x)>();
            if (spot.y < 7)
            {
                neighbours.Add((spot.y + 1, spot.x));
            }
            if (spot.y > 0)
            {
                neighbours.Add((spot.y - 1, spot.x));
            }
            if (spot.x < 7)
            {
                neighbours.Add((spot.y, spot.x + 1));
            }
            if (spot.x > 0)
            {
                neighbours.Add((spot.y, spot.x - 1));
            }
            return neighbours;
        }
        private (int y, int x) getBestNeighbour((int y ,int x) spot)
        {
            List<(int y, int x)> neighbours = Neighbours(spot);
            (int y, int x) best = neighbours[0];
            foreach((int y, int x) n in neighbours)
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
