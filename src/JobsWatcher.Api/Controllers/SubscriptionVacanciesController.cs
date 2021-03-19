using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.SubscriptionVacancy;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionVacanciesController : ControllerBase
    {
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly IMapper _mapper;
        private readonly ISubscriptionVacancyService _subscriptionVacancyService;

        public SubscriptionVacanciesController(ILogger<SubscriptionsController> logger, IMapper mapper,
            ISubscriptionVacancyService subscriptionVacancyService)
        {
            _logger = logger;
            _mapper = mapper;
            _subscriptionVacancyService = subscriptionVacancyService;
        }

        [HttpGet("{subscriptionVacancyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionVacancyDto>> GetSubscriptionById(int subscriptionVacancyId)
        {
            try
            {
                var subscriptionVacancy =
                    await _subscriptionVacancyService.GetSubscriptionVacancyByIdAsync(subscriptionVacancyId);
                var subscriptionVacancyDto = _mapper.Map<SubscriptionVacancyDto>(subscriptionVacancy);
                return Ok(subscriptionVacancyDto);
            }
            catch (SubscriptionVacancyValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionVacancyException)
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

        [HttpPatch("{subscriptionVacancyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionVacancyDto>> Patch(int subscriptionVacancyId,
            [FromBody] JsonPatchDocument<SubscriptionVacancyDto> subscriptionVacancyPatch)
        {
            try
            {
                var storageSubscriptionVacancy = await
                    _subscriptionVacancyService.GetSubscriptionVacancyByIdAsync(subscriptionVacancyId);
                var subscriptionVacancyDto = _mapper.Map<SubscriptionVacancyDto>(storageSubscriptionVacancy);
                subscriptionVacancyPatch.ApplyTo(subscriptionVacancyDto, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var editedSubscriptionVacancy = _mapper.Map<SubscriptionVacancy>(subscriptionVacancyDto);
                var updatedSubscriptionVacancy = await
                    _subscriptionVacancyService.UpdateSubscriptionVacancyAsync(editedSubscriptionVacancy);
                var updatedSubscriptionVacancyDto = _mapper.Map<SubscriptionVacancyDto>(updatedSubscriptionVacancy);

                return updatedSubscriptionVacancyDto;
            }
            catch (SubscriptionVacancyValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionVacancyException)
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
        
        [HttpGet("{subscriptionVacancyId}/similar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SubscriptionVacancyDto>>> GetSimilarSubscriptionVacancies(int subscriptionVacancyId)
        {
            try
            {
                var similarVacancies = await 
                    _subscriptionVacancyService.GetSimilarSubscriptionVacancies(subscriptionVacancyId);
                var subscriptionVacanciesDto = _mapper.Map<List<SubscriptionVacancyDto>>(similarVacancies);
                return Ok(subscriptionVacanciesDto);
            }
            catch (SubscriptionVacancyValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionVacancyException)
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

        private static string GetInnerMessage(Exception exception)
        {
            return exception.InnerException?.Message;
        }
    }
}