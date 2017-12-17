using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf
{
    // A Move is an Action that has a Start and Endpoint
    public class Move : IEquatable<Move>
    {
        // - Just a basic Constructor, nothing special going on here
        public Move(Point start, Point target)
        {
            this.varStart = start;
            this.varTarget = target;
        }

        // - Start and End Points of the Move, Update Details when either is changed
        private Point varStart;
        public Point Start
        {
            get { return varStart; }
            set
            {
                varStart = value;
                UpdateDetails();
            }
        }

        private Point varTarget;
        public Point Target
        {
            get { return varTarget; }
            set
            {
                varTarget = value;
                UpdateDetails();
            }
        }

        // - Details contain a Direction and a Distance
        private (string direction, int distance) varDetails = ("", -1);
        public (string direction, int distance) Details
        {
            get
            {
                if (varDetails.distance < 0) { UpdateDetails(); }
                return varDetails;
            }
        }

        // Recalculates the Details Property
        private void UpdateDetails()
        {
            if (StartIsTarget()) { this.varDetails = ("none", 0); }

            else if (IsVertical()) { this.varDetails = ("vertical", Math.Abs(Start.y - Target.y)); }

            else if (IsHorizontal()) { this.varDetails = ("horizontal", Math.Abs(Start.x - Target.x)); }

            else if (IsDiagonal()) { this.varDetails = ("diagonal", Math.Abs(Start.y - Target.y)); }

            else { this.varDetails = ("invalid", -1); }
        }

        // - Start == Target
        private bool StartIsTarget() { return Start.x == Target.x && Start.y == Target.y; }

        // - X = X; Y != Y
        private bool IsVertical() { return Start.x == Target.x && Start.y != Target.y; }

        // - X != X; Y = Y
        private bool IsHorizontal() { return Start.x != Target.x && Start.y == Target.y; }

        // Is on the same line with m = 1
        private bool IsDiagonal() { return Math.Abs(Start.x - Target.x) == Math.Abs(Start.y - Target.y); }

        // - When any of the coordinates is outside the given Grid
        public bool IsOutsideGrid() { return Start.IsOutsideGrid() || Target.IsOutsideGrid(); }

        // 
        public bool Equals(Move other)
        {
            if (this.Start != other.Start) return false;
            if (this.Target != other.Target) return false;
            return true;
        }
    }
}
