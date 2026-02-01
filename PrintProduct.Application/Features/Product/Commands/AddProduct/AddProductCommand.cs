using MediatR;
using PrintProduct.Application.Features.Product.Dtos;

namespace PrintProduct.Application.Features.Product.Commands.AddProduct;

public record AddProductCommand(ProductDto Product) : IRequest<int>;