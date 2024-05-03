using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BookFormatConfiguration : IEntityTypeConfiguration<BookFormat>
{
    public void Configure(EntityTypeBuilder<BookFormat> builder)
    {
        builder.ToTable("BookFormats").HasKey(bf => bf.Id);

        builder.Property(bf => bf.Id).HasColumnName("Id").IsRequired();
        builder.Property(bf => bf.BookId).HasColumnName("BookId");
        builder.Property(bf => bf.FormatId).HasColumnName("FormatId");
        builder.Property(bf => bf.Book).HasColumnName("Book");
        builder.Property(bf => bf.Format).HasColumnName("Format");
        builder.Property(bf => bf.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bf => bf.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bf => bf.DeletedDate).HasColumnName("DeletedDate");


        builder.HasQueryFilter(bf => !bf.DeletedDate.HasValue);
    }
}