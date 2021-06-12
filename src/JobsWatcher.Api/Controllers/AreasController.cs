using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Exceptions.Area;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;

        public AreasController(IAreaService areaService, IMapper mapper)
        {
            _areaService = areaService;
            _mapper = mapper;
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