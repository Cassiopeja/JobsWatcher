using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Requests.Queries;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Skill;
using JobsWatcher.Core.Exceptions.Subscription;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ILogger<SkillsController> _logger;
        private readonly IMapper _mapper;
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService, ILogger<SkillsController> logger, IMapper mapper)
        {
            _skillService = skillService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VacancySkillDto>>> GetAllSkills([FromQuery] GetAllSkillsQuery skillsQuery)
        {
            try
            {
                var filter = _mapper.Map<GetAllSkillsFilter>(skillsQuery);
                var skills = await _skillService.GetAllSkills(filter);
                var skillsResponse = _mapper.Map<List<VacancySkillDto>>(skills);
                return Ok(skillsResponse);
            }
            catch (SkillValidationException exception)
                when (exception.InnerException is NotFoundSubscriptionException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SkillValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);

                return BadRequest(innerMessage);
            }
            catch (SkillServiceException exception)
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