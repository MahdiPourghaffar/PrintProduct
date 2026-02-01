using MediatR;
using PrintProduct.Application.Features.Product.Dtos;

namespace PrintProduct.Application.Features.Product.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;