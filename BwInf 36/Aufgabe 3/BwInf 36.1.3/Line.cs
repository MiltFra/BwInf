using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    public class Line
    {
        // just putting the values in there and getting min and max of x and y
        public Line(double x1, double y1, double x2, double y2)
        {
            this.A = y1 - y2;
            this.B = x2 - x1;
            this.C = x2 * y1 - x1 * y2;
            if (x1 > x2)
            {
                this.X[0] = x2;
                this.X[1] = x1;
            }
            else
            {
                this.X[0] = x1;
                this.X[1] = x2;
            }
            if (y1 > y2)
            {
                this.Y[0] = y2;
                this.Y[1] = y1;
            }
            else
            {
                this.Y[0] = y1;
                this.Y[1] = y2;
            }
        }

        // a * x + b * y = c 
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        // min and max of x and y
        public double[] X = new double[2];
        public double[] Y = new double[2];
        // checks if there is a point at which these lines intersect
        public double[] SingleSharedPoint(Line line2)
        {
            Line line1 = this;
            //don't want to divide by zero, treating those cases differently
            if (line1.B == 0) { return SingleShardPointWithVerticalLine(line2, line1); }
            else if (line2.B == 0) { return SingleShardPointWithVerticalLine(line1, line2); }
            //parallels don't have a shared point
            else if (AreParallel(line1, line2)) { return new double[1]; }
            //finding the point, checking if it is in the boundaries of the lines
            else
            {
                //basic analysis
                double x = (line1.C - (line1.B / line2.B) * line2.C) / (line1.A - (line1.B / line2.B) * line2.A);
                double y = line1.ValueAt(x);
                // that point is only valid if it's in the area of both lines
                if (PointIsInAreaOfBothLines(line1, line2, new double[2] { x, y }))
                {
                    return new double[2] { x, y };
                }
                else { return new double[1]; }
            }

        }
        private double[] SingleShardPointWithVerticalLine(Line normalLine, Line verticalLine)
        {
            // two verticals don't have a single shared point, just zero or infinite
            if (normalLine.B == 0) { return new double[1]; }

            double x = verticalLine.X[0];
            double y = normalLine.ValueAt(verticalLine.X[0]);
            // as long as both the lines are defined in the needed area, they will have a single shared point
            if (PointIsInAreaOfBothLines(verticalLine, normalLine, new double[2] { x, y }))
            {
                return new double[2] { x, y };
            }
            // if it isn't in the area, return something invalid
            else { return new double[1]; }

        }
        public double ValueAt(double x)
        {
            if (x >= this.X[0] && x <= this.X[1] && this.B != 0)
            {
                return -(this.A / this.B) * x + (this.C / this.B); //converted to linear function
            }
            else
            {
                return this.X[1] + 1; //not a function => no value; no value for x defined => no value; if the value is out of boundaries, it can't be used
            }
        }
        // true if a1 = a2 and b1 = b2
        private bool AreParallel(Line line1, Line line2)
        {
            return line1.A == line2.A && line1.B == line2.B;
        }
        private bool PointIsInAreaOfBothLines(Line line1, Line line2, double[] point)
        {
            // checking for invalid points
            if (point.Count() != 2) { return false; }
            // assigning some easy to read names
            double x = point[0];
            double y = point[1];  
            // returning some not so easy to read bool which is exactly what the name of this method suggests
            return x >= line1.X[0] && x <= line1.X[1] && x >= line2.X[0] && x <= line2.X[1] && y >= line1.Y[0] && y <= line1.Y[1] && y >= line2.Y[0] && y <= line2.Y[1];
        }
    }
}
