namespace Backend;

static class DatabaseInitializer
{
    public static void InitAndSeedBackendContest(this WebApplication app)
    {
        // Make sure, that the database exists
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<BackendContext>();
        context.Database.EnsureCreated();

        if (app.Environment.IsDevelopment())
            context.Seed();
    }
}
