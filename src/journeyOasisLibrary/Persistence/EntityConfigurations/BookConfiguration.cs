using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.MaterialId).HasColumnName("MaterialId");
        builder.Property(b => b.Name).HasColumnName("Name");
        builder.Property(b => b.Status).HasColumnName("Status");
        builder.Property(b => b.Isbn).HasColumnName("Isbn");
        builder.Property(b => b.Edition).HasColumnName("Edition");
        builder.Property(b => b.ReleaseDate).HasColumnName("ReleaseDate");
        builder.Property(b => b.PhysicalInfo).HasColumnName("PhysicalInfo");
        builder.Property(b => b.Note).HasColumnName("Note");
        builder.Property(b => b.Material).HasColumnName("Material");
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}