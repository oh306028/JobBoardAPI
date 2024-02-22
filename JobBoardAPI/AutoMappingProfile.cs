using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Models;

namespace JobBoardAPI
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<JobOffert, JobOfferDto>()
                .ForMember(e => e.Experience, s => s.MapFrom(r => r.Requirement.Experience))
                 .ForMember(e => e.Education, s => s.MapFrom(r => r.Requirement.Education.ToString()))
                  .ForMember(e => e.JobTime, s => s.MapFrom(r => r.JobTime.ToString()))
                   .ForMember(e => e.JobType, s => s.MapFrom(r => r.JobType.ToString()))
                    .ForMember(e => e.Age, s => s.MapFrom(r => r.Requirement.Age));

            CreateMap<Seeker, SeekerDto>()
                .ForMember(e => e.Education, s => s.MapFrom(t => t.Education.ToString()));
        }
    }
}
