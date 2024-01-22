using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.PL2.Entities
{
    public class tblCartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid MovieId { get; set; }
        public int Quantity { get; set; }
        public virtual tblCart Cart { get; set; } = new tblCart();
        public virtual tblMovie Movie { get; set; } = new tblMovie();

    }
}
