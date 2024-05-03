using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Subject : Entity<Guid>
{
    public string Name { get; set; }

    public ICollection<SubjectBook> SubjectBooks { get; set; }
}
