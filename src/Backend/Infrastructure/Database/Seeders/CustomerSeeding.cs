class CustomerSeeding : SeedEntity<BackendContext, Customer>
{
    readonly List<int?> CustomerCategoryIdList;

    public CustomerSeeding(BackendContext context) : base(context)
    {
        CustomerCategoryIdList = context.CustomerCategories.Select(q => (int?)q.Id).ToList();
        CustomerCategoryIdList.Add(null);
    }

    protected override IEnumerable<Customer> GetSeedItems()
    {
        for (var i = 0; i < 500; i++)
        {
            var faker = new Faker("it");
            yield return new Customer
            {
                Name = faker.Company.CompanyName(),
                Address = faker.Address.FullAddress(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
                Iban = faker.Finance.Iban(),
                CustomerCategoryId = faker.PickRandom(CustomerCategoryIdList),
            };
        }
    }
}