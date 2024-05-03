using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class LanguageBook : Entity<Guid>
{
    public Guid BookId { get; set; }
    public Guid LanguageId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Language? Language { get; set; }
}
