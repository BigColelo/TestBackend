using Backend.Features.Employees;
using Backend.Features.Suppliers;
using Backend.Services;


namespace Backend;

static class RouteRegistrationExtensions
{
    public static void UseApiRoutes(this WebApplication app)
    {
        var apiGroup = app.MapGroup("api");

        apiGroup.MapGet("suppliers/list", async ([AsParameters] SupplierListQuery query, IMediator mediator) => await mediator.Send(query))
                    .WithName("GetSuppliersList")
                    .WithOpenApi();

        apiGroup.MapGet("employees/list", async ([AsParameters] EmployeesListQuery query, IMediator mediator) => await mediator.Send(query))
                    .WithName("GetEmployeesList")
                    .WithOpenApi();
        
        apiGroup.MapGet("customers/list", async ([AsParameters] CustomersListQuery query, IMediator mediator) => await mediator.Send(query))
                    .WithName("GetCustomersList")
                    .WithOpenApi();
        
        apiGroup.MapGet("customers/export", async (
            [Microsoft.AspNetCore.Mvc.FromServices] IMediator mediator, // Specifica che mediator è un servizio iniettato
            [Microsoft.AspNetCore.Mvc.FromServices] IExportService exportService // Specifica che exportService è un servizio iniettato
        ) =>
        {
            var query = new CustomersListQuery();
            var customers = await mediator.Send(query);
            var stream = exportService.ExportToXml(customers, "Customers");
            return Results.File(stream, "application/xml", "customers.xml");
        })
        .WithName("ExportCustomers")
        .WithOpenApi();
    }
}
