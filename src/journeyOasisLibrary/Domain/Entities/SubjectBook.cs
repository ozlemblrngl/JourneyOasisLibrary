using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class SubjectBook : Entity<Guid>
{
    public Guid BookId { get; set; }

    public Guid SubjectId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Subject? Subject { get; set; }
}
