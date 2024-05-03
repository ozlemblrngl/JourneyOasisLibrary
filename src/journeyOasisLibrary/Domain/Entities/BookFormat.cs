using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class BookFormat : Entity<Guid>
{
    public Guid BookId { get; set; }

    public Guid FormatId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Format? Format { get; set; }
}
