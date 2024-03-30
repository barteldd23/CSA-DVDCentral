using System;
using System.Collections.Generic;

namespace DDB.DVDCentral.PL2.Entities;

public class tblGenre : IEntity
{ 
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;
    public virtual ICollection<tblMovieGenre> tblMovieGenres { get; set; }
    public string SortField { get { return Description; } }
}
