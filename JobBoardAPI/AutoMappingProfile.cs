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

          

            CreateMap<CreateOfferDto, JobOffert>()
               .ForMember(dest => dest.Requirement,
                          opt => opt.MapFrom(src => new Requirement
                          {
                              Education = Enum.Parse<Forms.Education>(src.Education),
                              Experience = src.Experience,
                              Age = src.Age
                          }))
               .ForMember(dest => dest.JobTime,
                          opt => opt.MapFrom(src => Enum.Parse<Forms.JobTime>(src.JobTime)))
               .ForMember(dest => dest.JobType,
                          opt => opt.MapFrom(src => Enum.Parse<Forms.JobType>(src.JobType)));


            CreateMap<Requirement, RequirementDto>()
                .ForMember(e => e.Education, obj => obj.MapFrom(src => src.Education.ToString()));

        }
    }
}
