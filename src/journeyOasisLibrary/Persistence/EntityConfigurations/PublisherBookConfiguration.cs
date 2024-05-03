using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PublisherBookConfiguration : IEntityTypeConfiguration<PublisherBook>
{
    public void Configure(EntityTypeBuilder<PublisherBook> builder)
    {
        builder.ToTable("PublisherBooks").HasKey(pb => pb.Id);

        builder.Property(pb => pb.Id).HasColumnName("Id").IsRequired();
        builder.Property(pb => pb.BookId).HasColumnName("BookId");
        builder.Property(pb => pb.PublisherId).HasColumnName("PublisherId");
        builder.Property(pb => pb.Book).HasColumnName("Book");
        builder.Property(pb => pb.Publisher).HasColumnName("Publisher");
        builder.Property(pb => pb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pb => pb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pb => pb.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pb => !pb.DeletedDate.HasValue);
    }
}