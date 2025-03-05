internal class CustomersListQueryHandler(BackendContext context) : IRequestHandler<CustomersListQuery, List<CustomersListQueryResponse>>
{
    private readonly BackendContext _context = context;

    public async Task<List<CustomersListQueryResponse>> Handle(CustomersListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers.AsQueryable();

        // Filtro per SearchText sui campi Name ed Email
        if (!string.IsNullOrEmpty(request.SearchText))
        {
            //var lowerSearchText = request.SearchText.ToLower();
            query = query.Where(c => c.Name.ToLower().StartsWith(request.SearchText.ToLower()) || c.Email.ToLower().StartsWith(request.SearchText.ToLower()));
        }

        // Ordinamento dinamico in base a SortBy
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            if (request.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                query = query.OrderBy(c => c.Name);
            else if (request.SortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                query = query.OrderBy(c => c.Email);

        }
        else
        {
            query = query.OrderBy(c => c.Id);
        }

        // Gestione del valore null per Skip e Take
        int skipValue = request.Skip ?? 0; // Se Skip è null, usa 0 come valore predefinito
        int takeValue = request.Take ?? 20; // Se Take è null, usa 20 come valore predefinito

        // Paginazione
        query = query.Skip(skipValue).Take(takeValue);

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