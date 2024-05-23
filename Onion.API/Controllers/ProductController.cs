using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Features.Products.Commands.CreateProduct;
using Onion.Application.Features.Products.Commands.DeleteProduct;
using Onion.Application.Features.Products.Commands.UpdateProduct;
using Onion.Application.Features.Products.Queries.GetAllProducts;

namespace Onion.API.Controllers;
//action koymazsak eğer HttpGet içerisine ("GetAllProducts") yazmamız lazım.
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;

    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var response = await mediator.Send(new GetAllProductsQueryRequest());

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
    {
        await mediator.Send(request);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
    {
        await mediator.Send(request);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
    {
        await mediator.Send(request);

        return Ok();
    }
}

