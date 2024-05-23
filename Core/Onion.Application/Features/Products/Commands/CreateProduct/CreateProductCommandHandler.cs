using MediatR;
using Onion.Application.Interfaces.UnitOfWorks;
using Onion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
{
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Product product = new Product(request.Title, request.Description, request.BrandId, request.Price, request.Discount);

        await unitOfWork.GetWriteRepository<Product>().AddAsync(product);

        if(await unitOfWork.SaveAsync() > 0)
        {
            foreach (var item in request.CategoryIds)
            {
                await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = item
                });
            }

            await unitOfWork.SaveAsync();
        }
    }
}
