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
            // at first we need a path and get the data from it
            string tempPath = InputPath();
            Console.WriteLine("");
            int lastUpdate = 0;
            // then we need to process every word; 
            // meanwhile we have a nice progress display, but that's not important for the problem solving
            for (int i = 0; i < words.Count(); i++)
            {
                results.Add(FirstSolution(words[i]));
                lastUpdate = UpdateProgress(i, lastUpdate);
            }
            // now we convert the input path to an output path 
            string returnPath = ReturnPath(tempPath);
            // so we can return that stuff to a file and the console stream
            ReturnResults(returnPath);

            Console.WriteLine("The output was saved to: " + returnPath);
            Console.ReadKey();
        }
        // tries to find a solution, cancels once the first is found and only calculates one solution per part
        static List<string[]> FirstSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            char[] wordChar = word.ToCharArray();
            int length = wordChar.Count();
            // if the word is short enough, only one license plate leads to a solution
            if (length < 3)
            {
                return FirstShortSolution(word);
            }
            // else we might have to do some work
            else
            {
                // we need an array with all the solutions for every length now, in this case we will only try to get one per index
                List<string[]>[] solutionsAt = new List<string[]>[length];
                string wordAtCurrentLength = "";
                // now we cycle through the array, step by step
                for (int currentLength = 0; currentLength < length; currentLength++)
                {
                    // create a new solutions list, so there's something to add solutions to
                    solutionsAt[currentLength] = new List<string[]>();
                    // extends the current word by one character
                    wordAtCurrentLength += wordChar[currentLength];    
                    // add all the possible first solutions (likely none)
                    solutionsAt[currentLength].AddRange(FirstShortSolution(wordAtCurrentLength));
                    // add all the other possible solutions
                    solutionsAt = UpdateFirstSolutionAtLength(solutionsAt, currentLength, word);
                }
                // return the solution(s) at max length
                return solutionsAt[length - 1];
            }
        }
        // tries to find a solution, doesn't cancel 'til every single one is found
        // same procedure like in the method before, i won't explain that again
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
        // updates the solutions at array for a certain length
        static List<string[]>[] UpdateEverySolutionAtLength(List<string[]>[] solutionsAt, int length, string word)
        {
            // that would be awkward
            if (length > 2)
            {
                // cycling through all the segment lengths [idea: current length = n; segment length = s; were we need a solution = n - s;]
                for (int currentSegmentLength = 2; currentSegmentLength < 6; currentSegmentLength++)
                {
                    // true if there is no fitting counter part or the index would be zero
                    if (UnfittingSegmentLength(solutionsAt, currentSegmentLength, length)) { }
                    else
                    {
                        // find the word that is to be solved (namely the segment)
                        string currentSegmentWord = "";
                        for (int i = length - currentSegmentLength + 1; i <= length; i++)
                        {
                            currentSegmentWord += word[i];
                        }
                        // get the short solutions
                        List<string[]> solutionsForCurrentSegment = EveryShortSolution(currentSegmentWord);
                        // combine all the short solutions with all the fitting long ones
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
            // return the updated array
            return solutionsAt;
        }
        // same as every solution at length
        // just terminates early
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
        // finds the first short solution
        static List<string[]> FirstShortSolution(string word)
        {
            List<string[]> solutions = new List<string[]>();
            // if the word is too short or too long, this won't work
            if (word.Length > 1 && word.Length < 6)
            {
                // tries to find all the starts from the database (matching the beginning of the word)
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
                // tries to fill in the rest, if that's possible the combination is added to the solutions for the current word
                foreach (string possibleStart in possibleStarts)
                {
                    string rest = Rest(word, possibleStart);
                    if (rest.Length > 0 && rest.Length < 3)
                    {
                        string[] solution = new string[2] { possibleStart, rest };
                        solutions.Add(solution);
                        // if a solution is found, the method can retrun 'cause just one is needed
                        return solutions;
                    }
                }
            }
            return solutions;
        }
        // same procedure, doesn't cancel early
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
            // finds the rest when given a word and a start
            string total = "";
            // returns an invalid rest (just empty) if there is an invalid letter
            for (int i = start.Length; i < word.Length; i++)
            {
                char currentChar = word[i];
                if (currentChar == 'Ü' || currentChar == 'Ä' || currentChar == 'Ö') { return ""; }
                else { total += currentChar; }
            }
            return total;
        }
        // just putting an array at the end of another
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
        private static string ReturnPath(string oldPath)
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
        private static string InputPath()
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
            Console.Clear();
            StreamWriter sw = new StreamWriter(path);
            int currentWord = 0;
            foreach (List<string[]> result in results)
            {
                string word = words[currentWord];
                Console.WriteLine(word + ":");
                sw.WriteLine(word+":");
                if (result.Count() > 0)
                {
                    foreach (string[] s in result)
                    {
                        for (int i = 0; i < s.Count(); i++)
                        {
                            if (i % 2 == 0)
                            {
                                Console.Write("["+ s[i] + "-");
                                sw.Write("[" + s[i] + "-");
                            }
                            else
                            {
                                Console.Write(s[i] + "]");
                                sw.Write(s[i] + "]");
                            }

                        }
                        Console.Write("\n");
                        sw.Write("\n");
                    }
                }
                else
                {
                    Console.WriteLine(">>No Solution<<");
                    sw.WriteLine(">>No Solution<<");
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