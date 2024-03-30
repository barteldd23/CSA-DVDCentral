using System;
using System.Collections.Generic;

namespace DDB.DVDCentral.PL2.Entities;

public class tblFormat : IEntity
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;
    public virtual ICollection<tblMovie> tblMovies { get; set; }
    public string SortField { get { return Description; } }
}
