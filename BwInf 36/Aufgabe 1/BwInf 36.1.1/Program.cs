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
    // DISCLAIMER: you might not be satisfied with the kind of comentary in this file
    // if you are looking for something truly serious you might want to contact me (vrelda@outlook.de) or look into the documentation
    public class Program
    {
        static int rawCount = 0;
        public static List<Student> rawStudents { get; set; }
        [STAThread]
        static void Main(string[] args)
        {
            // getting a path
            string tempPath = Path();
            // getting the students from the path
            rawStudents = Relations(tempPath);
            // calculation
            // the rooms that are returned in the end, only the ones that actually work
            List<Room> finalRooms = new List<Room>();
            // the target is to process every student, when that's done all the possible rooms are found
            while (rawStudents.Count() > 0)
            {
                // gets the necessary room based on the first student in rawStudents
                Room tempRoom = NextRoom();
                // if it's not contradictory, it's added to the final rooms list
                if (tempRoom.MembersAreHated() == false) { finalRooms.Add(tempRoom); }
                else
                {
                    // just some output to let the user know
                    Console.WriteLine("");
                    Console.WriteLine("The given instructions are contradictory!");
                }
            }
            // output
            Console.WriteLine("");
            // returns the results of the calculation
            ReturnRooms(finalRooms, tempPath);
            // don't exit 'til the user demands it
            Console.ReadKey();
        }

        // - finds the room based on the first member of rawStudents
        private static Room NextRoom()
        {
            // creates a room to store all the members in
            Room tempRoom = new Room();
            // first one is the first one from raw students, removed from there immediately
            Student startStudent = rawStudents[0];
            tempRoom.Members.Add(startStudent.name);
            rawStudents.RemoveAt(0);
            // since the hate list is empty atm, we can add all the hated students without checking for duplicates
            tempRoom.PeopleMembersHate.AddRange(startStudent.HatedPeople);
            // just a to do list for students, has names of ppl that we need to check
            List<string> toAdd = new List<string>();
            // same as the hate list, no duplicates possible yet
            toAdd.AddRange(startStudent.LikedPeople);
            // gotta work on that to do list
            while (toAdd.Count() > 0)
            {
                // updates the to do list and the room so that all the relevant students are added for now
                (Room room, List<string> toAdd) result = AddRelevantStudents(tempRoom, toAdd);
                tempRoom = result.room;
                toAdd = result.toAdd;
                // that process is repeated until no more students for that room are found
            }

            return tempRoom;
        }

        // - returns room, toAdd and the remaining potential, after all relevant students for the current step have been added to the data structures
        private static (Room room, List<string> toAdd) AddRelevantStudents(Room room, List<string> toAdd)
        {
            // we need a student with the given name
            (Student student, bool exists) listEntry = FromList(toAdd[0]);
            // if we found one, we can add her to the room
            if (listEntry.exists)
            {
                room.Members.Add(toAdd[0]);
            }
            // in both cases we can remove the name from the to do list and the raw list
            toAdd.RemoveAt(0);
            rawStudents.Remove(listEntry.student);
            // we don't need to do that if we didn't find a student, but that is an error anyways, so that's not usual
            if (listEntry.exists)
            {
                // we need to put all the necessary data from the student into the room and the toAdd list
                (Room room, List<string> toAdd) merged = MergeIntoRoom(room, listEntry.student, toAdd);
                room = merged.room;
                toAdd = merged.toAdd;
            }
            // after we put some more members into the room, we need to find other students that like those new members
            toAdd = UpdateMemberLikedBy(room, toAdd);
            // these are all the current relevant students, that process might need to be repeated to find ALL of them
            return (room, toAdd);
        }

        // - returns the first student with a given name from the raw list
        private static (Student student, bool exists) FromList(string name)
        {
            // just checking through all names of the rawStudents, return the first match (if none, then exists is false)
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
            // needs to be done for all liked persons
            foreach (string liked in currentStudent.LikedPeople)
            {
                bool newEntry = true;
                // we don't need to add people twice
                foreach (string alreadyThere in room.Members)
                {
                    if (liked == alreadyThere)
                    {
                        newEntry = false;
                        break;
                    }
                }
                // if the person isn't in the room yet, we have to do something
                if (newEntry)
                {
                    // if the person is already on the toAdd list, we don't need to add her one more time
                    foreach (string alreadyLiked in toAdd)
                    {
                        if (liked == alreadyLiked)
                        {
                            newEntry = false;
                            break;
                        }
                    }
                    // if the person is truly new to the calculation, we need to write her onto the to do list
                    if (newEntry)
                    {
                        toAdd.Add(liked);
                    }
                }
            }
            // the final to do list is returned
            return toAdd;
        }

        // - Adds all the students hated by a given one to the hated list of a given room
        private static Room UpdateHated(Room room, Student currentStudent)
        {
            // we check for each of the hated persons of the student wether it's on the rooms hate list already, if not, we write it down. simple.
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
            // we check every raw students like list. 
            // if a liked person is member of the room, the original students is written onto the to do list
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
