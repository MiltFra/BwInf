using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._5_v2
{
    public class cFigure
    {
        public int x { get; set; }
        public int y { get; set; }
        public int maximumDirection = new int();
        public int minimumDirection = new int();
        public double maximumDistance = new int();
        public double minimumDistance = new int();
        public void move(cMove move)
        {
            double direction = move.direction();
            double distance = move.distance();
            if (direction >= this.minimumDirection && direction <= this.maximumDirection)
            {
                if (distance >= this.minimumDistance && distance <= this.maximumDistance)
                {
                    if (move.originX == this.x && move.originY == this.y)
                    {
                        Console.WriteLine("Move: [" + move.originX.ToString() + ";" + move.originY.ToString() + "] >> [" + move.targetX.ToString() + ";" + move.targetY.ToString() + "]");
                        this.x = move.targetX;
                        this.y = move.targetY;
                    }
                    else
                    {
                        Console.WriteLine("Invalid origin!");
                    }
                }
                else
                {
                    throw new Exception();
                    Console.WriteLine("Invalid distance!");
                }
            }
            else
            {
                Console.WriteLine("Invalid direction!");
            }
        }
    }
}
