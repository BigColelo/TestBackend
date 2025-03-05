class SupplierSeeding(BackendContext context) : SeedEntity<BackendContext, Supplier>(context)
{
    protected override IEnumerable<Supplier> GetSeedItems()
    {
        for (var i = 0; i < 10; i++)
        {
            var faker = new Faker("it");
            yield return new Supplier
            {
                Name = faker.Company.CompanyName(),
                Address = faker.Address.FullAddress(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
            };
        }
    }
}