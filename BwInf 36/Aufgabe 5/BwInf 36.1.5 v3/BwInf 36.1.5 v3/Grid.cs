using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BwInf_36._1._5_v3
{
    public class Grid
    {
        public Grid(Form1 activeForm)
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
            this.Colors = new Color[3] { Color.Gray, Color.White, Color.Black };
            this.Values = values;

        }
        public Grid(Form1 activeForm, int[,] values)
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

        private Form1 varActiveForm;
        public Form1 ActiveForm
        {
            get { return varActiveForm; }
            set
            {
                varActiveForm = value;
            }
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

        private int MoveCount = 0;

        public (int y, int x) Size
        {
            get { return (this.ActiveForm.pbGrid.Height, this.ActiveForm.pbGrid.Width); }
        }
        public (int y, int x) SingleSpotSize
        {
            get { return (this.Size.y / Height, this.Size.x / Width); }
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
                            UpdateSingleSpot(y, x);
                        }
                    }
                }
            }
            this.LastValues = this.Values;
            this.ActiveForm.Update();
        }
        private void UpdateSingleSpot(int Y, int X)
        {
            (int y, int x) PixelOffset = this.PixelOffset(Y, X);
            (int y, int x) SingleSpotSize = this.SingleSpotSize;
            Bitmap GridImage = new Bitmap(this.ActiveForm.pbGrid.Image);
            for (int y = 0; y < SingleSpotSize.y; y++)
            {
                for (int x = 0; x < SingleSpotSize.x; x++)
                {
                    GridImage.SetPixel(PixelOffset.x + x, PixelOffset.y + y, Colors[Values[Y, X]]);
                }
            }
            this.ActiveForm.pbGrid.Image = GridImage;
        }
        private (int y, int x) PixelOffset(int Y, int X)
        {
            return ((this.Size.y / this.Height) * Y, (this.Size.x / this.Width) * X);
        }

        public (string move, bool successful) Move(Move move)
        {
            (string direction, int distance) moveDetails = MoveDetails(move);

            int startValue = this.Values[move.Start.y, move.Start.x];
            int targetValue = this.Values[move.Target.y, move.Target.x];
            int[,] tempValues = (int[,])this.Values.Clone();
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
        private (string direction, int distance) MoveDetails(Move move)
        {
            if (move.Start.x == move.Target.x && move.Start.y != move.Target.y)
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
        private bool PathBlocked(Move move)
        {
            (string direction, int distance) moveDetails = MoveDetails(move);
            if (moveDetails.direction == "invalid") { return false; }
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