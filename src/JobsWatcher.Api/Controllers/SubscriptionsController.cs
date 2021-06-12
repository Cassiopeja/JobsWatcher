using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Requests.Queries;
using JobsWatcher.Api.Contracts.Responses;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.Subscription;
using JobsWatcher.Core.Exceptions.SubscriptionVacancy;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionVacancyService _subscriptionVacancyService;
        private readonly IEmploymentService _employmentService;
        private readonly IScheduleService _scheduleService;
        private readonly IEmployerService _employerService;

        public SubscriptionsController(IMapper mapper,
            ISubscriptionService subscriptionService,
            ISubscriptionVacancyService subscriptionVacancyService,
            IEmploymentService employmentService,
            IScheduleService scheduleService,
            IEmployerService employerService)
        {
            _mapper = mapper;
            _subscriptionService = subscriptionService;
            _subscriptionVacancyService = subscriptionVacancyService;
            _employmentService = employmentService;
            _scheduleService = scheduleService;
            _employerService = employerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SubscriptionDto>>> GetAllSubscriptions()
        {
            try
            {
                var subscriptions = await _subscriptionService.GetSubscriptionsAsync();
                var subscriptionsResponse = _mapper.Map<List<SubscriptionDto>>(subscriptions);
                return Ok(subscriptionsResponse);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("{subscriptionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionDto>> GetSubscriptionById(int subscriptionId)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
                var subscriptionResponse = _mapper.Map<SubscriptionDto>(subscription);
                return Ok(subscriptionResponse);
            }
            catch (SubscriptionValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SubscriptionValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);
                return BadRequest(innerMessage);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionDto>> PostSubscription(SubscriptionCreateDto createSubscriptionDto)
        {
            try
            {
                var inputSubscription = _mapper.Map<SourceSubscription>(createSubscriptionDto);
                var insertedSubscription = await _subscriptionService.AddSubscriptionAsync(inputSubscription);
                var subscriptionResponse = _mapper.Map<SubscriptionDto>(insertedSubscription);
                return Ok(subscriptionResponse);
            }
            catch (SubscriptionValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);

                return BadRequest(innerMessage);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPut("{subscriptionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionDto>> PutSubscription(int subscriptionId,
            SubscriptionUpdateDto subscriptionDto)
        {
            try
            {
                var subscription = _mapper.Map<SourceSubscription>(subscriptionDto);
                subscription.Id = subscriptionId;
                var updatedSubscription = await _subscriptionService.UpdateSubscriptionAsync(subscription);
                var subscriptionResponse = _mapper.Map<SubscriptionDto>(updatedSubscription);
                return Ok(subscriptionResponse);
            }
            catch (SubscriptionValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SubscriptionValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);

                return BadRequest(innerMessage);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpDelete("{subscriptionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionDto>> DeleteSubscription(int subscriptionId)
        {
            try
            {
                var deletedSubscription = await _subscriptionService.DeleteSubscriptionByIdAsync(subscriptionId);
                var subscriptionResponse = _mapper.Map<SubscriptionDto>(deletedSubscription);
                return Ok(subscriptionResponse);
            }
            catch (SubscriptionValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SubscriptionValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);

                return BadRequest(innerMessage);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("{subscriptionId}/vacancies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<SubscriptionVacancyDto>>> GetAllSubscriptionVacancies(
            int subscriptionId,
            [FromQuery] GetAllSubscriptionVacanciesQuery query,
            [FromQuery] PaginationQuery paginationQuery,
            [FromQuery] SortByQuery sortByQuery)
        {
            try
            {
                var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
                var queryFilter = _mapper.Map<GetAllSubscriptionVacanciesFilter>(query);
                var sortByOption = _mapper.Map<SortByOptions>(sortByQuery);
                var pagedVacancies =
                    await _subscriptionVacancyService.GetSubscriptionVacanciesAsync(subscriptionId, queryFilter,
                        paginationFilter, sortByOption);
                var vacanciesResponse = _mapper.Map<PagedResponse<SubscriptionVacancyDto>>(pagedVacancies);
                return Ok(vacanciesResponse);
            }
            catch (SubscriptionVacancyValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SubscriptionVacancyValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);

                return BadRequest(innerMessage);
            }
            catch (SubscriptionVacancyServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("{subscriptionId}/employments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmploymentDto>>> GetAllEmployments(int subscriptionId)
        {
            var employments = await _employmentService.GetEmploymentsBySubscriptionIdAsync(subscriptionId);
            var response = _mapper.Map<List<EmploymentDto>>(employments);
            return Ok(response);
        }

        [HttpGet("{subscriptionId}/schedules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ScheduleDto>>> GetAllSchedules(int subscriptionId)
        {
            var schedules = await _scheduleService.GetSchedulesBySubscriptionIdAsync(subscriptionId);
            var response = _mapper.Map<List<ScheduleDto>>(schedules);
            return Ok(response);
        }

        [HttpGet("{subscriptionId}/employers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmployerDto>>> GetAllEmployers(int subscriptionId)
        {
            var schedules = await _employerService.GetEmployersBySubscriptionIdAsync(subscriptionId);
            var response = _mapper.Map<List<EmployerDto>>(schedules);
            return Ok(response);
        }

        [HttpPost("{subscriptionId}/updatevacancies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateVacancies(int subscriptionId)
        {
            try
            {
                var storageSubscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
                await _subscriptionService.UpdateSubscriptionVacanciesAsync(storageSubscription);
                return Ok();
            }
            catch (SubscriptionValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SubscriptionValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);

                return BadRequest(innerMessage);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPost("archivevacancies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ArchiveVacancies()
        {
            await _subscriptionService.ArchiveSubscriptionVacanciesAsync();
            return Ok();
        }

        private static string GetInnerMessage(Exception exception)
        {
            return exception.InnerException?.Message;
        }
    }
}