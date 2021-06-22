using System.Collections.Generic;
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
            CreateMap<Skill, VacancySkillDto>();
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
            CreateMap<Vacancy, GroupedVacancyDto>()
                .ForMember(dest => dest.Descriptions, opt => opt.MapFrom(src => src.Descriptions))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.VacancySkills, opt => opt.MapFrom(src => src.VacancySkills))
                .ForMember(dest => dest.SimilarVacancies, opt => opt.MapFrom(src => new List<SimilarVacancyDto>()));
            CreateMap<SubscriptionVacancy, SimilarVacancyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Vacancy.Area))
                .ForMember(dest => dest.SourceType, opt => opt.MapFrom(src => src.Vacancy.SourceType))
                .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => src.Vacancy.Employment))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Vacancy.Schedule))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Vacancy.Currency.Name))
                .ForMember(dest => dest.SalaryFrom, opt => opt.MapFrom(src => src.Vacancy.SalaryFrom))
                .ForMember(dest => dest.SalaryTo, opt => opt.MapFrom(src => src.Vacancy.SalaryTo))
                .ForMember(dest => dest.IsSalaryGross, opt => opt.MapFrom(src => src.Vacancy.IsSalaryGross))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Vacancy.Url))
                .ForMember(dest => dest.IsArchived, opt => opt.MapFrom(src => src.Vacancy.IsArchived))
                .ForMember(dest => dest.IsRemote, opt => opt.MapFrom(src => src.Vacancy.IsRemote))
                .ForMember(dest => dest.SourceCreatedDate, opt => opt.MapFrom(src => src.Vacancy.SourceCreatedDate))
                .ForMember(dest => dest.SourceUpdatedDate, opt => opt.MapFrom(src => src.Vacancy.SourceUpdatedDate))
                .ForMember(dest => dest.ContentUpdatedDate, opt => opt.MapFrom(src => src.Vacancy.ContentUpdatedDate))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Vacancy.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.Vacancy.UpdatedDate))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.IsHidden, opt => opt.MapFrom(src => src.IsHidden));
        }
    }
}