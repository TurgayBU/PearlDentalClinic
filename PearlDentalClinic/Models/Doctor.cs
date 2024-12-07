using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlDentalClinic.Models
{
    public class Doctor : Person
    {
        public int Id { get; set; }
        public required string DoctorName { get; set; }
        public required string Specialization { get; set; }
        public int Experience { get; set; }

    }
}
