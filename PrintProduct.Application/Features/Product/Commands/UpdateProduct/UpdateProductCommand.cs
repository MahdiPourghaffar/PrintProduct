using MediatR;
using PrintProduct.Application.Features.Product.Dtos;

namespace PrintProduct.Application.Features.Product.Commands.UpdateProduct;

public record UpdateProductCommand(int Id, ProductDto Product) : IRequest<int>;