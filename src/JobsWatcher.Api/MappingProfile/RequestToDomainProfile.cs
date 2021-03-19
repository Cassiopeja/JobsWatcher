using System;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Requests.Queries;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Api.MappingProfile
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<SourceTypeDto, SourceType>();
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<SubscriptionCreateDto, SourceSubscription>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.SourceType, opt => opt.Ignore());
            CreateMap<SubscriptionUpdateDto, SourceSubscription>()
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.SourceType, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SubscriptionVacancyDto, SubscriptionVacancy>()
                .ForMember(dest => dest.Vacancy, opt => opt.MapFrom(src => (Vacancy) null))
                .ForMember(dest => dest.VacancyId, opt => opt.MapFrom(src => src.Vacancy.Id));
            CreateMap<GetAllSubscriptionVacanciesQuery, GetAllSubscriptionVacanciesFilter>();
            CreateMap<SortByQuery, SortByOptions>()
                .ForMember(dest=>dest.SortBy, opt=>opt.MapFrom(src=> StartWithMinus(src.SortBy)? src.SortBy.Substring(1): src.SortBy))
                .ForMember(dest => dest.Ascending, opt => opt.MapFrom(src => !StartWithMinus(src.SortBy)));
        }

        private bool StartWithMinus(string text)
        {
            return !string.IsNullOrEmpty(text) && text.StartsWith("-");
        }
    }
}