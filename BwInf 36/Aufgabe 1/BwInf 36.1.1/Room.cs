using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._1
{
    public class Room
    {
        public Room()
        {
            this.Members = new List<string>();
            this.PeopleMembersHate = new List<string>();
        }
        public List<string> Members { get; set; }
        public List<string> PeopleMembersHate { get; set; }
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
