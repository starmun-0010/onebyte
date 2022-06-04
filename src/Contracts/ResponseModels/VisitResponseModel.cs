using System;

namespace OneByte.Contracts.ResponseModels
{
    public class VisitResponseModel
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; } 
        public Guid PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }
}