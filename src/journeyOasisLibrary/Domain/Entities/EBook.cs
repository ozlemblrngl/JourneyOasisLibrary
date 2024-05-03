using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class EBook : Entity<Guid>
{
    public Guid BookFormatId { get; set; }
    public virtual BookFormat? BookFormat { get; set; }

}
