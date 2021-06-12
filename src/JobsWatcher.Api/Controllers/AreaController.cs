using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Exceptions.Area;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly ILogger<AreaController> _logger;
        private readonly IMapper _mapper;

        public AreaController(IAreaService areaService, IMapper mapper, ILogger<AreaController> logger)
        {
            _areaService = areaService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AreaDto>>> GetAllAreas([FromQuery]int? subscriptionId)
        {
            try
            {
                var areas = subscriptionId == null
                    ? await _areaService.GetAreasAsync()
                    : await _areaService.GetAreasBySubscriptionIdAsync(subscriptionId.Value);
                var response = _mapper.Map<List<AreaDto>>(areas);
                return Ok(response);
            }
            catch (AreaServiceException exception)
            {
                return Problem(exception.Message);
            }
        }
    }
}