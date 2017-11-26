using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._1
{
    // a room has members and those members hate people
    public class Room
    {
        // creates new lists for hated ppl and members
        public Room()
        {
            this.Members = new List<string>();
            this.PeopleMembersHate = new List<string>();
        }
        // names of members
        public List<string> Members { get; set; }
        // unique names of people hated by a member
        public List<string> PeopleMembersHate { get; set; }
        // true if there is a string that's identical in both lists
        public bool MembersAreHated ()
        {
            foreach (string member in Members)
            {
                foreach (string hated in PeopleMembersHate)
                {
                    if (member == hated)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
    }
}
