class DepartmentSeeding(BackendContext context) : SeedEntity<BackendContext, Department>(context)
{
    protected override IEnumerable<Department> GetSeedItems()
    {
        for (var i = 0; i < 5; i++)
        {
            var faker = new Faker("it");
            yield return new Department
            {
                Code = faker.Random.AlphaNumeric(3).ToUpper(),
                Description = faker.Commerce.Department(1),
            };
        }
    }
}