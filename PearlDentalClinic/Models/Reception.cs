﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlDentalClinic.Models
{
  class Reception: Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
