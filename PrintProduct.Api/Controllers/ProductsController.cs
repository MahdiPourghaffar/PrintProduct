using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintProduct.Application.Features.Product.Commands.AddProduct;
using PrintProduct.Application.Features.Product.Commands.UpdateProduct;
using PrintProduct.Application.Features.Product.Dtos;
using PrintProduct.Application.Features.Product.Queries.GetProductById;
using PrintProduct.Application.Features.Product.Queries.GetProductsList;

namespace PrintProduct.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var products = await _mediator.Send(new GetProductsListQuery());
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdate([FromBody] ProductDto productDto)
    {
        int id = 0;

        if(productDto.Id is not 0)
            id = await _mediator.Send(new AddProductCommand(productDto));
        else
            id = await _mediator.Send(new UpdateProductCommand(productDto.Id, productDto));

        return Ok(id);
    }
}