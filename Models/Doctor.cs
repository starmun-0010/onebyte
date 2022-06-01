using System.Collections.Generic;

namespace OneByte.Models
{
    public class Doctor 
    {
        public string ID { get; set; }
        public string Contact { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}