using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._5_v3
{
    public class Move
    {
        public (int y, int x) Start { get; set; }
        public (int y, int x) Target { get; set; }
        public Move(( int y, int x) start, (int y, int x) target)
        {
            this.Start = start;
            this.Target = target; 
        }
    }
}
