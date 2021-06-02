using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Application.Features.Products.Commands.DeleteProductById;
using Product.Application.Features.Products.Queries.GetAllProducts;
using Product.Application.Features.Products.Queries.GetProductById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Product.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
                
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductViewModel>> Get(string id, string email)
        {
            var query = new GetProductByIdQuery() { Id = id, Email = email };
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(string id, string email)
        {
            var command = new DeleteProductByIdCommand() { Id = id, Email= email };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ActionResult> Post(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]       
        public async Task<ActionResult<IEnumerable<GetProductViewModel>>> GetAll()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }
    }
}
