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
    public class EmploymentsController : ControllerBase
    {
        private readonly IEmploymentService _employmentService;
        private readonly IMapper _mapper;

        public EmploymentsController(IMapper mapper, IEmploymentService employmentService)
        {
            _mapper = mapper;
            _employmentService = employmentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmploymentDto>>> GetAllEmployments([FromQuery] int? subscriptionId)
        {
            var employments = subscriptionId == null
                ? await _employmentService.GetEmploymentsAsync()
                : await _employmentService.GetEmploymentsBySubscriptionIdAsync(subscriptionId.Value);
            var response = _mapper.Map<List<EmploymentDto>>(employments);
            return Ok(response);
        }
    }
}