using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneByte.Models
{
    public class Visit 
    {
        [Key]
        public string ID { get; set; }
        public DateTime VisitDate { get; set; }
        [ForeignKey(nameof(Patient))]
        public string PatientId {get; set; }
        public Patient Patient {get; set;}
    }
}