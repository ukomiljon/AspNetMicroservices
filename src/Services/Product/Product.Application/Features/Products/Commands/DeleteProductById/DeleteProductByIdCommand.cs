 
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Exceptions;
using Product.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands.DeleteProductById
{

    public class DeleteProductByIdCommand : IRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }


    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductFeatureRepository _productFeatureRepository;

        public DeleteProductByIdCommandHandler(
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

        public async Task<Unit> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var enabled = await _productFeatureRepository.IsEnabled(command.Email, "Delete");

            if (!enabled) throw new NotSupportedException("Deelet product feature is not evailable.");

            var product = await _productRepository.GetProduct(command.Id);
            if (product == null)
            {
                throw new ApiException($"Product Not Found.");
            }

            await _productRepository.DeleteProduct(product.Id); 

            _logger.LogInformation($"Product {product.Id} is successfully deleted.");

            return Unit.Value;
        }
    }

}
