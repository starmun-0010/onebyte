using AutoMapper;
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
        }
    }
}