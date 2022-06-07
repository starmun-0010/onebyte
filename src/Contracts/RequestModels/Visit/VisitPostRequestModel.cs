using System;

namespace OneByte.Contracts.RequestModels.Visit
{
    public class VisitPostRequestModel
    {
        public DateTime VisitDate {get; set;}
        public Guid PatientID { get; set; }
        public Guid DoctorID { get; set; }
    }
}