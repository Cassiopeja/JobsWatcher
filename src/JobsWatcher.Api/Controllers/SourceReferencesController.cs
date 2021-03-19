using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Exceptions.SourceReference;
using JobsWatcher.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SourceReferencesController : ControllerBase
    {
        private readonly ILogger<SourceReferencesController> _logger;
        private readonly IMapper _mapper;
        private readonly ISourceReferenceService _sourceReferenceService;

        public SourceReferencesController(ILogger<SourceReferencesController> logger, IMapper mapper,
            ISourceReferenceService sourceReferenceService)
        {
            _logger = logger;
            _mapper = mapper;
            _sourceReferenceService = sourceReferenceService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SourceReferenceDto>>> GetAllSourceReferences()
        {
            try
            {
                var references = await _sourceReferenceService.GetAllSourceReferencesAsync();
                var response = _mapper.Map<IEnumerable<SourceReferenceDto>>(references);
                return Ok(response);
            }
            catch (SourceReferenceServiceException exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("{sourceTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SourceReferenceDto>> GetSourceReferenceBySourceTypeId(int sourceTypeId)
        {
            try
            {
                var reference = await _sourceReferenceService.GetSourceReferenceBySourceTypeIdAsync(sourceTypeId);
                var response = _mapper.Map<SourceReferenceDto>(reference);
                return Ok(response);
            }
            catch (SourceReferenceValidationException exception)
                when (exception.InnerException is NotFoundSourceReferenceException)
            {
                var innerMessage = GetInnerMessage(exception);

                return NotFound(innerMessage);
            }
            catch (SourceReferenceValidationException exception)
            {
                var innerMessage = GetInnerMessage(exception);
                return BadRequest(innerMessage);
            }
            catch (SourceReferenceServiceException exception)
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