using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Book : Entity<Guid>
{
    public Guid MaterialId { get; set; }

    public string Name { get; set; }

    public string Status { get; set; }

    public string Isbn { get; set; }

    public string Edition { get; set; }

    public string ReleaseDate { get; set; }

    public string PhysicalInfo { get; set; }

    public string Note { get; set; }

    public virtual Material? Material { get; set; }

    public ICollection<LanguageBook> LanguageBooks { get; set; }
    public ICollection<PublisherBook> PublisherBooks { get; set; }
    public ICollection<SubjectBook> SubjectBooks { get; set; }
    public ICollection<TranslatorBook> TranslatorBooks { get; set; }
    public ICollection<WriterBook> WriterBooks { get; set; }
    public ICollection<BookFormat> BookFormats { get; set; }
}


