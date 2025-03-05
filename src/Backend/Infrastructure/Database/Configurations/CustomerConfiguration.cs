public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Address).HasMaxLength(200);
        builder.Property(s => s.Email).HasMaxLength(50);
        builder.Property(s => s.Phone).HasMaxLength(20);
        builder.Property(s => s.Iban).HasMaxLength(34);
        builder.HasOne(q => q.CustomerCategory).WithMany().HasForeignKey(q => q.CustomerCategoryId);
    }
}