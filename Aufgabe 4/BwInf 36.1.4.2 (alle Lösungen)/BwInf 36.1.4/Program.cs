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
            string tempPath = InputPath();
            Console.WriteLine("");
            int lastUpdate = 0;

            for (int i = 0; i < words.Count(); i++)
            {
                results.Add(EverySolution(words[i]));
                lastUpdate = UpdateProgress(i, lastUpdate);
            }

            string returnPath = ReturnPath(tempPath);
            ReturnResults(returnPath);

            Console.WriteLine("The output was saved to: " + returnPath);
            Console.ReadKey();
        }

        static List<string[]> FirstSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            char[] wordChar = word.ToCharArray();
            int length = wordChar.Count();
            if (length < 3)
            {
                return FirstShortSolution(word);
            }
            else
            {
                List<string[]>[] solutionsAt = new List<string[]>[length];
                string wordAtCurrentLength = "";
                for (int currentLength = 0; currentLength < length; currentLength++)
                {
                    solutionsAt[currentLength] = new List<string[]>();
                    wordAtCurrentLength += wordChar[currentLength];
                    solutionsAt[currentLength].AddRange(FirstShortSolution(wordAtCurrentLength));

                    solutionsAt = UpdateFirstSolutionAtLength(solutionsAt, currentLength, word);
                }
                return solutionsAt[length - 1];
            }
        }
        static List<string[]> EverySolution(string word)
        {
            List<string[]> solutions = new List<string[]>();

            if (word.Length < 3) { return EveryShortSolution(word); }

            List<string[]>[] solutionsAt = new List<string[]>[word.Length];
            string wordAtCurrentLength = "";

            for (int currentLength = 0; currentLength < word.Length; currentLength++)
            {
                solutionsAt[currentLength] = new List<string[]>();
                wordAtCurrentLength += word[currentLength];
                solutionsAt[currentLength].AddRange(EveryShortSolution(wordAtCurrentLength));

                solutionsAt = UpdateEverySolutionAtLength(solutionsAt, currentLength, word);
            }
            return solutionsAt[word.Length - 1];
        }

        static List<string[]>[] UpdateEverySolutionAtLength(List<string[]>[] solutionsAt, int length, string word)
        {
            if (length > 2)
            {
                for (int currentSegmentLength = 2; currentSegmentLength < 6; currentSegmentLength++)
                {
                    if (UnfittingSegmentLength(solutionsAt, currentSegmentLength, length)) { }
                    else
                    {
                        string currentSegmentWord = "";
                        for (int i = length - currentSegmentLength + 1; i <= length; i++)
                        {
                            currentSegmentWord += word[i];
                        }
                        List<string[]> solutionsForCurrentSegment = EveryShortSolution(currentSegmentWord);
                        foreach (string[] solutionAtSegmentStart in solutionsAt[length - currentSegmentLength])
                        {
                            foreach (string[] solutionForCurrentSegment in solutionsForCurrentSegment)
                            {
                                solutionsAt[length].Add(mergeStringArrays(solutionAtSegmentStart, solutionForCurrentSegment));
                            }
                        }
                    }
                }
            }
            return solutionsAt;
        }
        static List<string[]>[] UpdateFirstSolutionAtLength(List<string[]>[] solutionsAt, int length, string word)
        {
            if (length > 2)
            {
                for (int currentSegmentLength = 2; currentSegmentLength < 6; currentSegmentLength++)
                {
                    if (solutionsAt[length].Count() > 0) { break; }
                    else if (UnfittingSegmentLength(solutionsAt, currentSegmentLength, length)) { }
                    else
                    {
                        string currentSegmentWord = "";
                        for (int i = length - currentSegmentLength + 1; i <= length; i++)
                        {
                            currentSegmentWord += word[i];
                        }
                        List<string[]> solutionsForCurrentSegment = FirstShortSolution(currentSegmentWord);
                        if (solutionsAt[length - currentSegmentLength].Count() > 0)
                        {
                            if (solutionsForCurrentSegment.Count() > 0)
                            {
                                if (solutionsAt[length].Count() == 0)
                                {
                                    solutionsAt[length].Add(mergeStringArrays(solutionsAt[length - currentSegmentLength][0], solutionsForCurrentSegment[0]));
                                }
                            }
                        }
                    }
                }
            }
            return solutionsAt;
        }

        static List<string[]> FirstShortSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            if (word.Length > 1 && word.Length < 6)
            {
                List<string> possibleStarts = new List<string>();
                foreach (string data in database)
                {
                    char[] dataChar = data.ToCharArray();
                    bool identicalStart = true;
                    for (int i = 0; i < word.Length && i < dataChar.Count(); i++)
                    {
                        if (!(word[i] == dataChar[i]))
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
                    string rest = Rest(word, possibleStart);
                    if (rest.Length > 0 && rest.Length < 3)
                    {
                        string[] solution = new string[2] { possibleStart, rest };
                        solutions.Add(solution);
                        return solutions;
                    }
                }
            }
            return solutions;
        }
        static List<string[]> EveryShortSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            if (word.Length > 1 && word.Length < 6)
            {
                List<string> possibleStarts = new List<string>();
                foreach (string data in database)
                {
                    char[] dataChar = data.ToCharArray();
                    bool identicalStart = true;
                    for (int i = 0; i < word.Length && i < dataChar.Count(); i++)
                    {
                        if (!(word[i] == dataChar[i]))
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
                    string rest = Rest(word, possibleStart);
                    if (rest.Length > 0 && rest.Length < 3)
                    {
                        string[] solution = new string[2] { possibleStart, rest };
                        solutions.Add(solution);
                    }
                }
            }
            return solutions;
        }

        private static string Rest(string word, string start)
        {
            string total = "";
            for (int i = start.Length; i < word.Length; i++)
            {
                char currentChar = word[i];
                switch (currentChar)
                {
                    case 'Ü':
                        total += "UE";
                        break;
                    case 'Ä':
                        total += "AE";
                        break;
                    case 'Ö':
                        total += "OE";
                        break;
                    case 'ß':
                        total += "SS";
                        break;
                    default:
                        total += currentChar;
                        break;
                }
            }
            return total;
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
                    if (char.IsLetter(c.ToString().ToUpper().ToCharArray()[0]))
                    {
                        word += c.ToString().ToUpper();
                    }
                }
                file.Add(word);
            }
            return file;
        }
        private static string ReturnPath (string oldPath)
        {
            string newPath = "";
            string[] pathSplit = oldPath.Split('.');
            for (int i = 0; i < pathSplit.Count(); i++)
            {
                if (i > 0)
                {
                    newPath += ".";
                }
                newPath += pathSplit[i];
                if (i == pathSplit.Count() - 2)
                {
                    newPath += "-output";
                }
            }
            return newPath;
        }

        private static int UpdateProgress(int i, int lastUpdate)
        {
            double doubleProgress = (Convert.ToDouble(i) * 100) / Convert.ToDouble(words.Count());
            int currentProgress = Convert.ToInt32(doubleProgress);
            if (currentProgress > lastUpdate + 9)
            {
                lastUpdate = currentProgress;
                Console.WriteLine("{0} % completed ...", lastUpdate.ToString("00").PadLeft(3));
            }
            return lastUpdate;
        }

        private static string InputPath ()
        {
            string tempPath = ""; // Enter your custom database path here so you needn't enter it every time
            if (tempPath == "")
            {
                OpenFileDialog OFD = new OpenFileDialog();
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    tempPath = OFD.FileName;
                }
            }
            database = readFile(tempPath);
            Console.WriteLine("The following DataBase was opened: " + tempPath);
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                tempPath = oFD.FileName;
            }
            Console.WriteLine("The following WordFile was opened: " + tempPath);
            words = readFile(tempPath);
            return tempPath;
        }

        private static void ReturnResults(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            int currentWord = 0;
            foreach (List<string[]> result in results)
            {
                string word = words[currentWord];
                Console.WriteLine(word + " [" + result.Count().ToString() + "]:");
                sw.WriteLine(word + " [" + result.Count().ToString() + "]:");
                if (result.Count() > 0)
                {
                    foreach (string[] s in result)
                    {
                        for (int i = 0; i < s.Count(); i++)
                        {
                            if (i % 2 == 0)
                            {
                                Console.Write("[" + s[i].PadLeft(3) + "-");
                                sw.Write("[" + s[i].PadLeft(3) + "-");
                            }
                            else
                            {
                                Console.Write(s[i].PadLeft(2) + "]");
                                sw.Write(s[i].PadLeft(2) + "]");
                            }

                        }
                        Console.Write("\n");
                        sw.Write("\n");
                    }
                }
                else
                {
                    Console.WriteLine("<empty>");
                    sw.WriteLine("<empty>");
                }
                Console.WriteLine("");
                sw.WriteLine("");
                currentWord++;
            }
            sw.Dispose();
        }

        private static bool UnfittingSegmentLength(List<string[]>[] solutionsAt, int segment, int word)
        {
            return word < segment || solutionsAt[word - segment].Count() == 0;
        }
    }
}
