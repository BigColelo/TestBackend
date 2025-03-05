public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Iban { get; set; } = "";

    public int? CustomerCategoryId { get; set; }
    public CustomerCategory? CustomerCategory { get; set; }
}