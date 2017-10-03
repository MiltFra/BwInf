using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._5_v2
{
    public class cMove
    {
        public int originX { get; set; }
        public int originY { get; set; }
        public int targetX { get; set; }
        public int targetY { get; set; }

        public double distance()
        {
            if (direction() <= 4)
            {
                return Math.Abs(originX - targetX + originY - targetY);
            }
            else if (direction() <= 8)
            {
                return Math.Abs(originX - targetX);
            }
            return Math.Sqrt(Math.Pow(originX - targetX, 2) + Math.Pow(originY - targetY, 2));
        }
        //none, up, down, left, right, up-left, up-right, down-left, down-right, undefined
        public int direction()
        {
            if (originY == targetY)
            {
                //no move
                if (originX == targetX)
                {
                    return 0;
                }
                //left
                else if (originX > targetX)
                {
                    return 3;
                }
                //right
                else if (originX < targetX)
                {
                    return 4;
                }
            }
            else if (originY < targetY)
            {
                //down
                if (originX == targetX)
                {
                    return 2;
                }
                //down-left
                else if (originX > targetX)
                {
                    if (originX - targetX == originY - targetY)
                    {
                        return 7;
                    }
                }
                //down-right
                else if (originX < targetX)
                {
                    if (originX - targetX == originY - targetY)
                    {
                        return 8;
                    }
                }
            }
            else if (originY > targetY)
            {
                //up
                if (originX == targetX)
                {
                    return 1;
                }
                //up-left
                else if (originX > targetX)
                {
                    if (originX - targetX == originY - targetY)
                    {
                        return 5;
                    }
                }
                //up-right
                else if (originX < targetX)
                {
                    if (originX - targetX == originY - targetY)
                    {
                        return 6;
                    }
                }
            }
            //undefined
            return 9;
        }

        public cMove(int originX, int originY, int targetX, int targetY)
        {
            if (originX < 8 && originY < 8 && targetX < 8 && targetY < 8 && originX >= -1 && originY >= -1 && targetX >= -1 && targetY >= -1)
            {
                this.originX = originX;
                this.originY = originY;
                this.targetX = targetX;
                this.targetY = targetY;
            }
            else
            {
                this.originX = -1;
                this.originY = -1;
                this.targetX = -1;
                this.targetY = -1;
            }
        }
    }
}
