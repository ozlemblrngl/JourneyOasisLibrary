using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SubjectBookConfiguration : IEntityTypeConfiguration<SubjectBook>
{
    public void Configure(EntityTypeBuilder<SubjectBook> builder)
    {
        builder.ToTable("SubjectBooks").HasKey(sb => sb.Id);

        builder.Property(sb => sb.Id).HasColumnName("Id").IsRequired();
        builder.Property(sb => sb.BookId).HasColumnName("BookId");
        builder.Property(sb => sb.SubjectId).HasColumnName("SubjectId");
        builder.Property(sb => sb.Book).HasColumnName("Book");
        builder.Property(sb => sb.Subject).HasColumnName("Subject");
        builder.Property(sb => sb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sb => sb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sb => sb.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sb => !sb.DeletedDate.HasValue);
    }
}