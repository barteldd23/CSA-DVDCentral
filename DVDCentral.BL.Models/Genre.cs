﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
    }
}
