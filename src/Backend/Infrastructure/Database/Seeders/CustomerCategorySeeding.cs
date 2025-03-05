class CustomerCategorySeeding(BackendContext context) : SeedEntity<BackendContext, CustomerCategory>(context)
{
    protected override IEnumerable<CustomerCategory> GetSeedItems()
    {
        for (var i = 0; i < 7; i++)
        {
            var faker = new Faker("it");
            yield return new CustomerCategory
            {
                Code = faker.Random.AlphaNumeric(5).ToUpper(),
                Description = faker.Commerce.Department(1),
            };
        }
    }
}