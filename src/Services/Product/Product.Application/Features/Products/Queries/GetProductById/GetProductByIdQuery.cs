 
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Exceptions;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Application.Features.Products.Queries.GetAllProducts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Queries.GetProductById
{

    public class GetProductByIdQuery : IRequest<GetProductViewModel>
    {
        public string Id { get; set; }

        
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductViewModel>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateProductCommandHandler> _logger;
            public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
            {
                _productRepository = _productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }
            public async Task<GetProductViewModel> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProduct(query.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                return _mapper.Map<GetProductViewModel>(product);
            }
        }
    }
}
