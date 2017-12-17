using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace BwInf
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string path = Path();           
            List<double[][]> triangles = GetTriangles(GetLines(path));     
            
            Console.WriteLine();
            ReturnTriangles(triangles, path);
            Console.WriteLine();
            Console.ReadKey();
        }
        // getting the path from the user with file dialogs
        private static string Path()
        {
            string path = "";
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                path = oFD.FileName;
            }
            Console.WriteLine("The following path was opened: " + path);
            Console.WriteLine();
            return path;
        }
        // getting the stuff we need to process from the given path
        private static List<Line> GetLines(string path)
        {
            StreamReader tempSR = new StreamReader(path);
            List<Line> total = new List<Line>();
            int count = Convert.ToInt32(tempSR.ReadLine());
            for (int i = 0; i < count; i++)
            {
                string line = tempSR.ReadLine();
                Console.WriteLine(line);
                string[] values = line.Split(' ');
                double x1 = Convert.ToDouble(values[0]);
                double y1 = Convert.ToDouble(values[1]);
                double x2 = Convert.ToDouble(values[2]);
                double y2 = Convert.ToDouble(values[3]);
                total.Add(new Line(x1, y1, x2, y2));
            }
            tempSR.Dispose();
            return total;
        }
        // finding all the triangles in the given data
        private static List<double[][]> GetTriangles (List<Line> lines)
        {
            // a list of triangles (double[][] is for 3 points with 2 coordinates each)
            List<double[][]> triangles = new List<double[][]>();
            // we are cycling through all the possiblities of combining three different lines in no particular order
            for (int i = 0; i < lines.Count() - 2; i++)
            {
                for (int j = i + 1; j < lines.Count() - 1; j++)
                {
                    for (int k = j + 1; k < lines.Count(); k++)
                    {
                        // we store our combination for easy access
                        Line[] currentLines = new Line[3] { lines[i], lines[j], lines[k] };
                        // now we need the intersections of those lines
                        double[][] currentSharedPoints = new double[3][]
                        {
                            currentLines[0].SingleSharedPoint(currentLines[1]),
                            currentLines[0].SingleSharedPoint(currentLines[2]),
                            currentLines[1].SingleSharedPoint(currentLines[2])
                        };
                        // if a point of those three above has a number of coordinates unequal to two, we know something is wrong
                        bool validTriangle = true;
                        foreach (double[] d in currentSharedPoints)
                        {
                            if (d.Count() != 2)
                            {
                                validTriangle = false;
                            }
                        }
                        // if nothing is wrong and none of the two corners have identical coordinates, we can finally at the triangle to the total
                        if (validTriangle && TriangleHasCorners(currentSharedPoints))
                        {
                            double[][] triangle = new double[3][]
                            {
                                new double[2] { currentSharedPoints[0][0], currentSharedPoints[0][1] },
                                new double[2] { currentSharedPoints[1][0], currentSharedPoints[1][1] },
                                new double[2] { currentSharedPoints[2][0], currentSharedPoints[2][1] }
                            };
                            triangles.Add(triangle);

                        }
                    }
                }
            }
            return triangles;
        }

        public static void ReturnTriangles(List<double[][]> triangles)
        {
            Console.WriteLine(triangles.Count());
            foreach (double[][] triangle in triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(triangle[i][0] + " " + triangle[i][1] + " ");
                }
                Console.Write("\n");
            }
        }
        public static void ReturnTriangles(List<double[][]> triangles, string path)
        {
            ReturnTriangles(triangles);
            string[] splitPath = path.Split('.');
            splitPath[splitPath.Count() - 2] += "-output";
            path = "";
            for (int i = 0; i < splitPath.Count(); i++)
            {
                if (i > 0)
                {
                    path += ".";
                }
                path += splitPath[i];
            }

            StreamWriter tempSW = new StreamWriter(path);
            tempSW.WriteLine(triangles.Count());
            foreach (double[][] triangle in triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    tempSW.Write(triangle[i][0] + " " + triangle[i][1] + " ");
                }
                tempSW.Write("\n");
            }
            tempSW.Dispose();
            Console.WriteLine();
            Console.WriteLine("The results were saved in " + path);
        }

        // - true if none of the points are identical
        private static bool TriangleHasCorners(double[][] sharedPoints)
        {
            if (sharedPoints[0][0] == sharedPoints[1][0] && sharedPoints[0][1] == sharedPoints[1][1]) { return false; }
            if (sharedPoints[0][0] == sharedPoints[2][0] && sharedPoints[0][1] == sharedPoints[2][1]) { return false; }
            if (sharedPoints[1][0] == sharedPoints[2][0] && sharedPoints[1][1] == sharedPoints[2][1]) { return false; }
            return true;
        }
    }
}
