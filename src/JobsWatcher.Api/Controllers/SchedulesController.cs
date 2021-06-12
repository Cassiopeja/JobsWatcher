using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ScheduleDto>>> GetAllSchedules([FromQuery] int? subscriptionId)
        {
            var schedules = subscriptionId == null
                ? await _scheduleService.GetSchedulesAsync()
                : await _scheduleService.GetSchedulesBySubscriptionIdAsync(subscriptionId.Value);
            var response = _mapper.Map<List<ScheduleDto>>(schedules);
            return Ok(response);
        }
    }
}