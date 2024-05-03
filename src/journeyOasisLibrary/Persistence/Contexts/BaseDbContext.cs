using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<AnalogueBook> AnalogueBooks { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookFormat> BookFormats { get; set; }
    public DbSet<EBook> EBooks { get; set; }
    public DbSet<Format> Formats { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<LanguageBook> LanguageBooks { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<PublisherBook> PublisherBooks { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectBook> SubjectBooks { get; set; }
    public DbSet<Translator> Translators { get; set; }
    public DbSet<TranslatorBook> TranslatorBooks { get; set; }
    public DbSet<Writer> Writers { get; set; }
    public DbSet<WriterBook> WriterBooks { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
