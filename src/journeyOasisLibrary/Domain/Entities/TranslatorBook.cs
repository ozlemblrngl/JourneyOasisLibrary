using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class TranslatorBook : Entity<Guid>
{
    public Guid BookId { get; set; }
    public Guid TranslatorId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Translator? Translator { get; set; }
}
