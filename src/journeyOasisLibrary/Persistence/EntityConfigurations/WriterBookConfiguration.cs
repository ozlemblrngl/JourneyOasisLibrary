using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WriterBookConfiguration : IEntityTypeConfiguration<WriterBook>
{
    public void Configure(EntityTypeBuilder<WriterBook> builder)
    {
        builder.ToTable("WriterBooks").HasKey(wb => wb.Id);

        builder.Property(wb => wb.Id).HasColumnName("Id").IsRequired();
        builder.Property(wb => wb.BookId).HasColumnName("BookId");
        builder.Property(wb => wb.WriterId).HasColumnName("WriterId");
        builder.Property(wb => wb.Book).HasColumnName("Book");
        builder.Property(wb => wb.Writer).HasColumnName("Writer");
        builder.Property(wb => wb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(wb => wb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(wb => wb.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(wb => !wb.DeletedDate.HasValue);
    }
}