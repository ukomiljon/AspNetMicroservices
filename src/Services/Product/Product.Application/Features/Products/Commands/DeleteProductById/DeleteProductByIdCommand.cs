 
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
    }


    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
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
