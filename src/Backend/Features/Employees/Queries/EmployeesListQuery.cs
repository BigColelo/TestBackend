namespace Backend.Features.Employees;

public class EmployeesListQuery : IRequest<List<EmployeesListQueryResponse>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}