using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Responses;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Api.MappingProfile
{
    public static class GroupedVacanciesMapping
    {
        public static PagedResponse<EmployerVacanciesDto> ToGroupedVacancies(
            this PagedItems<SubscriptionVacancy> pagedVacancies, IMapper mapper)
        {
            var employersDict = new Dictionary<int, EmployerVacanciesDto>();
            foreach (var subscriptionVacancy in pagedVacancies.Data)
            {
                var employerId = subscriptionVacancy.Vacancy.EmployerId;
                if (employersDict.ContainsKey(employerId))
                {
                    var groupedVacancy = employersDict[employerId].GroupedVacancies
                        .FirstOrDefault(v => v.HashCode.Equals(subscriptionVacancy.Vacancy.HashCode));
                    if (groupedVacancy == null)
                    {
                        groupedVacancy = mapper.Map<GroupedVacancyDto>(subscriptionVacancy.Vacancy);
                        employersDict[employerId].GroupedVacancies.Add(groupedVacancy);
                    }

                    var similarVacancy = mapper.Map<SimilarVacancyDto>(subscriptionVacancy);
                    groupedVacancy.SimilarVacancies.Add(similarVacancy);
                    if (groupedVacancy.Comment == null && !string.IsNullOrEmpty(subscriptionVacancy.Comment))
                    {
                        groupedVacancy.Comment = subscriptionVacancy.Comment;
                    }
                }
                else
                {
                    var groupedVacancy = mapper.Map<GroupedVacancyDto>(subscriptionVacancy.Vacancy);
                    var similarVacancy = mapper.Map<SimilarVacancyDto>(subscriptionVacancy);
                    groupedVacancy.SimilarVacancies = new List<SimilarVacancyDto> {similarVacancy};
                    groupedVacancy.Comment = subscriptionVacancy.Comment;
                    employersDict[employerId] = new EmployerVacanciesDto
                    {
                        Id = subscriptionVacancy.Vacancy.EmployerId,
                        Name = subscriptionVacancy.Vacancy.Employer.Name,
                        GroupedVacancies = new List<GroupedVacancyDto> {groupedVacancy}
                    };
                }
            }

            return new PagedResponse<EmployerVacanciesDto>
            {
                TotalPages = pagedVacancies.TotalPages,
                PageNumber = pagedVacancies.PageNumber,
                PageSize = pagedVacancies.PageSize,
                TotalCount = pagedVacancies.TotalCount,
                Data = employersDict.Values.ToList()
            };
        }
    }
}