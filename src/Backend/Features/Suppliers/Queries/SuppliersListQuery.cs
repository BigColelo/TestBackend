using MediatR;

namespace Backend.Features.Suppliers;


public class SupplierListQuery : IRequest<List<SupplierListQueryResponse>>
{
    public string? Name { get; set; }
}