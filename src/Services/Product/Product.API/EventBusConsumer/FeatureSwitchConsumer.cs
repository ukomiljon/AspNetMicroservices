using AutoMapper;
using EventBus.Messages;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.EventBusConsumer
{
    public class FeatureSwitchConsumer : IConsumer<SwitchFeatureEvent>
    {       
        //private readonly IMapper _mapper;
        //private readonly ILogger<FeatureSwitchConsumer> _logger;

        //public FeatureSwitchConsumer(IMediator mediator, IMapper mapper, ILogger<FeatureSwitchConsumer> logger)
        //{           
        //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        public async Task Consume(ConsumeContext<SwitchFeatureEvent> context)
        {
            //var command = _mapper.Map<UpdateFeatureCommand>(context.Message);
            //var result = await _mediator.Send(command);

            //_logger.LogInformation("SwitchFeatureEvent consumed successfully. Created Feature Updated ", context.Message);
            //send to client            
        }
    }
}
