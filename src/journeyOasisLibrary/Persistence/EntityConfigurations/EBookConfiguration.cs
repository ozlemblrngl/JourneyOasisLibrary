using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EBookConfiguration : IEntityTypeConfiguration<EBook>
{
    public void Configure(EntityTypeBuilder<EBook> builder)
    {
        builder.ToTable("EBooks").HasKey(eb => eb.Id);

        builder.Property(eb => eb.Id).HasColumnName("Id").IsRequired();
        builder.Property(eb => eb.BookFormatId).HasColumnName("BookFormatId");
        builder.Property(eb => eb.BookFormat).HasColumnName("BookFormat");
        builder.Property(eb => eb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(eb => eb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(eb => eb.DeletedDate).HasColumnName("DeletedDate");


        builder.HasQueryFilter(eb => !eb.DeletedDate.HasValue);
    }
}