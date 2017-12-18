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
        public static List<Student> rawStudents { get; set; }
        [STAThread]
        static void Main(string[] args)
        {
            string tempPath = Path();
            rawStudents = Relations(tempPath);
            rawCount = rawStudents.Count();
            // calculation
            List<Room> finalRooms = new List<Room>();
            while (rawStudents.Count() > 0)
            {
                Room tempRoom = NextRoom();
                if (tempRoom.MembersAreHated() == false)
                {
                    finalRooms.Add(tempRoom);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("The given instructions are contradictory!");
                }
            }
            // output
            Console.WriteLine("");
            ReturnRooms(finalRooms, tempPath);
            Console.ReadKey();
        }

        // - finds the room based on the first member of rawStudents
        private static Room NextRoom()
        {
            Room tempRoom = new Room();
            Student startStudent = rawStudents[0];
            tempRoom.Members.Add(startStudent.name);
            rawStudents.RemoveAt(0);
            tempRoom.PeopleMembersHate.AddRange(startStudent.HatedPeople);
            List<string> toAdd = new List<string>();
            toAdd.AddRange(startStudent.LikedPeople);

            while (toAdd.Count() > 0)
            {
                (Room room, List<string> toAdd) result = AddRelevantStudents(tempRoom, toAdd);
                tempRoom = result.room;
                toAdd = result.toAdd;                
            }

            return tempRoom;
        }

        // - returns room, toAdd and the remaining potential, after all relevant students for the current step have been added to the data structures
        private static (Room room, List<string> toAdd) AddRelevantStudents(Room room, List<string> toAdd)
        {
            (Student student, bool exists) listEntry = FromList(toAdd[0]);

            if (listEntry.exists)
            {
                room.Members.Add(toAdd[0]);
            }
            toAdd.RemoveAt(0);
            rawStudents.Remove(listEntry.student);
            if (listEntry.exists)
            {
                (Room room, List<string> toAdd) merged = MergeIntoRoom(room, listEntry.student, toAdd);
                room = merged.room;
                toAdd = merged.toAdd;
            }

            toAdd = UpdateMemberLikedBy(room, toAdd);
            
            return (room, toAdd);
        }

        // - returns the first student with a given name from the raw list
        private static (Student student, bool exists) FromList(string name)
        {
            Student currentStudent = new Student(name);
            bool exists = false;
            foreach (Student potentialRawStudent in rawStudents)
            {
                if (potentialRawStudent.name == name)
                {
                    currentStudent = potentialRawStudent;
                    exists = true;
                    break;
                }
            }
            return (currentStudent, exists);
        }

        // - Updates the Room with the values of the current student
        private static (Room room, List<string> toAdd) MergeIntoRoom(Room room, Student currentStudent, List<string> toAdd)
        {
            return (UpdateHated(room, currentStudent), UpdateLiked(room, currentStudent, toAdd));
        }

        // - Adds all the students liked by a given one to the toAdd list
        private static List<string> UpdateLiked(Room room, Student currentStudent, List<string> toAdd)
        {
            foreach (string liked in currentStudent.LikedPeople)
            {
                bool newEntry = true;
                foreach (string alreadyThere in room.Members)
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
            return toAdd;
        }

        // - Adds all the students hated by a given one to the hated list of a given room
        private static Room UpdateHated(Room room, Student currentStudent)
        {
            foreach (string hated in currentStudent.HatedPeople)
            {
                bool newEntry = true;
                foreach (string alreadyHated in room.PeopleMembersHate)
                {
                    if (hated == alreadyHated)
                    {
                        newEntry = false;
                        break;
                    }
                }
                if (newEntry)
                {
                    room.PeopleMembersHate.Add(hated);
                }
            }
            return room;
        }

        // - Adds all the students that like another from a given room to the toAdd list
        private static List<string> UpdateMemberLikedBy(Room room, List<string> toAdd)
        {
            foreach (Student potentialMember in rawStudents)
            {
                bool needsToBeAdded = false;
                foreach (string liked in potentialMember.LikedPeople)
                {
                    foreach (string member in room.Members)
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
            return toAdd;
        }


        // - gets a path from the user and prints some interactive data to the console
        private static string Path()
        {
            string tempPath = "";
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                tempPath = oFD.FileName;
            }
            Console.WriteLine("The following path was opened: " + tempPath);
            return tempPath;
        }

        // - reads the necessary data from a given file
        private static List<Student> Relations(string path)
        {
            //creates a StreamReader to a custom file            
            StreamReader tempSR = new StreamReader(path);
            List<Student> total = new List<Student>();
            //reads the file and gets all the necessary information
            while (tempSR.EndOfStream == false)
            {
                //every block of information consists of four lines:
                //the name of the student
                Student tempStudent = new Student(tempSR.ReadLine());

                //all the liked persons in one line
                string stringForLikedPepole = tempSR.ReadLine();
                foreach (string likedPerson in stringForLikedPepole.Split(' '))
                {
                    if (likedPerson != "+" && likedPerson != "")
                    {
                        tempStudent.LikedPeople.Add(likedPerson);
                    }
                }
                //all the hated persons in one line
                string stringForHatedPeople = tempSR.ReadLine();
                foreach (string hatedPerson in stringForHatedPeople.Split(' '))
                {
                    if (hatedPerson != "-" && hatedPerson != "")
                    {
                        tempStudent.HatedPeople.Add(hatedPerson);
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

        // - Prints the final room arrangement to Console / a file
        private static void ReturnRooms(List<Room> rooms, string path)
        {
            Console.WriteLine("Every line represents one room, every name in one line is one room member.\n");


            ReturnRooms(rooms);
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
            foreach (Room room in rooms)
            {
                foreach (string member in room.Members)
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
        private static void ReturnRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                foreach (string member in room.Members)
                {
                    Console.Write(member + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
