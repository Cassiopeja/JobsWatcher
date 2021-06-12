using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsWatcher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IMapper _mapper;

        public EmployersController(IEmployerService employerService, IMapper mapper)
        {
            _employerService = employerService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmployerDto>>> GetAllEmployers([FromQuery] int? subscriptionId)
        {
            var schedules = subscriptionId == null
                ? await _employerService.GetEmployersAsync()
                : await _employerService.GetEmployersBySubscriptionIdAsync(subscriptionId.Value);
            var response = _mapper.Map<List<EmployerDto>>(schedules);
            return Ok(response);
        }
    }
}