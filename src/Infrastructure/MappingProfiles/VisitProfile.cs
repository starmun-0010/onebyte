using AutoMapper;
using OneByte.Contracts.ResponseModels;
using OneByte.DomainModels;

namespace OneByte.Infrastructure.MappingProfiles
{
    public class VisitProfiles : Profile
    {
        public VisitProfiles()
        {
            CreateMap<Visit, VisitResponseModel>()
            .ForMember(v=>v.DoctorName, options => options
            .MapFrom(v=>v.Doctor.Name));
        }
    }
}