class EmployeeSeeding : SeedEntity<BackendContext, Employee>
{
    readonly List<int?> DepartmentIdList;

    public EmployeeSeeding(BackendContext context) : base(context)
    {
        DepartmentIdList = context.Departments.Select(q => (int?)q.Id).ToList();
        DepartmentIdList.Add(null);
    }

    protected override IEnumerable<Employee> GetSeedItems()
    {
        for (var i = 0; i < 500; i++)
        {
            var faker = new Faker("it");
            yield return new Employee
            {
                Code = faker.Random.AlphaNumeric(10).ToUpper(),
                FirstName = faker.Person.FirstName,
                LastName = faker.Person.LastName,
                Address = faker.Address.FullAddress(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
                DepartmentId = faker.PickRandom(DepartmentIdList),
            };
        }
    }
}