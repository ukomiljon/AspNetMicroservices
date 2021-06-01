using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.SwitchFeature.Commands.UpdateFeatureStatus
{
    public class UpdateFeatureCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string FeatureName { get; set; }
        public bool Enable { get; set; }
    }

    public class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFeatureCommandHandler> _logger;

        public UpdateFeatureCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateFeatureCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Domain.Entities.Product>(request);
            await _productRepository.CreateProduct(product);

            _logger.LogInformation($"Product {product.Id} is successfully created.");

            return true;
        } 
    }
}
