
using AutoMapper;
using EventBus.Messages;
using FeatureSwitch.API.Dto;
using FeatureSwitch.API.Models;
using FeatureSwitch.API.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureSwitch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly ILogger<FeatureController> _logger;
        private readonly ISwitchRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;


        public FeatureController(ISwitchRepository repository, ILogger<FeatureController> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<FeatureResponse>> Get(string email, string featureName)
        {
            var foundFeature = await _repository.Get(email, featureName);
            if (foundFeature == null)
            {
                return Ok(new FeatureResponse { CanAccess = false });
            }

            return Ok(new FeatureResponse { CanAccess = foundFeature.Enable });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ActionResult> Post(FeatureRequest request)
        {
            var foundFeature = await _repository.Get(request.Email, request.FeatureName);

            if (foundFeature==null)
            {                 
                await _repository.Create(_mapper.Map<Switch>(request));
                return Ok();
            }

            var saved = await _repository.Update(_mapper.Map<Switch>(request));
            await _publishEndpoint.Publish<SwitchFeatureEvent>(_mapper.Map<SwitchFeatureEvent>(request));

            return saved ? Ok() : StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
