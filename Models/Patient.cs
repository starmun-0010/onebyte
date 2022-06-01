using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneByte.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}