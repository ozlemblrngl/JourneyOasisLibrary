using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Format : Entity<Guid>
{
    public string Name { get; set; }

    public ICollection<BookFormat> BookFormats { get; set; }
}
