using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._1
{
    public class cRoom
    {
        public List<string> members { get; set; }
        public List<string> peopleMembersHate { get; set; }
        public bool membersAreHated ()
        {
            foreach (string member in members)
            {
                foreach (string hated in peopleMembersHate)
                {
                    if (member == hated)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public cRoom()
        {
            this.members = new List<string>();
            this.peopleMembersHate = new List<string>();
        }
    }
}
