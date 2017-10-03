using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._5_v2
{
    public class cPawn : cFigure
    {
        public cPawn(int x, int y)
        {
            this.maximumDirection = 4;
            this.minimumDirection = 1;
            this.maximumDistance = 1;
            this.minimumDistance= 1;
            this.x = x;
            this.y = y;
        }
    }
}
