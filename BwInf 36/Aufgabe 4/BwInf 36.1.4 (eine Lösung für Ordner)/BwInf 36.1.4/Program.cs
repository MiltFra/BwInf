using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace BwInf_36._1._4
{
    class Program
    {
        static List<string> database = new List<string>();
        static List<string> words = new List<string>();
        static List<List<string[]>> results = new List<List<string[]>>();
        [STAThread]
        static void Main(string[] args)
        {
            string databasePath = "";
            if (databasePath == "")
            {
                OpenFileDialog OFD = new OpenFileDialog();
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    databasePath = OFD.FileName;
                }
            }
            database = readFile(databasePath);
            Console.WriteLine("The following DataBase was opened: " + databasePath);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string directoryPath = "";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                directoryPath = fbd.SelectedPath;
            }
            Console.WriteLine("The following Directory was opened: " + directoryPath);
            List<string> files = getFiles(directoryPath);
            while (files.Count() > 0)
            {
                results = new List<List<string[]>>();
                string tempPath = files[0];
                Console.WriteLine();
                Console.WriteLine("The following WordFile was opened: " + tempPath);
                files.RemoveAt(0);
                words = readFile(tempPath);
                Console.WriteLine("");
                int lastUpdate = 0;
                for (int i = 0; i < words.Count(); i++)
                {
                    results.Add(getMultipleSolution(words[i]));
                    double doubleProgress = (Convert.ToDouble(i) * 100) / Convert.ToDouble(words.Count());
                    int currentProgress = Convert.ToInt32(doubleProgress);
                    if (currentProgress > lastUpdate + 9)
                    {
                        lastUpdate = currentProgress;
                        Console.WriteLine("{0} % completed ...", lastUpdate.ToString("00").PadLeft(3));
                    }
                }
                string fileName = tempPath.Split('\\')[tempPath.Split('\\').Count() - 1];
                string newPath = directoryPath + "\\output";
                string[] pathSplit = tempPath.Split('.');
                returnResults(newPath, fileName);
                Console.WriteLine("The output was saved to: " + newPath);
            }
            Console.Write("\nTask completed. Press any key to exit ...");
            Console.ReadKey();
        }
        static List<string> getFiles(string directory)
        {
            string[] stringArray = Directory.GetFiles(directory, "*.txt");
            List<string> files = new List<string>();
            foreach (string s in stringArray)
            {
                files.Add(s);
            }
            return files;
        }
        static List<string[]> getMultipleSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            char[] wordChar = word.ToCharArray();
            int length = wordChar.Count();
            if (length < 3)
            {
                return getSingleSolution(word);
            }
            else
            {
                List<string[]>[] solutionsAt = new List<string[]>[length];
                string wordAtCurrentLength = "";
                for (int currentLength = 0; currentLength < length; currentLength++)
                {
                    solutionsAt[currentLength] = new List<string[]>();
                    wordAtCurrentLength += wordChar[currentLength];
                    solutionsAt[currentLength].AddRange(getSingleSolution(wordAtCurrentLength));
                    if (currentLength > 2)
                    {
                        for (int currentSegmentLength = 2; currentSegmentLength < 6; currentSegmentLength++)
                        {
                            if (currentLength - currentSegmentLength < 1) { }
                            else if (solutionsAt[currentLength - currentSegmentLength].Count() == 0) { }
                            else
                            {
                                string currentSegmentWord = "";
                                for (int i = currentLength - currentSegmentLength + 1; i <= currentLength; i++)
                                {
                                    currentSegmentWord += wordChar[i];
                                }
                                List<string[]> solutionsForCurrentSegment = getSingleSolution(currentSegmentWord);
                                if (solutionsAt[currentLength - currentSegmentLength].Count() > 0)
                                {
                                    if (solutionsForCurrentSegment.Count() > 0)
                                    {
                                        if (solutionsAt[currentLength].Count() == 0)
                                        {
                                            solutionsAt[currentLength].Add(mergeStringArrays(solutionsAt[currentLength - currentSegmentLength][0], solutionsForCurrentSegment[0]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                solutions.AddRange(solutionsAt[length - 1]);
            }
            return solutions;
        }
        static List<string[]> getMultipleSolutions(string word)
        {
            List<string[]> solutions = new List<string[]>();
            char[] wordChar = word.ToCharArray();
            int length = wordChar.Count();
            if (length < 3)
            {
                return getSingleSolutions(word);
            }
            else
            {
                List<string[]>[] solutionsAt = new List<string[]>[length];
                string wordAtCurrentLength = "";
                for (int currentLength = 0; currentLength < length; currentLength++)
                {
                    solutionsAt[currentLength] = new List<string[]>();
                    wordAtCurrentLength += wordChar[currentLength];
                    solutionsAt[currentLength].AddRange(getSingleSolutions(wordAtCurrentLength));
                    if (currentLength > 2)
                    {
                        for (int currentSegmentLength = 2; currentSegmentLength < 6; currentSegmentLength++)
                        {
                            if (currentLength - currentSegmentLength < 1) { }
                            else if (solutionsAt[currentLength - currentSegmentLength].Count() == 0) { }
                            else
                            {
                                string currentSegmentWord = "";
                                for (int i = currentLength - currentSegmentLength + 1; i <= currentLength; i++)
                                {
                                    currentSegmentWord += wordChar[i];
                                }
                                List<string[]> solutionsForCurrentSegment = getSingleSolutions(currentSegmentWord);
                                foreach (string[] solutionAtSegmentStart in solutionsAt[currentLength - currentSegmentLength])
                                {
                                    foreach (string[] solutionForCurrentSegment in solutionsForCurrentSegment)
                                    {
                                        solutionsAt[currentLength].Add(mergeStringArrays(solutionAtSegmentStart, solutionForCurrentSegment));
                                    }
                                }
                            }
                        }
                    }
                }
                solutions.AddRange(solutionsAt[length - 1]);
            }
            return solutions;
        }
        static List<string[]> getSingleSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            char[] wordChar = word.ToCharArray();
            int length = wordChar.Count();
            if (length > 1 && length < 6)
            {
                List<string> possibleStarts = new List<string>();
                foreach (string data in database)
                {
                    char[] dataChar = data.ToCharArray();
                    bool identicalStart = true;
                    for (int i = 0; i < wordChar.Count() && i < dataChar.Count(); i++)
                    {
                        if (!(wordChar[i] == dataChar[i]))
                        {
                            identicalStart = false;
                            break;
                        }
                    }
                    if (identicalStart)
                    {
                        possibleStarts.Add(data);
                    }
                }
                foreach (string possibleStart in possibleStarts)
                {
                    char[] dataChar = possibleStart.ToCharArray();
                    int restLength = length - dataChar.Count();
                    if (restLength > 0 && restLength < 3)
                    {
                        string rest = "";
                        for (int i = length - restLength; i < length; i++)
                        {
                            rest += wordChar[i];
                        }
                        string[] solution = new string[2] { possibleStart, rest };
                        solutions.Add(solution);
                        return solutions;
                    }
                }
            }
            return solutions;
        }
        static List<string[]> getSingleSolutions(string word)
        {
            List<string[]> solutions = new List<string[]>();
            char[] wordChar = word.ToCharArray();
            int length = wordChar.Count();
            if (length > 1 && length < 6)
            {
                List<string> possibleStarts = new List<string>();
                foreach (string data in database)
                {
                    char[] dataChar = data.ToCharArray();
                    bool identicalStart = true;
                    for (int i = 0; i < wordChar.Count() && i < dataChar.Count(); i++)
                    {
                        if (!(wordChar[i] == dataChar[i]))
                        {
                            identicalStart = false;
                            break;
                        }
                    }
                    if (identicalStart)
                    {
                        possibleStarts.Add(data);
                    }
                }
                foreach (string possibleStart in possibleStarts)
                {
                    char[] dataChar = possibleStart.ToCharArray();
                    int restLength = length - dataChar.Count();
                    if (restLength > 0 && restLength < 3)
                    {
                        string rest = "";
                        for (int i = length - restLength; i < length; i++)
                        {
                            rest += wordChar[i];
                        }
                        string[] solution = new string[2] { possibleStart, rest };
                        solutions.Add(solution);
                    }
                }
            }
            return solutions;
        }
        static string[] mergeStringArrays(string[] array1, string[] array2)
        {
            string[] mergedStringArray = new string[array1.Count() + array2.Count()];
            for (int i = 0; i < array1.Count(); i++)
            {
                mergedStringArray[i] = array1[i];
            }
            for (int i = array1.Count(); i < mergedStringArray.Count(); i++)
            {
                mergedStringArray[i] = array2[i - array1.Count()];
            }
            return mergedStringArray;
        }
        private static List<string> readFile(string path)
        {
            List<string> file = new List<string>();
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string word = "";
                foreach (char c in sr.ReadLine().ToCharArray())
                {
                    if (char.IsLetter(c))
                    {
                        word += c.ToString().ToUpper();
                    }
                }
                if (word != "")
                {
                    file.Add(word);
                }
            }

            return file;
        }
        private static void returnResults(string directory, string fileName)
        {
            Directory.CreateDirectory(directory);
            StreamWriter sw = new StreamWriter(directory + "\\" + fileName);
            int currentWord = 0;
            foreach (List<string[]> result in results)
            {
                string word = words[currentWord];
                sw.Write(word + ": ");
                if (result.Count() > 0)
                {
                    foreach (string[] s in result)
                    {
                        for (int i = 0; i < s.Count(); i++)
                        {
                            if (i % 2 == 0)
                            {
                                sw.Write("[ " + s[i] + " - ");
                            }
                            else
                            {
                                sw.Write(s[i] + " ]");
                            }

                        }
                        sw.Write("\n");
                    }
                }
                else
                {
                    sw.WriteLine("<empty>");
                }
                sw.WriteLine("");
                currentWord++;
            }
            sw.Dispose();
        }
        private static void returnResults()
        {
            int currentWord = 0;
            foreach (List<string[]> result in results)
            {
                string word = words[currentWord];
                Console.WriteLine(word + ", [" + result.Count().ToString() + "]: ");
                if (result.Count() > 0)
                {
                    foreach (string[] s in result)
                    {
                        for (int i = 0; i < s.Count(); i++)
                        {
                            if (i % 2 == 0)
                            {
                                Console.Write("[ " + s[i].PadLeft(3) + " - ");
                            }
                            else
                            {
                                Console.Write(s[i].PadLeft(2) + " ]");
                            }

                        }
                        Console.Write("\n");
                    }
                }
                else
                {
                    Console.WriteLine("<empty>");
                }
                Console.WriteLine("");
                currentWord++;
            }
        }
    }
}
