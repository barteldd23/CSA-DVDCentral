using System;
using System.Collections.Generic;

namespace DDB.DVDCentral.PL2.Entities;

public class tblMovieGenre : IEntity
{
    public Guid Id { get; set; }

    public Guid MovieId { get; set; }

    public Guid GenreId { get; set; }
    public virtual tblGenre Genre { get; set; }
    public virtual tblMovie Movie { get; set; }
}
