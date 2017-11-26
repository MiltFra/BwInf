using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._1
{
    //everything in here should be self-explanatory
    public class Student
    {
        // creates a student without hated/like people; has just a name
        public Student(string name)
        {
            this.name = name;
            this.LikedPeople = new List<string>();
            this.HatedPeople = new List<string>();
        }
        // identifier for a student (same name, same student), like a primary key in databases
        public string name { get; set; }
        // Lazy Properties, more isn't needed though
        // contain all the other Students names this student likes / hates; should be unique
        public List<string> LikedPeople { get; set; }
        public List<string> HatedPeople { get; set; }        
    }
}
