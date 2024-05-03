using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class PublisherBook : Entity<Guid>
{
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Publisher? Publisher { get; set; }

}
