using AutoMapper;
using EventBus.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.FeatureStatus.Commands
{
    public partial class SwitchFeatureCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string FeatureName { get; set; }
        public bool Enable { get; set; }
    }
    public class SwitchFeatureCommandHandler : IRequestHandler<SwitchFeatureCommand, bool>
    {
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SwitchFeatureCommandHandler> _logger;

        public SwitchFeatureCommandHandler(IProductFeatureRepository productFeatureRepository, IMapper mapper, ILogger<SwitchFeatureCommandHandler> logger)
        {
            _productFeatureRepository = productFeatureRepository ?? throw new ArgumentNullException(nameof(productFeatureRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(SwitchFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = _mapper.Map<SwitchFeatureEvent>(request);
            var foundFeature = await _productFeatureRepository.Get(feature.Email, feature.FeatureName);
          
            if (foundFeature == null)            
                await _productFeatureRepository.Create(feature);
            else
                await _productFeatureRepository.Update(feature);

            foundFeature = await _productFeatureRepository.Get(feature.Email, feature.FeatureName);
            _logger.LogInformation($"{feature.FeatureName} status successfully updated to {feature.Enable} for {feature.Email} user.");

            return foundFeature.Enable;
        }
    }
}
