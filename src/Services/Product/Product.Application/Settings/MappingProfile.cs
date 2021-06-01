using AutoMapper;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Application.Features.Products.Queries.GetAllProducts;

namespace Product.Application.Features.Products.Commands.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {           
            CreateMap<CreateProductCommand, Domain.Entities.Product>().ReverseMap();
            CreateMap<GetProductViewModel, Domain.Entities.Product>().ReverseMap();

            
        }
    }
}
