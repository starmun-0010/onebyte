using System;

namespace OneByte.Models
{
    public class Visit 
    {
        public int ID { get; set; }
        public DateTime VisitDate { get; set; }
        public string PatientId {get; set; }
        public Patient Patient {get; set;}
    }
}