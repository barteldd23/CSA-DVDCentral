using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDCentral.BL.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set;}
        public string Phone { get; set;}
        public Guid UserId { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
