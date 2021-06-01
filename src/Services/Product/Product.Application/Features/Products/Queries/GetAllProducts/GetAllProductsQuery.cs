 
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<GetProductViewModel>>
    {
       
    }

    public class GetProductViewModel
    {
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetProductViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));            
        }

        public async Task<IEnumerable<GetProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _productRepository.GetProducts();
            return _mapper.Map<List<GetProductViewModel>>(orderList);
        }


    }
}
