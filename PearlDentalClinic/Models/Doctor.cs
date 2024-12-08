using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlDentalClinic.Models
{
    public class Doctor : Person
    {
        [Key]
        public int Id { get; set; }
        public required string DoctorName { get; set; }
        public required string Surname { get; set; }
        public required string Specialization { get; set; }
        public string Experience { get; set; }

    }
}
