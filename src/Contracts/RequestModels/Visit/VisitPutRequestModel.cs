using System;

namespace OneByte.Contracts.RequestModels.Visit
{
    public class VisitPutRequestModel
    {
        public Guid ID {get; set;}
        public DateTime VisitDate {get; set;}
        public Guid PatientID { get; set; }
        public Guid DoctorID { get; set; }
    }
}