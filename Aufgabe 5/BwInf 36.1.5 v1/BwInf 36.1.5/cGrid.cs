using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BwInf_36._1._5
{
    public static class cGrid
    {
        private static int[,] _grid;

        public static int[,] grid
        {
            get
            {
                return _grid;
            }
            set
            {
                int pawnCount = 0;
                int rookCount = 0;
                if ((value.GetLength(0) != 8) || (value.GetLength(1) != 8))
                {
                    throw new Exception();
                }
                List<int[]> tempPawnPositions = new List<int[]>();
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        switch (value[y, x])
                        {
                            case 1:
                                int[] pawnPosition = new int[2] { y, x };
                                tempPawnPositions.Add(pawnPosition);
                                pawnCount++;
                                break;
                            case 2:
                                rookCount++;
                                int[] tempRookPosition = new int[2];
                                tempRookPosition[0] = y;
                                tempRookPosition[1] = x;
                                rookPosition = tempRookPosition;
                                break;
                        }
                    }
                }
                pawnPositions = tempPawnPositions;
                if (rookCount <= 1)
                {
                    _grid = value;
                }
            }
        }
        private static int[] rookPosition { get; set; }
        private static List<int[]> pawnPositions { get; set; }

        public static Bitmap render()
        {
            Bitmap output = new Bitmap(650, 650);
            using (Graphics gfx = Graphics.FromImage(output))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(104, 71, 42)))
            {
                gfx.FillRectangle(brush, 0, 0, 650, 650);
            }
            for (int i = 0; i < 64; i++)
            {
                int x = i / 8; //horizontal
                int y = i % 8; //vertical
                switch (grid[y, x])
                {
                    case 0: //empty space
                        for (int j = 0; j < 4900; j++)
                        {
                            if ((x + y) % 2 == 1)
                            {
                                output.SetPixel(x * 80 + 10 + j % 70, y * 80 + 10 + j / 70, Color.FromArgb(135, 82, 44));
                            }
                            else
                            {
                                output.SetPixel(x * 80 + 10 + j % 70, y * 80 + 10 + j / 70, Color.FromArgb(170, 146, 88));
                            }
                        }
                        break;
                    case 1: //pawn
                        for (int j = 0; j < 4900; j++)
                        {
                            output.SetPixel(x * 80 + 10 + j % 70, y * 80 + 10 + j / 70, Color.White);
                        }
                        break;
                    case 2: //rook
                        for (int j = 0; j < 4900; j++)
                        {
                            output.SetPixel(x * 80 + 10 + j % 70, y * 80 + 10 + j / 70, Color.Black);
                        }
                        break;
                }
            }
            return output;
        }

        public static void moveRook(string direction, int distance)
        {
            switch (direction)
            {
                case "n":
                    rookPosition[0] -= distance;
                    break;
                case "s":
                    rookPosition[0] += distance;
                    break;
                case "w":
                    rookPosition[1] -= distance;
                    break;
                case "e":
                    rookPosition[1] += distance;
                    break;
            }
        }
        public static void movePawn(string direction, int index)
        {
            switch (direction)
            {
                case "n":
                    pawnPositions[index][0]--;
                    break;
                case "s":
                    pawnPositions[index][0]++;
                    break;
                case "w":
                    pawnPositions[index][1]--;
                    break;
                case "e":
                    pawnPositions[index][1]++;
                    break;
            }
        }
        public static void setPawn(int x, int y)
        {
            if (grid[y, x] == 0)
            {
                grid[y, x] = 1;
            }
        }
        public static void setRook(int x, int y)
        {
            clearRook();
            if (grid[y, x] == 0)
            {
                grid[y, x] = 2;
            }
        }
        public static void clearRook()
        {
            for (int i = 0; i < 64; i++)
            {
                int x = i % 8;
                int y = i / 8;
                if (grid[y, x] == 2)
                {
                    grid[y, x] = 0;
                    break;
                }
            }
        }
        public static void clearPawns()
        {
            for (int i = 0; i < 64; i++)
            {
                int x = i % 8;
                int y = i / 8;
                if (grid[y, x] == 1)
                {
                    grid[y, x] = 0;
                }
            }
        }
        public static List<int[]> getRookMoves()
        {
            List<int[]> total = new List<int[]>();
            int currentValue = rookPosition[0];
            while (true) //up
            {
                currentValue--;
                if (currentValue < 0)
                {
                    break;
                }
                int[] currentPosition = new int[2] { currentValue, rookPosition[1] };
                foreach (int[] currentPawnPosition in pawnPositions)
                {
                    if (currentPawnPosition == currentPosition)
                    {
                        break;
                    }
                }
                total.Add(currentPosition);
            }
            currentValue = rookPosition[0];
            while (true) //down
            {
                currentValue++;
                if (currentValue > 7)
                {
                    break;
                }
                int[] currentPosition = new int[2] { currentValue, rookPosition[1] };
                foreach (int[] currentPawnPosition in pawnPositions)
                {
                    if (currentPawnPosition == currentPosition)
                    {
                        break;
                    }
                }
                total.Add(currentPosition);
            }
            currentValue = rookPosition[1];
            while (true) //left
            {
                currentValue--;
                if (currentValue < 0)
                {
                    break;
                }
                int[] currentPosition = new int[2] { rookPosition[0], currentValue };
                foreach (int[] currentPawnPosition in pawnPositions)
                {
                    if (currentPawnPosition == currentPosition)
                    {
                        break;
                    }
                }
                total.Add(currentPosition);
            }
            currentValue = rookPosition[0];
            while (true) //right
            {
                currentValue++;
                if (currentValue > 7)
                {
                    break;
                }
                int[] currentPosition = new int[2] { rookPosition[0], currentValue };
                foreach (int[] currentPawnPosition in pawnPositions)
                {
                    if (currentPawnPosition == currentPosition)
                    {
                        break;
                    }
                }
                total.Add(currentPosition);
            }
            return total;
        }
        public static bool checkCollision()
        {
            foreach (int[] currentPawnPosition in pawnPositions)
            {
                if ((currentPawnPosition[0] == rookPosition[0] - 1 || currentPawnPosition[0] == rookPosition[0] - 1) && currentPawnPosition[1] == rookPosition[1])
                {
                    return true;
                }
                if ((currentPawnPosition[1] == rookPosition[1] - 1 || currentPawnPosition[1] == rookPosition[1] - 1) && currentPawnPosition[0] == rookPosition[0])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
