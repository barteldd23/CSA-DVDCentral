using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDCentral.BL.Models
{
    public class Director
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set;}
        
        public string FullName 
        {
            get { return FirstName + " " + LastName; } 
        }
    }
}
