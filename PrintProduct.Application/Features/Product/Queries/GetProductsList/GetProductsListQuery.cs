using MediatR;
using PrintProduct.Application.Contracts.Persistence;
using PrintProduct.Application.Features.Product.Dtos;
using PrintProduct.Application.Features.Product.Queries.GetProductById;

namespace PrintProduct.Application.Features.Product.Queries.GetProductsList;
public record GetProductsListQuery() : IRequest<IEnumerable<ProductDto>>;