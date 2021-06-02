using AutoMapper;
using EventBus.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Features.FeatureStatus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.EventBusConsumer
{
    public class FeatureSwitchConsumer : IConsumer<SwitchFeatureEvent>
    {
        
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FeatureSwitchConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<SwitchFeatureEvent> context)
        {
            var command = _mapper.Map<SwitchFeatureCommand>(context.Message);
            await _mediator.Send(command); 
            //TODO. using signalR send message to client about enable/disable feature.                       
        }
    }
}
