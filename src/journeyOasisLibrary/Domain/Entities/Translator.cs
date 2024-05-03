using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Translator : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<TranslatorBook> TranslatorBooks { get; set; }
}
