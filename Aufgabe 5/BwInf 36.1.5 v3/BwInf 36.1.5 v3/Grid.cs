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

        private int[,] varValues;
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
            LastValues = new int[Height - 1, Width - 1];
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
        }

        private (int y, int x) PixelOffset(int Y, int X)
        {
            return ((this.Size.y / this.Height) * Y, (this.Size.x / this.Width) * X);
        }

        public Grid(Form1 activeForm)
        {
            this.ActiveForm = activeForm;
        }
    }
}