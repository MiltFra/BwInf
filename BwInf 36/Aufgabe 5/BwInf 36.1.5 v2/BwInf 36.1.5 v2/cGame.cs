using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BwInf_36._1._5_v2
{
    public class cGame
    {
        private List<PictureBox> whitePictures = new List<PictureBox>();
        private PictureBox blackPictures = new PictureBox();
        private Bitmap whiteFigure = new Bitmap(80, 80);
        private Bitmap blackFigure = new Bitmap(80, 80);
        public int whiteMoves { get; set; }
        public List<cPawn> whiteSide { get; set; }
        public cFigure blackSide { get; set; }
        public List<int[]> getOptimalRookPositions()
        {
            List<int[]> total = new List<int[]>();
            List<int> columns = new List<int>();
            List<int> rows = new List<int>();
            for (int x = 0; x < 8; x++)
            {
                bool isOptimal = true;
                foreach (cPawn pawn in whiteSide)
                {
                    if (pawn.x == x)
                    {
                        isOptimal = false;
                        break;
                    }
                }
                if (blackSide.x == x)
                {
                    isOptimal = false;
                }
                if (isOptimal)
                {
                    columns.Add(x);
                }
            }
            for (int y = 0; y < 8; y++)
            {
                bool isOptimal = true;
                foreach (cPawn pawn in whiteSide)
                {
                    if (pawn.y == y)
                    {
                        isOptimal = false;
                        break;
                    }
                }
                if (blackSide.y == y)
                {
                    isOptimal = false;
                }
                if (isOptimal)
                {
                    rows.Add(y);
                }
            }
            foreach (int x in columns)
            {
                foreach (int y in rows)
                {
                    int[] position = new int[2] { x, y };
                    total.Add(position);
                }
            }
            return total;
        }
        public List<int[]> getOptimalQueenPositions()
        {
            throw new NotImplementedException();
        }
        public cMove getOptimalBlackMove()
        {
            if (blackSide.GetType() == typeof(cRook))
            {
                bool isOptimal = false;
                List<int[]> optimalRookPositions = getOptimalRookPositions();
                foreach (int[] position in optimalRookPositions)
                {
                    if (position[0] == blackSide.x && position[1] == blackSide.y)
                    {
                        isOptimal = true;
                        break;
                    }
                }
                if (isOptimal)
                {
                    return new cMove(blackSide.x, blackSide.y, blackSide.x, blackSide.y);
                }
                else
                {
                    foreach (int[] position in optimalRookPositions)
                    {
                        cMove tempMove = new cMove(blackSide.x, blackSide.y, position[0], position[1]);
                        double tempDistance = tempMove.distance();
                        if (tempDistance >= blackSide.minimumDistance && tempDistance < blackSide.maximumDistance)
                        {
                            int tempDirection = tempMove.direction();
                            if (tempDirection >= blackSide.minimumDirection && tempDirection < blackSide.maximumDirection)
                            {
                                return tempMove;
                            }
                        }
                    }
                    return new cMove(blackSide.x, blackSide.y, blackSide.x, blackSide.y);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public List<cMove> getOptimalWhiteMoves()
        {
            cGame simulation = this;
            List<cMove> total = new List<cMove>();
            int whiteMoves = this.whiteMoves;
            List<int[]> optimalRookPositions = getOptimalRookPositions();
            List<int>[] pawnsInRange = new List<int>[optimalRookPositions.Count()];
            for (int i = 0; i < optimalRookPositions.Count(); i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < whiteSide.Count(); j++)
                {
                    if (whiteSide[j].y == optimalRookPositions[i][1])
                    {
                        if (whiteSide[j].x == optimalRookPositions[i][0] + 1 || whiteSide[j].x == optimalRookPositions[i][0] - 1)
                        {
                            pawnsInRange[i].Add(j);
                        }
                    }
                    else if (whiteSide[j].y == optimalRookPositions[i][1] + 1 || whiteSide[j].y == optimalRookPositions[i][1] - 1)
                    {
                        if (whiteSide[j].x == optimalRookPositions[i][0])
                        {
                            pawnsInRange[i].Add(j);
                        }
                    }
                }
            }
            while (optimalRookPositions.Count() > 0)
            {
                int minimalCount = 0;
                for (int i = 0; i < pawnsInRange.Count(); i++)
                {
                    if (pawnsInRange[i].Count() < pawnsInRange[minimalCount].Count())
                    {
                        minimalCount = i;
                    }
                }
                int xOfPawnInRange = whiteSide[pawnsInRange[minimalCount][0]].x;
                int yOfPawnInRange = whiteSide[pawnsInRange[minimalCount][0]].y;
                int xOfCurrentOptimalRookPosition = optimalRookPositions[minimalCount][0];
                int yOfCurrentOptimalRookPosition = optimalRookPositions[minimalCount][1];
                cMove move = new cMove(xOfPawnInRange, yOfPawnInRange, xOfCurrentOptimalRookPosition, yOfCurrentOptimalRookPosition);
                total.Add(move);
                optimalRookPositions.RemoveAt(minimalCount);
                whiteMoves--;
            }
            for (int i = 0; i < whiteMoves; i++)
            {
                int furthesAway = 0;
                checkIfAlreadyMoved:
                if (furthesAway < simulation.whiteSide.Count())
                {
                    bool notMovedYet = true;
                    foreach (cMove move in total)
                    {
                        if (move.originX == simulation.whiteSide[furthesAway].x && move.originY == simulation.whiteSide[furthesAway].y)
                        {
                            notMovedYet = false;
                        }
                    }
                    if (notMovedYet == false)
                    {
                        furthesAway++;
                        goto checkIfAlreadyMoved;
                    }
                }
                else
                {
                    return total;
                }
                for (int j = 0; j < simulation.whiteSide.Count(); j++)
                {
                    if (Math.Abs(simulation.blackSide.y - simulation.whiteSide[furthesAway].y) > Math.Abs(simulation.blackSide.y - simulation.whiteSide[j].y))
                    {
                        bool notMovedYet2 = true;
                        foreach (cMove move in total)
                        {
                            if (move.originX == simulation.whiteSide[j].x && move.originY == simulation.whiteSide[j].y)
                            {
                                notMovedYet2 = false;
                            }
                        }
                        if (notMovedYet2)
                        {
                            furthesAway = j;
                        }
                    }
                }
                int originX = simulation.whiteSide[furthesAway].x;
                int originY = simulation.whiteSide[furthesAway].y;
                int targetX = originX;
                int targetY = originY + 1;
                cMove pawnMove = new cMove(originX, originY, targetX, targetY);
                total.Add(pawnMove);
                foreach (cPawn pawn in simulation.whiteSide)
                {
                    if (pawn.x == pawnMove.originX && pawn.y == pawnMove.originY)
                    {
                        if (pawnMove.targetX < 8 && pawnMove.targetY < 8)
                        {
                            pawn.move(pawnMove);
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            return total;
        }
        public void render(Form1 form)
        {
            for (int i = 0; i < whiteSide.Count(); i++)
            {
                PictureBox pb = new PictureBox();
                pb.Image = this.whiteFigure;
                pb.
                whitePictures[i].
            }

        }
        public cGame()
        {
            this.blackSide = new cRook(0, 0);
            this.whiteSide = new List<cPawn>();
            for (int i = 0; i < 6400; i++)
            {
                int x = i % 80;
                int y = i / 80;
                this.whiteFigure.SetPixel(x, y, Color.White);
                this.blackFigure.SetPixel(x, y, Color.Black);
            }
        }
        public cGame(cFigure blackSide, List<cPawn> whiteSide)
        {
            this.blackSide = blackSide;
            this.whiteSide = whiteSide;
        }
    }
}
