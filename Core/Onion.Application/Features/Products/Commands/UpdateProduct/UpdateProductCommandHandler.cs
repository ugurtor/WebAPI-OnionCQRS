﻿using MediatR;
using Onion.Application.Interfaces.AutoMapper;
using Onion.Application.Interfaces.UnitOfWorks;
using Onion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

        var map = mapper.Map<Product, UpdateProductCommandRequest>(request);

        var productCategories = await unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(x => x.ProductId == product.Id);

        await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

        foreach (var item in request.CategoryIds)
        {
            await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory { CategoryId = item, ProductId = product.Id });
        }

        await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
        await unitOfWork.SaveAsync();
    }
}
