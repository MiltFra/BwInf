using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Point
    {
        public Point(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        public int y { get; set; }
        public int x { get; set; }
    }
}
