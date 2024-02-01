using System;
using System.Collections.Generic;

namespace DDB.DVDCentral.PL2.Entities;

public class tblOrderItem : IEntity
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid MovieId { get; set; }

    public int Quantity { get; set; }

    public double Cost { get; set; }
    public virtual tblMovie Movie { get; set; }
    public virtual tblOrder Order { get; set; }
}
