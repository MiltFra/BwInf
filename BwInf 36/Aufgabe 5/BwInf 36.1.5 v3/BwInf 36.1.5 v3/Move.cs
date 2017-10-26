using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Move
    {
        public  Point Start { get; set; }
        public  Point Target { get; set; }
        public Move(Point start,  Point target)
        {
            this.Start = start;
            this.Target = target; 
        }
    }
}
