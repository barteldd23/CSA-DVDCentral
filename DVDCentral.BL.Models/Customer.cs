using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDCentral.BL.Models
{
    public class Customer
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string ZIP { get; set;}
        public required string Phone { get; set;}
        public required int UserId { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
