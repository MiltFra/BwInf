using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace BwInf_36._1._3
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string path = "";
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                path = oFD.FileName;
            }
            Console.WriteLine("The following path was opened: " + path);
            Console.WriteLine();
            List<cLine> rawLines = getLines(path);
            List<double[][]> triangles = new List<double[][]>();
            for (int i = 0; i < rawLines.Count() - 2; i++)
            {
                for (int j = i + 1; j < rawLines.Count() - 1; j++)
                {
                    for (int k = j + 1; k < rawLines.Count(); k++)
                    {
                        cLine[] currentLines = new cLine[3] { rawLines[i], rawLines[j], rawLines[k] };
                        double[][] currentSharedPoints = new double[3][]
                        {
                            currentLines[0].singleSharedPoint(currentLines[1]),
                            currentLines[0].singleSharedPoint(currentLines[2]),
                            currentLines[1].singleSharedPoint(currentLines[2])
                        };
                        bool validTriangle = true;
                        foreach (double[] d in currentSharedPoints)
                        {
                            if (d.Count() != 2)
                            {
                                validTriangle = false;
                            }
                        }
                        
                        if (validTriangle)
                        {
                            if (currentSharedPoints[0][0] == currentSharedPoints[1][0] && currentSharedPoints[0][1] == currentSharedPoints[1][1]) { validTriangle = false; }
                            if (currentSharedPoints[0][0] == currentSharedPoints[2][0] && currentSharedPoints[0][1] == currentSharedPoints[2][1]) { validTriangle = false; }
                            if (currentSharedPoints[1][0] == currentSharedPoints[2][0] && currentSharedPoints[1][1] == currentSharedPoints[2][1]) { validTriangle = false; }
                            if (validTriangle)
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
            }
            Console.WriteLine();
            returnTriangles(triangles, path);
            Console.WriteLine();
            Console.ReadKey();

        }
        public static List<cLine> getLines(string path)
        {
            StreamReader tempSR = new StreamReader(path);
            List<cLine> total = new List<cLine>();
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
                total.Add(new cLine(x1, y1, x2, y2));
            }
            tempSR.Dispose();
            return total;
        }
        public static void returnTriangles(List<double[][]> triangles)
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
        public static void returnTriangles(List<double[][]> triangles, string path)
        {
            returnTriangles(triangles);
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
    }
}
