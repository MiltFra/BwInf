using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BwInf
{
    // Basic abstract class to symbolize 
    public class Grid
    {
        // Constructor 

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
            this.Colors = new Color[5] { Color.Gray, Color.White, Color.Black, Color.Blue, Color.Red };
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

        // Properties

        // - Contains the Display Form where all the Outputs are supposed to go
        private Form1 varActiveForm;
        public Form1 ActiveForm
        {
            get { return varActiveForm; }
            set { varActiveForm = value; }
        }

        // - Contains the Coordinates of a Spot where the Calculation currently is, gets highlighted in red, mostly for debugging
        private Point varActiveSpot = new Point(-1, -1);
        public Point ActiveSpot
        {
            get { return varActiveSpot; }
            set { varActiveSpot = value; Update(); }
        }

        // - Stores all the Values of the Grid; Values[y,x]
        private int[,] varValues = new int[8, 8];
        public int[,] Values
        {
            get { return varValues; }
            set
            {
                varValues = value;
                Update();
            }
        }

        // - Translates the actual Values into Colors to be drawn
        private Color[] varColors;
        public Color[] Colors
        {
            get { return varColors; }
            set
            {
                varColors = value;
                Update();
            }
        }

        // - Size Values of the Grid, based on SpotCount, not PixelCount
        private Point varSize;
        public Point Size
        {
            get { return varSize; }
            set { varSize = value; }
        }

        // - How many Moves have been made so far
        private int varMoveCount = 0;
        protected int MoveCount
        {
            get { return varMoveCount; }
            private set
            {
                varMoveCount = value;
                this.ActiveForm.lbl_count.Text = varMoveCount.ToString();
                this.ActiveForm.lbl_count.Update();
            }
        }

        // - Size Values of the Grid, based on PixelCount
        public Point SizePX
        {
            get { return new Point(this.ActiveForm.pbGrid.Height, this.ActiveForm.pbGrid.Width); }
        }

        public Point SingleSpotSizePX
        {
            get { return new Point(SizePX.y / Size.y, SizePX.x / Size.y); }
        }

        // - Returns the current Postition of the Black Figure
        private Point varBlackPosition;
        protected Point BlackPosition
        {
            get
            {
                if (varBlackPosition == null) { UpdateBlackPosition(); }
                return varBlackPosition;
            }
            set { varBlackPosition = value; }
        }

        // - Returns the current Positions of the White Figures
        private List<Point> varWhitePositions;
        protected List<Point> WhitePositions
        {
            get
            {
                if (varWhitePositions == null) { UpdateWhitePositions(); }
                return varWhitePositions;
            }
            set { varWhitePositions = value; }
        }

        // - Stores all possible Black Moves
        private List<Move> varPossibleBlackMoves;
        public List<Move> PossibleBlackMoves
        {
            get
            {
                if (varPossibleBlackMoves == null) { UpdatePossibleBlackMoves(); }
                return varPossibleBlackMoves;
            }
            set { varPossibleBlackMoves = value; }
        }

        // - Stores all possible White Moves
        private List<Move> varPossibleWhiteMoves;
        public List<Move> PossibleWhiteMoves
        {
            get
            {
                if (varPossibleWhiteMoves == null) { UpdatePossibleWhiteMoves(); }
                return varPossibleWhiteMoves;
            }
            set { varPossibleWhiteMoves = value; }
        }

        // - Ideal Positions for Black Figures
        private List<Point> varIdealPositions;
        public List<Point> IdealPositions
        {
            get
            {
                if (varIdealPositions == null) { UpdateIdealPositions(); }
                return varIdealPositions;
            }
            set { varIdealPositions = value; }
        }

        // - Half Ideal Positions, just one row had to be empty
        private List<Point> varHalfIdealPositions;
        protected List<Point> HalfIdealPositions
        {
            get
            {
                if (varHalfIdealPositions == null) { UpdateHalfIdealBlackPositions(); }
                return varHalfIdealPositions;
            }
            set { varHalfIdealPositions = value; }
        }


        // - Indicated wether the Game is over ... quite simple
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


        // - Property for the Update Method, stores the last Updated Values Array
        private int[,] LastValues { get; set; }

        // Methods

        // - Applies a given Move to the current Values, if there are complications they will be printed to the console - same when everything works
        public (string move, bool successful) Move(Move move)
        {
            if (IsFirstMove(move) && TargetSpotEmpty(move))
            {
                if (FirstMoveAvailable()) { return FirstMove(move); }
                else { return ("invalid", false); }
            }
            else if (move.IsOutsideGrid()) { return ("invalid", false); }

            if (PathBlocked(move)) { return ("There's something in the way", false); }

            int startValue = this.Values[move.Start.y, move.Start.x];
            int targetValue = this.Values[move.Target.y, move.Target.x];

            if (move.Details.direction == "invalid") { return ("Invalid Direction", false); }

            if (move.Details.direction == "diagonal" && startValue < 3) { return ("Invalid Direction for the chosen Spot", false); }

            if (move.Details.distance > 1 && startValue < 2) { return ("The Distance is too long, it's a pawn you're trying to move", false); }

            if (startValue == 0) { return ("You can't move null-objects", false); }

            if (startValue == targetValue) { return SkipMove(move); }

            if (startValue == 1) { return PawnMove(move); }

            if (startValue > 1) { return BlackMove(move); }

            return ("", false);
        }

        // - Returns how many empty Slots are in every row
        protected (int[] inY, int[] inX) Empty()
        {
            int[] emptyInX = new int[8];
            int[] emptyInY = new int[8];
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (this.Values[y, x] != 1)
                {
                    emptyInX[x]++;
                    emptyInY[y]++;
                }
            }
            return (emptyInY, emptyInX);
        }
        protected (int[] inY, int[] inX) Empty(int[,] values)
        {
            int[] emptyInX = new int[8];
            int[] emptyInY = new int[8];
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (values[y, x] != 1)
                {
                    emptyInX[x]++;
                    emptyInY[y]++;
                }
            }
            return (emptyInY, emptyInX);
        }

        // - Treats the Move as the First
        private (string move, bool successful) FirstMove(Move move)
        {
            int[,] tempValues = (int[,])this.Values.Clone();
            tempValues[move.Target.y, move.Target.x] = 2;
            this.Values = (int[,])tempValues.Clone();
            MoveCount++;
            return ("[---Placed----]: Enemy", true);
        }

        // - The Move is Skipped, if the "moved" Spot has a Black Figure on it
        private (string move, bool successful) SkipMove(Move move)
        {
            if (Values[move.Start.y, move.Start.x] == 1) { return ("You tried to move a pawn where you already have a pawn", false); }
            else { MoveCount++; return ("[---Skipped---]: Enemy", true); }
        }

        // - Treats the Move as a Pawn Move
        private (string move, bool successful) PawnMove(Move move)
        {
            if (Values[move.Target.y, move.Target.x] != 0)
            {
                GameOver = true;
            }
            int[,] tempValues = (int[,])this.Values.Clone();
            tempValues[move.Start.y, move.Start.x] = 0;
            tempValues[move.Target.y, move.Target.x] = this.Values[move.Start.y, move.Start.x];
            this.Values = (int[,])tempValues.Clone();
            MoveCount++;
            return ("[" + move.Start.x.ToString() + ", " + move.Start.y.ToString() + "] > [" + move.Target.x.ToString() + ", " + move.Target.y.ToString() + "]: Pawn ", true);
        }

        // - Treats the Move as a Move performed by any Black Figure
        private (string move, bool successful) BlackMove(Move move)
        {
            int[,] tempValues = (int[,])this.Values.Clone();
            if (Values[move.Target.y, move.Target.x] == 0)
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

        // - Sets the current Active Spot
        protected void SetActive(Point spot)
        {
            this.ActiveSpot = spot;
        }

        // - Calculates the Distance between two Points based on the Moves a Pawn would need
        protected int Distance(Point pos1, Point pos2)
        {
            int yDif = Math.Abs(pos2.y - pos1.x);
            int xDif = Math.Abs(pos2.x - pos1.x);
            return yDif + xDif;
        }

        // - Updates every single spot on the Grid that has changed compared to the last time the Method was called
        protected virtual void Update()
        {
            UpdateSize();
            if (LastValues == null)
            {
                ResetLastValues();
                Update();
            }
            else
            {
                for (int y = 0; y < Size.y; y++)
                {
                    for (int x = 0; x < Size.x; x++)
                    {
                        if (Values[y, x] != LastValues[y, x])
                        {
                            UpdateSingleSpot(new Point(y, x));
                        }
                    }
                }
            }
            LastValues = Values;

            UpdateBlackPosition();

            UpdateWhitePositions();

            UpdatePossibleWhiteMoves();

            UpdatePossibleBlackMoves();

            UpdateIdealPositions();

            UpdateHalfIdealBlackPositions();

            ActiveForm.pbGrid.Update();
        }

        // - Updates a single spot based on the input and the Values Array
        private void UpdateSingleSpot(Point spot)
        {
            Point PixelOffset = this.PixelOffset(spot);
            Point SingleSpotSize = this.SingleSpotSizePX;
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

        // - Updates PossibleBlackMoves
        private void UpdatePossibleBlackMoves()
        {
            int i = BlackPosition.y;
            PossibleBlackMoves = new List<Move>();
            while (AddBlackMoveIfPossible(new Point(i, BlackPosition.x))) { i++; }

            i = BlackPosition.y - 1;
            while (AddBlackMoveIfPossible(new Point(i, BlackPosition.x))) { i--; }

            i = BlackPosition.x;
            while (AddBlackMoveIfPossible(new Point(BlackPosition.y, i))) { i++; }

            i = BlackPosition.x - 1;
            while (AddBlackMoveIfPossible(new Point(BlackPosition.y, i))) { i++; }
        }

        // - Updates PossibleWhiteMoves
        private void UpdatePossibleWhiteMoves()
        {
            PossibleWhiteMoves = new List<Move>();
            for (int i = 0; i < WhitePositions.Count(); i++)
            {
                foreach (Point p in Neighbours(WhitePositions[i]))
                {
                    if (Values[p.y, p.x] != 1)
                    {
                        AddPossibleWhiteMoveIfNotDublicate(new Move(WhitePositions[i], p));
                    }
                }
            }
        }

        // - Updates IdealBlackPositions
        private void UpdateIdealPositions()
        {
            List<Point> total = new List<Point>();
            (int[] inY, int[] inX) empty = Empty();
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (empty.inY[y] == 8 && empty.inX[x] == 8)
                {
                    if (Values[y, Math.Min(x + 1, 7)] != 1 && Values[y, Math.Max(x - 1, 0)] != 1 && Values[Math.Min(y + 1, 7), x] != 1 && Values[Math.Max(y - 1, 0), x] != 1)
                    {
                        total.Add(new Point(y, x));
                    }
                }
            }
            IdealPositions = total;
        }

        // - Updates HalfIdealBlackPositions
        private void UpdateHalfIdealBlackPositions()
        {
            List<Point> total = new List<Point>();
            int[] emptyInX = new int[8];
            int[] emptyInY = new int[8];
            for (int i = 0; i < 64; i++)
            {
                int y = i / 8;
                int x = i % 8;
                if (this.Values[y, x] != 1)
                {
                    emptyInX[x]++;
                    emptyInY[y]++;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (emptyInX[i] == 8)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        total.Add(new Point(j, i));
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (emptyInY[i] == 8)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (emptyInX[j] < 8)
                        {
                            total.Add(new Point(j, i));
                        }
                    }
                }
            }
            HalfIdealPositions = total;
        }

        // - Adds a Move to the PossibleWhiteMove List as long as it's not already in there
        private void AddPossibleWhiteMoveIfNotDublicate(Move move)
        {
            bool duplicate = false;
            foreach (Move pmove in PossibleWhiteMoves)
            {
                if (move == pmove)
                {
                    duplicate = true;
                    break;
                }
            }
            if (!duplicate)
            {
                PossibleWhiteMoves.Add(move);
            }
        }

        // - When the given Target is viable, return true and add the related Move to the PossibleBlackMoves
        private bool AddBlackMoveIfPossible(Point target)
        {
            Move candidate = new Move(BlackPosition, target);
            if (candidate.IsOutsideGrid()) { return false; }
            if (this.Values[target.y, target.x] != 1)
            {
                PossibleBlackMoves.Add(candidate);
                return true;
            }
            else { return false; }
        }

        // - Calculates the Values to Translate the top left pixel of a spot to (0,0)
        private Point PixelOffset(Point point)
        {
            return new Point((this.SizePX.y / Size.y) * point.y, (this.SizePX.x / this.Size.x) * point.x);
        }

        // - Sets the Height and Width to the correct values
        private void UpdateSize()
        {
            Point Size = new Point(Values.GetLength(0), Values.GetLength(1));
            this.Size = Size;
        }

        // - Updates the current BlackPosition based on Values
        private void UpdateBlackPosition()
        {
            varBlackPosition = new Point(-1, -1);
            for (int i = 0; i < 64; i++)
            {
                if (this.Values[i / 8, i % 8] == 2)
                {
                    varBlackPosition = new Point(i / 8, i % 8);
                }
            }

        }

        // - Updates the current WhitePositions based on Values
        private void UpdateWhitePositions()
        {
            WhitePositions = new List<Point>();
            for (int i = 0; i < 64; i++)
            {
                if (this.Values[i / 8, i % 8] == 1)
                {
                    WhitePositions.Add(new Point(i / 8, i % 8));
                }
            }
        }

        // - Returns all the Neighbours of a certain Spot
        private List<Point> Neighbours(Point spot)
        {
            List<Point> neighbours = new List<Point>();
            if (spot.y < 7)
            {
                neighbours.Add(new Point(spot.y + 1, spot.x));
            }
            if (spot.y > 0)
            {
                neighbours.Add(new Point(spot.y - 1, spot.x));
            }
            if (spot.x < 7)
            {
                neighbours.Add(new Point(spot.y, spot.x + 1));
            }
            if (spot.x > 0)
            {
                neighbours.Add(new Point(spot.y, spot.x - 1));
            }
            return neighbours;
        }

        // - Any Move that starts at (-1,-1) and ends somewhere in the given Grid is considered a "First Move", It is also important that the Target is empty
        private bool IsFirstMove(Move move)
        {
            return move.Start.y == -1 && move.Start.x == -1 && move.Target.y >= 0 && move.Target.x >= 0 && move.Target.y < 8 && move.Target.x < 8;
        }

        // - When no Move has been done so far, the First Move can still be made ... 
        private bool FirstMoveAvailable()
        {
            return MoveCount == 0;
        }

        // - Sets every Spot in LastValues to -1
        private void ResetLastValues()
        {
            LastValues = new int[Size.y, Size.x];
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    LastValues[y, x] = -1;
                }
            }
        }

        // - Checks wether the Target of the given Move is empty in the current Grid
        private bool TargetSpotEmpty(Move move)
        {
            return this.Values[move.Target.y, move.Target.x] == 0;
        }

        // - When something is on the Path of the Move
        private bool PathBlocked(Move move)
        {
            if (move.Details.direction == "invalid") { return false; }
            if (move.Details.distance == 0) { return false; }
            int SignY = 0;
            int SignX = 0;
            if (move.Start.x > move.Target.x) { SignX = -1; }
            else if (move.Start.x < move.Target.x) { SignX = 1; }
            if (move.Start.y > move.Target.y) { SignY = -1; }
            else if (move.Start.y < move.Target.y) { SignY = 1; }
            for (int i = 0; i < move.Details.distance; i++)
            {
                if (this.Values[move.Start.y + SignY * i, move.Start.x + SignX * i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}