 
using AutoMapper;
using EventBus.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<string>
    {        
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IProductFeatureRepository productFeatureRepository,
            IMapper mapper,
            ILogger<CreateProductCommandHandler> logger)
        {           
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productFeatureRepository = productFeatureRepository ?? throw new ArgumentNullException(nameof(productFeatureRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var enabled = await _productFeatureRepository.IsEnabled(request.Email, "Create");

            if (!enabled) throw new NotSupportedException("Create product feature is not evailable.");

            var product = _mapper.Map<Domain.Entities.Product>(request);
            await _productRepository.CreateProduct(product);

            _logger.LogInformation($"Product {product.Id} is successfully created.");

            return product.Id;
        }
    }
}
