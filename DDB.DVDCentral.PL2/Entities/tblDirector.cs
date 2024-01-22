using System;
using System.Collections.Generic;

namespace DDB.DVDCentral.PL2.Entities;

public class tblDirector
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public virtual ICollection<tblMovie> tblMovies { get; set; }
}
