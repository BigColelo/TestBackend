using Backend.Features.Employees.BL;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi necessari per i controller
builder.Services.AddControllers();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CustomersListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EmployeesListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SupplierListQueryHandler>());
builder.Services.AddScoped<IExportService, ExportService>();

// Setup Database
var connectionString = builder.Configuration.GetConnectionString("Backend") ?? throw new ArgumentNullException("Backend Connectionsting not set");
builder.Services.AddDbContext<BackendContext>(x => x.UseSqlite(connectionString));

// Build app
var app = builder.Build();

app.InitAndSeedBackendContest();

// Register Swagger UI
app.UseSwaggerDocumentation();

// Configura la pipeline di richiesta HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Register all the routes for the api
app.MapControllers();

// Run the application
app.Run();
