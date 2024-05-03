using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TranslatorBookConfiguration : IEntityTypeConfiguration<TranslatorBook>
{
    public void Configure(EntityTypeBuilder<TranslatorBook> builder)
    {
        builder.ToTable("TranslatorBooks").HasKey(tb => tb.Id);

        builder.Property(tb => tb.Id).HasColumnName("Id").IsRequired();
        builder.Property(tb => tb.BookId).HasColumnName("BookId");
        builder.Property(tb => tb.TranslatorId).HasColumnName("TranslatorId");
        builder.Property(tb => tb.Book).HasColumnName("Book");
        builder.Property(tb => tb.Translator).HasColumnName("Translator");
        builder.Property(tb => tb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(tb => tb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(tb => tb.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(tb => !tb.DeletedDate.HasValue);
    }
}