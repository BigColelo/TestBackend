public class CustomersListQuery : IRequest<List<CustomersListQueryResponse>>
{
    public string? SearchText { get; set; }
    public string? SortBy { get; set; }  // Valori validi: "Name", "Email"
    //public bool Descending { get; set; } = false;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;
}

public class CustomersListQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Iban { get; set; } = "";
    public CustomersListQueryResponseCategory? Category { get; set; }
}

public class CustomersListQueryResponseCategory
{
    public string Code { get; set; } = "";
    public string Description { get; set; } = "";
}

internal class CustomersListQueryHandler(BackendContext context) : IRequestHandler<CustomersListQuery, List<CustomersListQueryResponse>>
{
    private readonly BackendContext _context = context;

    public async Task<List<CustomersListQueryResponse>> Handle(CustomersListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers.AsQueryable();

        // Filtro per SearchText sui campi Name ed Email
        if (!string.IsNullOrEmpty(request.SearchText))
        {
            var lowerSearchText = request.SearchText.ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(lowerSearchText) || c.Email.ToLower().Contains(lowerSearchText));
        }

        // Ordinamento dinamico in base a SortBy
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            if (request.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                //query = request.Descending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name);
                query = query.OrderBy(c => c.Name);
            else if (request.SortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                //query = request.Descending ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email);
                query = query.OrderBy(c => c.Email);
            else
                query = query.OrderBy(c => c.Id);
        }
        else
        {
            query = query.OrderBy(c => c.Id);
        }

        // Paginazione
        query = query.Skip(request.Skip).Take(request.Take);

        // Include per caricare la relazione con CustomerCategory
        var data = await query.Include(c => c.CustomerCategory).ToListAsync(cancellationToken);

        var result = new List<CustomersListQueryResponse>();

        foreach (var item in data)
        {
            var resultItem = new CustomersListQueryResponse
            {
                Id = item.Id,
                Name = item.Name,
                Address = item.Address,
                Email = item.Email,
                Phone = item.Phone,
                Iban = item.Iban,
                Category = item.CustomerCategory is not null ? new CustomersListQueryResponseCategory
                {
                    Code = item.CustomerCategory.Code,
                    Description = item.CustomerCategory.Description
                } : null
            };

            result.Add(resultItem);
        }

        return result;
    }
}