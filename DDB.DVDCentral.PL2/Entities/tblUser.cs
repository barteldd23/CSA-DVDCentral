using System;
using System.Collections.Generic;

namespace DDB.DVDCentral.PL2.Entities;

public class tblUser : IEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
