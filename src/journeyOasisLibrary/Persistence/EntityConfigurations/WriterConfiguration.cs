using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WriterConfiguration : IEntityTypeConfiguration<Writer>
{
    public void Configure(EntityTypeBuilder<Writer> builder)
    {
        builder.ToTable("Writers").HasKey(w => w.Id);

        builder.Property(w => w.Id).HasColumnName("Id").IsRequired();
        builder.Property(w => w.FirstName).HasColumnName("FirstName");
        builder.Property(w => w.LastName).HasColumnName("LastName");
        builder.Property(w => w.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(w => w.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(w => w.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(w => !w.DeletedDate.HasValue);
    }
}