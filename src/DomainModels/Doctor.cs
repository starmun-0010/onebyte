using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OneByte.DomainModels
{
    public class Doctor 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public virtual ICollection<Patient> Patients { get; set; } 
    }
}