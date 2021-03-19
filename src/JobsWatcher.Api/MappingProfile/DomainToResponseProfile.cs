using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Responses;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Api.MappingProfile
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Area, AreaDto>();
            CreateMap<Employer, EmployerDto>();
            CreateMap<Employment, EmploymentDto>();
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<SourceType, SourceTypeDto>();
            CreateMap<VacancySkill, VacancySkillDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Skill.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Skill.Name));
            CreateMap<Vacancy, VacancyDto>()
                .ForMember(des => des.Currency, opt => opt.MapFrom(src => src.Currency.Name));
            CreateMap<Vacancy, VacancySnippetDto>()
                .ForMember(des => des.Currency, opt => opt.MapFrom(src => src.Currency.Name));
            CreateMap<PagedItems<Vacancy>, PagedResponse<VacancyDto>>();
            CreateMap<PagedItems<Vacancy>, PagedResponse<VacancySnippetDto>>();
            CreateMap<SourceSubscription, SubscriptionDto>()
                .ForMember(dest => dest.Data, opt => opt.Ignore());
            CreateMap<SourceReference, SourceReferenceDto>()
                .ForMember(dest => dest.Data, opt => opt.Ignore());
            CreateMap<SubscriptionVacancy, SubscriptionVacancyDto>();
            CreateMap<PagedItems<SubscriptionVacancy>, PagedResponse<SubscriptionVacancyDto>>();
        }
    }
}