using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    // Just two coordinates, could as well be described as (int y, int x)
    public class Point : IEquatable<Point>
    {
        public Point(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        public int y { get; set; }
        public int x { get; set; }

        public bool Equals(Point other)
        {
            if (this.y != other.y) return false;
            if (this.x != other.x) return false;
            return true;
        }

        // - When the coordinates are outside the given Grid
        public bool IsOutsideGrid() { return y < 0 || x < 0 || y > 7 || x > 7; }
    }
}
