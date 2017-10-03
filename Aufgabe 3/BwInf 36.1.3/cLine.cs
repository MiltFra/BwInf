using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._3
{
    public class cLine
    {
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public double[] x = new double[2]; //min, max
        public double[] y = new double[2];
        public double[] singleSharedPoint(cLine line2)
        {
            cLine line1 = this;
            //don't want to divide by zero, treating those cases differently
            if (line1.b == 0)
            {
                if (line2.b == 0)
                {
                    return new double[1];
                }
                else
                {
                    double x = line1.x[0];
                    double y = line2.valueAt(line1.x[0]);
                    if (y >= line1.y[0] && y <= line1.y[1] && y >= line2.y[0] && y <= line2.y[1])
                    {
                        return new double[2] { x, y };
                    }
                    else
                    {
                        return new double[1];
                    }
                }
            }
            else if (line2.b == 0)
            {
                if (line1.b == 0)
                {
                    return new double[1];
                }
                else
                {
                    double x = line2.x[0];
                    double y = line1.valueAt(line1.x[0]);
                    if (y >= line1.y[0] && y <= line1.y[1] && y >= line2.y[0] && y <= line2.y[1])
                    {
                        return new double[2] { x, y };
                    }
                    else
                    {
                        return new double[1];
                    }
                }
            }
            //parallels don't have a shared point
            else if (line1.a == line2.a && line1.b == line2.b)
            {
                return new double[1];
            }
            //finding the point, checking if it is in the boundaries of the lines
            else
            {
                //basic analysis
                double x = (line1.c - (line1.b / line2.b) * line2.c) / (line1.a - (line1.b / line2.b) * line2.a);
                double y = line1.valueAt(x);
                if (x >= line1.x[0] && x <= line1.x[1] && x >= line2.x[0] && x <= line2.x[1] && y >= line1.y[0] && y <= line1.y[1] && y >= line2.y[0] && y <= line2.y[1])
                {
                    return new double[2] { x, y };
                }
                else
                {
                    return new double[1];
                }
            }

        }
        public double valueAt(double x)
        {
            if (x >= this.x[0] && x <= this.x[1] && this.b != 0)
            {
                return -(this.a / this.b) * x + (this.c / this.b); //converted to linear function
            }
            else
            {
                return this.x[1] + 1; //not a function => no value; no value for x defined => no value; if the value is out of boundaries, it can't be used
            }
        }
        public cLine(double x1, double y1, double x2, double y2)
        {
            this.a = y1 - y2;
            this.b = x2 - x1;
            this.c = x2 * y1 - x1 * y2;
            if (x1 > x2)
            {
                this.x[0] = x2;
                this.x[1] = x1;
            }
            else
            {
                this.x[0] = x1;
                this.x[1] = x2;
            }
            if (y1 > y2)
            {
                this.y[0] = y2;
                this.y[1] = y1;
            }
            else
            {
                this.y[0] = y1;
                this.y[1] = y2;
            }
        }
    }
}
