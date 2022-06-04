using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneByte.DomainModels
{
    public class Visit 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public DateTime VisitDate { get; set; }
        [ForeignKey(nameof(Patient))]
        public Guid PatientId {get; set; }
        public Patient Patient {get; set;}
        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId {get; set;}
        public Doctor Doctor {get; set;}
    }
}