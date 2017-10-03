using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace BwInf_36._1._1
{
    public class Program
    {
        static int rawCount = 0;
        [STAThread]
        static void Main(string[] args)
        {
            string tempPath = "";
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                tempPath = oFD.FileName;
            }
            Console.WriteLine("The following path was opened: " + tempPath);
            List<cStudent> rawStudents = getRelations(tempPath);
            rawCount = rawStudents.Count();
            List<cRoom> finalRooms = new List<cRoom>();
            while (rawStudents.Count() > 0)
            {
                cRoom tempRoom = new cRoom();
                cStudent startStudent = rawStudents[0];
                tempRoom.members.Add(startStudent.name);
                rawStudents.RemoveAt(0);
                tempRoom.peopleMembersHate.AddRange(startStudent.hatedPeople);
                List<string> toAdd = new List<string>();
                bool potential = true;
                toAdd.AddRange(startStudent.likedPeople);
                while (potential)
                {
                    while (toAdd.Count() > 0)
                    {                   
                        cStudent currentStudent = new cStudent(toAdd[0]);
                        bool studentExists = false;
                        foreach (cStudent potentialRawStudent in rawStudents)
                        {
                            if (potentialRawStudent.name == toAdd[0])
                            {
                                currentStudent = potentialRawStudent;
                                studentExists = true;
                                break;
                            }
                        }
                        if (studentExists)
                        {
                            tempRoom.members.Add(toAdd[0]);
                        }
                        toAdd.RemoveAt(0);
                        rawStudents.Remove(currentStudent);
                        if (studentExists)
                        {
                            foreach (string liked in currentStudent.likedPeople)
                            {
                                bool newEntry = true;
                                foreach (string alreadyThere in tempRoom.members)
                                {
                                    if (liked == alreadyThere)
                                    {
                                        newEntry = false;
                                        break;
                                    }
                                }
                                if (newEntry)
                                {
                                    foreach (string alreadyLiked in toAdd)
                                    {
                                        if (liked == alreadyLiked)
                                        {
                                            newEntry = false;
                                            break;
                                        }
                                    }
                                    if (newEntry)
                                    {
                                        toAdd.Add(liked);
                                    }
                                }
                            }
                            foreach (string hated in currentStudent.hatedPeople)
                            {
                                bool newEntry = true;
                                foreach (string alreadyHated in tempRoom.peopleMembersHate)
                                {
                                    if (hated == alreadyHated)
                                    {
                                        newEntry = false;
                                        break;
                                    }
                                }
                                if (newEntry)
                                {
                                    tempRoom.peopleMembersHate.Add(hated);
                                }
                            }
                        }
                    }
                    foreach (cStudent potentialMember in rawStudents)
                    {
                        bool needsToBeAdded = false;
                        foreach (string liked in potentialMember.likedPeople)
                        {
                            foreach (string member in tempRoom.members)
                            {
                                if (liked == member)
                                {
                                    needsToBeAdded = true;
                                    break;
                                }
                            }
                            if (needsToBeAdded)
                            {
                                break;
                            }
                        }
                        if (needsToBeAdded)
                        {
                            toAdd.Add(potentialMember.name);
                        }
                    }
                    if (toAdd.Count() == 0)
                    {
                        potential = false;
                    }
                }
                if (tempRoom.membersAreHated() == false)
                {
                    finalRooms.Add(tempRoom);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("The given instructions are contradictory!");
                }
            }
            Console.WriteLine("");
            returnRooms(finalRooms, tempPath);
            Console.ReadKey();
        }
        private static List<cStudent> getRelations(string path)
        {
            //creates a StreamReader to a custom file            
            StreamReader tempSR = new StreamReader(path);
            List<cStudent> total = new List<cStudent>();
            //reads the file and gets all the necessary information
            while (tempSR.EndOfStream == false)
            {
                //every block of information consists of four lines:
                //the name of the student
                cStudent tempStudent = new cStudent(tempSR.ReadLine());

                //all the liked persons in one line
                string stringForLikedPepole = tempSR.ReadLine();
                foreach (string likedPerson in stringForLikedPepole.Split(' '))
                {
                    if (likedPerson != "+" && likedPerson != "")
                    {
                        tempStudent.likedPeople.Add(likedPerson);
                    }
                }
                //all the hated persons in one line
                string stringForHatedPeople = tempSR.ReadLine();
                foreach (string hatedPerson in stringForHatedPeople.Split(' '))
                {
                    if (hatedPerson != "-" && hatedPerson != "")
                    {
                        tempStudent.hatedPeople.Add(hatedPerson);
                    }
                }
                total.Add(tempStudent); //the information is saved here

                //and an empty line
                tempSR.ReadLine();
            }
            tempSR.Dispose();
            //returns that information
            return total;
        }
        private static void returnRooms(List<cRoom> rooms, string path)
        {
            returnRooms(rooms);
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
            StreamWriter sw = new StreamWriter(path);            
            int count = 0;
            foreach (cRoom room in rooms)
            {
                foreach (string member in room.members)
                {
                    sw.Write(member + " ");
                    count++;
                }
                sw.Write("\n");
            }
            sw.WriteLine(count.ToString() + " / " + rawCount.ToString());
            sw.Dispose();
            Console.WriteLine("The results were saved to " + path);
        }
        private static void returnRooms(List<cRoom> rooms)
        {
            foreach (cRoom room in rooms)
            {
                foreach (string member in room.members)
                {
                    Console.Write(member + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
