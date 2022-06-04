using System;

namespace OneByte.Contracts.ResponseModels
{
    public class PatientResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
    }
}