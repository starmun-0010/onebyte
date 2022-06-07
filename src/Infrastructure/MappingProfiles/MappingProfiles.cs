using AutoMapper;
using OneByte.Contracts.RequestModels.Doctor;
using OneByte.Contracts.RequestModels.Patient;
using OneByte.Contracts.RequestModels.Visit;
using OneByte.Contracts.ResponseModels;
using OneByte.DomainModels;

namespace OneByte.Infrastructure.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doctor, DoctorResponseModel>();
            CreateMap<Patient, PatientResponseModel>();

            CreateMap<DoctorPostRequestModel, Doctor>();
            CreateMap<DoctorPutRequestModel, Doctor>();
            CreateMap<PatientPostRequestModel, Patient>();
            CreateMap<PatientPutRequestModel, Patient>();
            CreateMap<VisitPostRequestModel, Visit>();
            CreateMap<VisitPutRequestModel, Visit>(); 
        }
    }
}