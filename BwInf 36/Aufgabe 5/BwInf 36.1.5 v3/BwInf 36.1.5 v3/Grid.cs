using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BwInf
{
    public class Grid
    {
        public Grid(Display activeForm)
        {
            this.ActiveForm = activeForm;
            int[,] values = new int[8, 8];
            for (int i = 0; i < 64; i++)
            {
                int x = i % 8;
                int y = i / 8;
                values[y, x] = 0;
            }
            this.LastValues = new int[8, 8];
            for (int i = 0; i < 64; i++)
            {
                int x = i % 8;
                int y = i / 8;
                this.LastValues[y, x] = -1;
            }
            Bitmap bmp = new Bitmap(800, 800);
            for (int y = 0; y < 800; y++)
            {
                for (int x = 0; x < 800; x++)
                {
                    bmp.SetPixel(x, y, Color.Gray);
                }
            }
            this.ActiveForm.pbGrid.Image = bmp;
            this.Colors = new Color[5] { Color.Gray, Color.White, Color.Black, Color.Blue, Color.Red };
            this.Values = values;

        }
        public Grid(Display activeForm, int[,] values)
        {
            this.ActiveForm = activeForm;
            this.LastValues = new int[8, 8];
            for (int i = 0; i < 64; i++)
            {
                int x = i % 8;
                int y = i / 8;
                this.LastValues[y, x] = -1;
            }
            Bitmap bmp = new Bitmap(800, 800);
            for (int y = 0; y < 800; y++)
            {
                for (int x = 0; x < 800; x++)
                {
                    bmp.SetPixel(x, y, Color.Gray);
                }
            }
            this.ActiveForm.pbGrid.Image = bmp;
            this.Colors = new Color[3] { Color.Gray, Color.White, Color.Black };
            this.Values = values;
        }

        private Display varActiveForm;
        public Display ActiveForm
        {
            get { return varActiveForm; }
            set
            {
                varActiveForm = value;
            }
        }

        private Point varActiveSpot = new Point(-1, -1);
        public Point ActiveSpot
        {
            get { return varActiveSpot; }
            set { varActiveSpot = value; Update(); }
        }

        private Color[] varColors;
        public Color[] Colors
        {
            get { return varColors; }
            set
            {
                varColors = value;
                ResetLastValues();
            }
        }

        private int[,] varValues = new int[8, 8];
        public int[,] Values
        {
            get { return varValues; }
            set { varValues = value; Update(); }
        }

        public int Height
        {
            get { return varValues.GetLength(0); }
        }
        public int Width
        {
            get { return varValues.GetLength(1); }
        }

        protected int MoveCount = 0;

        public Point Size
        {
            get { return new Point(this.ActiveForm.pbGrid.Height, this.ActiveForm.pbGrid.Width); }
        }
        public Point SingleSpotSize
        {
            get { return new Point(this.Size.y / Height, this.Size.x / Width); }
        }

        private int[,] LastValues { get; set; }
        private void ResetLastValues()
        {
            LastValues = new int[Height, Width];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    LastValues[y, x] = -1;
                }
            }
            Update();
        }

        private void Update()
        {
            if (LastValues == null)
            {
                ResetLastValues();
            }
            else
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (Values[y, x] != LastValues[y, x])
                        {
                            UpdateSingleSpot(new Point(y, x));
                        }
                    }
                }
            }
            this.LastValues = this.Values;
            this.ActiveForm.pbGrid.Update();
        }
        private void UpdateSingleSpot(Point spot)
        {
            Point PixelOffset = this.PixelOffset(spot);
            Point SingleSpotSize = this.SingleSpotSize;
            Bitmap GridImage = new Bitmap(this.ActiveForm.pbGrid.Image);
            if (spot.y == this.ActiveSpot.y && spot.x == this.ActiveSpot.x)
            {
                for (int y = 0; y < SingleSpotSize.y; y++)
                {
                    for (int x = 0; x < SingleSpotSize.x; x++)
                    {
                        GridImage.SetPixel(PixelOffset.x + x, PixelOffset.y + y, Colors[4]);
                    }
                }
            }
            else
            {
                for (int y = 0; y < SingleSpotSize.y; y++)
                {
                    for (int x = 0; x < SingleSpotSize.x; x++)
                    {
                        GridImage.SetPixel(PixelOffset.x + x, PixelOffset.y + y, Colors[Values[spot.y, spot.x]]);
                    }
                }
            }
            this.ActiveForm.pbGrid.Image = GridImage;
        }
        private Point PixelOffset (Point point)
        {
            return new Point((this.Size.y / this.Height) * point.y, (this.Size.x / this.Width) * point.x);
        }
        private (int value, int y, int x) lastActive { get; set; }
        protected void setActive(Point spot)
        {

            this.ActiveSpot = spot;

            //int[,] tempValues = (int[,])this.Values.Clone();
            //tempValues[lastActive.y, lastActive.x] = lastActive.value;
            //lastActive = (tempValues[spot.y, spot.x], spot.y, spot.x);
            //tempValues[spot.y, spot.x] = 4;
            //this.Values = tempValues;

        }
        public (string move, bool successful) Move(Move move)
        {
            int[,] tempValues = (int[,])this.Values.Clone();
            (string direction, int distance) moveDetails = MoveDetails(move);
            if (move.Start.y == -1 && move.Start.x == -1 && move.Target.y >= 0 && move.Target.x >= 0 && move.Target.y < 8 && move.Target.x < 8 && this.Values[move.Target.y, move.Target.x] == 0)
            {
                if (MoveCount == 0)
                {
                    tempValues[move.Target.y, move.Target.x] = 2;
                    this.Values = (int[,])tempValues.Clone();
                    MoveCount++;
                    return ("[---Placed----]: Enemy", true);
                }
                else
                {
                    return ("invalid", false);
                }
            }
            else if (move.Start.y < 0 || move.Start.x < 0 || move.Target.y < 0 || move.Target.x < 0 || move.Start.y > 7 || move.Start.x > 7 || move.Target.y > 7 || move.Target.x > 7)
            {
                return ("invalid", false);
            }
            int startValue = this.Values[move.Start.y, move.Start.x];
            int targetValue = this.Values[move.Target.y, move.Target.x];
            if (PathBlocked(move))
            {
                return ("There's something in the way", false);
            }
            if (moveDetails.direction == "invalid")
            {
                return ("Invalid Direction", false);
            }
            if (moveDetails.direction == "diagonal" && startValue < 3)
            {
                return ("Invalid Direction for the chosen Spot", false);
            }
            if (moveDetails.distance > 1 && startValue < 2)
            {
                return ("The Distance is too long, it's a pawn you're trying to move", false);
            }
            if (startValue == 0)
            {
                return ("You can't move null-objects", false);
            }
            if (startValue == targetValue)
            {
                if (startValue == 1)
                {
                    return ("You tried to move a pawn where you already have a pawn", false);
                }
                else
                {
                    MoveCount++;
                    return ("[---Skipped---]: Enemy", true);
                }
            }
            if (startValue == 1)
            {
                tempValues[move.Start.y, move.Start.x] = 0;
                tempValues[move.Target.y, move.Target.x] = this.Values[move.Start.y, move.Start.x];
                this.Values = (int[,])tempValues.Clone();
                if (targetValue != 0)
                {
                    GameOver = true;
                }
                MoveCount++;
                return ("[" + move.Start.x.ToString() + ", " + move.Start.y.ToString() + "] > [" + move.Target.x.ToString() + ", " + move.Target.y.ToString() + "]: Pawn ", true);
            }
            if (startValue > 1)
            {
                if (targetValue == 0)
                {
                    tempValues[move.Start.y, move.Start.x] = 0;
                    tempValues[move.Target.y, move.Target.x] = this.Values[move.Start.y, move.Start.x];
                    this.Values = (int[,])tempValues.Clone();
                    MoveCount++;
                    return ("[" + move.Start.x.ToString() + ", " + move.Start.y.ToString() + "] > [" + move.Target.x.ToString() + ", " + move.Target.y.ToString() + "]: Enemy", true);
                }
                else
                {
                    return ("You can't move a black figure into a pawn, that would end the game", false);
                }
            }
            return ("", false);
        }
        protected (string direction, int distance) MoveDetails(Move move)
        {
            if (move.Start.x == move.Target.x && move.Start.y == move.Target.y)
            {
                return ("none", 0);
            }
            else if (move.Start.x == move.Target.x && move.Start.y != move.Target.y)
            {
                return ("vertical", Math.Abs(move.Start.y - move.Target.y));
            }
            else if (move.Start.x != move.Target.x && move.Start.y == move.Target.y)
            {
                return ("horizontal", Math.Abs(move.Start.x - move.Target.x));
            }
            else if (Math.Abs(move.Start.x - move.Target.x) == Math.Abs(move.Start.y - move.Target.y))
            {
                return ("diagonal", Math.Abs(move.Start.y - move.Target.y));
            }
            else
            {
                return ("invalid", -1);
            }
        }
        protected int distance(Point pos1, Point pos2)
        {
            int yDif = Math.Abs(pos1.y - pos2.y);
            int xDif = Math.Abs(pos2.x - pos1.x);
            return yDif + xDif;
        }
        private bool PathBlocked(Move move)
        {
            (string direction, int distance) moveDetails = MoveDetails(move);
            if (moveDetails.direction == "invalid") { return false; }
            if (moveDetails.distance == 0) { return false; }
            int SignY = 0;
            int SignX = 0;
            if (move.Start.x > move.Target.x)
            {
                SignX = -1;
            }
            else if (move.Start.x < move.Target.x)
            {
                SignX = 1;
            }
            if (move.Start.y > move.Target.y)
            {
                SignY = -1;
            }
            else if (move.Start.y < move.Target.y)
            {
                SignY = 1;
            }
            for (int i = 0; i < moveDetails.distance; i++)
            {
                if (this.Values[move.Start.y + SignY * i, move.Start.x + SignX * i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private bool varGameOver = false;
        public bool GameOver
        {
            get { return varGameOver; }
            set
            {
                if (value)
                {
                    Console.WriteLine("Game Over! It took {0} moves.", MoveCount);
                    varGameOver = true;
                }
            }
        }
    }
}