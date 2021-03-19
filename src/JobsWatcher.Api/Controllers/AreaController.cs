using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Exceptions.Subscription;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AreaController> _logger;

        public AreaController(IAreaService areaService, IMapper mapper, ILogger<AreaController> logger)
        {
            _areaService = areaService;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SubscriptionDto>>> GetAllSubscriptions()
        {
            try
            {
                var areas = await _areaService.GetAreasAsync();
                var response = _mapper.Map<List<AreaDto>>(areas);
                return Ok(response);
            }
            catch (SubscriptionServiceException exception)
            {
                return Problem(exception.Message);
            }
        }
    }
}