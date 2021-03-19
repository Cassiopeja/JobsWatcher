using System;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Requests.Queries;
using JobsWatcher.Api.Contracts.Responses;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Vacancy;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly ILogger<VacanciesController> _logger;
        private readonly IMapper _mapper;
        private readonly IVacancyService _vacancyService;

        public VacanciesController(ILogger<VacanciesController> logger, IMapper mapper, IVacancyService vacancyService)
        {
            _logger = logger;
            _mapper = mapper;
            _vacancyService = vacancyService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<VacancySnippetDto>>> GetAllVacancies(
            [FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var pagedVacancies = await _vacancyService.GetVacanciesAsync(paginationFilter);
            var vacanciesResponse = _mapper.Map<PagedResponse<VacancySnippetDto>>(pagedVacancies);
            return Ok(vacanciesResponse);
        }

        [HttpGet("{vacancyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> GetVacancyById(int vacancyId)
        {
            try
            {
                var vacancy = await _vacancyService.GetVacancyByIdAsync(vacancyId);
                var vacancyDto = _mapper.Map<VacancyDto>(vacancy);
                return Ok(vacancyDto);
            }
            catch (VacancyValidationException vacancyValidationException)
                when (vacancyValidationException.InnerException is NotFoundVacancyException)
            {
                var innerMessage = GetInnerMessage(vacancyValidationException);

                return NotFound(innerMessage);
            }
            catch (VacancyValidationException vacancyValidationException)
            {
                var innerMessage = GetInnerMessage(vacancyValidationException);
                return BadRequest(innerMessage);
            }
            catch (VacancyServiceException vacancyServiceException)
            {
                return Problem(vacancyServiceException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception)
        {
            return exception.InnerException?.Message;
        }
    }
}