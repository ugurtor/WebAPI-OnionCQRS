using MediatR;
using Onion.Application.Interfaces.UnitOfWorks;
using Onion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Features.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
        product.IsDeleted = true;

        await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
        await unitOfWork.SaveAsync();
    }
}
