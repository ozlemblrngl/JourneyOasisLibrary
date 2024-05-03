using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AnalogueBookConfiguration : IEntityTypeConfiguration<AnalogueBook>
{
    public void Configure(EntityTypeBuilder<AnalogueBook> builder)
    {
        builder.ToTable("AnalogueBooks").HasKey(ab => ab.Id);

        builder.Property(ab => ab.Id).HasColumnName("Id").IsRequired();
        builder.Property(ab => ab.BookFormatId).HasColumnName("BookFormatId");
        builder.Property(ab => ab.BookFormat).HasColumnName("BookFormat");
        builder.Property(ab => ab.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ab => ab.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ab => ab.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ab => !ab.DeletedDate.HasValue);
    }
}