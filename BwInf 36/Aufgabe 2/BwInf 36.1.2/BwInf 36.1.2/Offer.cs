using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._2
{
    public class Offer
    {
        public double Price { get; set; }
        public List<string> Tickets { get; set; }
        public Offer(List<string> tickets, double price)
        {
            this.Price = price;
            this.Tickets = tickets;
        }
    }
}
