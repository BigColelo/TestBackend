public class CustomersListQuery : IRequest<List<CustomersListQueryResponse>>
{
    public string? SearchText { get; set; }
    public string? SortBy { get; set; }  // Valori validi: "Name", "Email"
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; }
}