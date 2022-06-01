using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace OneByte.Models
{
    public class Patient
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}