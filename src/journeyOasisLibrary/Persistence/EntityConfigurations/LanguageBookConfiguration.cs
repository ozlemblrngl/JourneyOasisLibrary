using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LanguageBookConfiguration : IEntityTypeConfiguration<LanguageBook>
{
    public void Configure(EntityTypeBuilder<LanguageBook> builder)
    {
        builder.ToTable("LanguageBooks").HasKey(lb => lb.Id);

        builder.Property(lb => lb.Id).HasColumnName("Id").IsRequired();
        builder.Property(lb => lb.BookId).HasColumnName("BookId");
        builder.Property(lb => lb.LanguageId).HasColumnName("LanguageId");
        builder.Property(lb => lb.Book).HasColumnName("Book");
        builder.Property(lb => lb.Language).HasColumnName("Language");
        builder.Property(lb => lb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lb => lb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lb => lb.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lb => !lb.DeletedDate.HasValue);
    }
}