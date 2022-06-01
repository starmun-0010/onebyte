using System.Collections.Generic;

namespace OneByte.Models
{
    public class Doctor 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<Patient> Patients { get; set; }
    }
}