using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._1
{
    //everything in here should be self explanatory
    public class cStudent
    {
        public string name { get; set; }
        public List<string> likedPeople { get; set; }
        public List<string> hatedPeople { get; set; }
        public cStudent(string name)
        {
            this.name = name;
            this.likedPeople = new List<string>();
            this.hatedPeople = new List<string>();
        }
    }
}
