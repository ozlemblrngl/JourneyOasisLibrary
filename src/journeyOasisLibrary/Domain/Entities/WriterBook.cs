using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class WriterBook : Entity<Guid>
{
    public Guid BookId { get; set; }
    public Guid WriterId { get; set; }

    public virtual Book? Book { get; set; }
    public virtual Writer? Writer { get; set; }
}
