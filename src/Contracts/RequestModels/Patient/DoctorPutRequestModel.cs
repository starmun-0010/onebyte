using System;

namespace OneByte.Contracts.RequestModels.Patient
{
    public class PatientPutRequestModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
    }
}