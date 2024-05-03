using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.AnalogueBooks.Constants;
using Application.Features.Books.Constants;
using Application.Features.BookFormats.Constants;
using Application.Features.EBooks.Constants;
using Application.Features.Formats.Constants;
using Application.Features.Languages.Constants;
using Application.Features.LanguageBooks.Constants;
using Application.Features.Libraries.Constants;
using Application.Features.Materials.Constants;
using Application.Features.Publishers.Constants;
using Application.Features.PublisherBooks.Constants;
using Application.Features.Subjects.Constants;
using Application.Features.SubjectBooks.Constants;
using Application.Features.Translators.Constants;
using Application.Features.TranslatorBooks.Constants;
using Application.Features.Writers.Constants;
using Application.Features.WriterBooks.Constants;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region AnalogueBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AnalogueBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = AnalogueBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = AnalogueBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = AnalogueBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = AnalogueBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = AnalogueBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region BookFormats
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BookFormatsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BookFormatsOperationClaims.Read },
                new() { Id = ++lastId, Name = BookFormatsOperationClaims.Write },
                new() { Id = ++lastId, Name = BookFormatsOperationClaims.Create },
                new() { Id = ++lastId, Name = BookFormatsOperationClaims.Update },
                new() { Id = ++lastId, Name = BookFormatsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region EBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = EBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = EBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = EBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = EBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = EBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = EBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Formats
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FormatsOperationClaims.Admin },
                new() { Id = ++lastId, Name = FormatsOperationClaims.Read },
                new() { Id = ++lastId, Name = FormatsOperationClaims.Write },
                new() { Id = ++lastId, Name = FormatsOperationClaims.Create },
                new() { Id = ++lastId, Name = FormatsOperationClaims.Update },
                new() { Id = ++lastId, Name = FormatsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Languages
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LanguagesOperationClaims.Admin },
                new() { Id = ++lastId, Name = LanguagesOperationClaims.Read },
                new() { Id = ++lastId, Name = LanguagesOperationClaims.Write },
                new() { Id = ++lastId, Name = LanguagesOperationClaims.Create },
                new() { Id = ++lastId, Name = LanguagesOperationClaims.Update },
                new() { Id = ++lastId, Name = LanguagesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region LanguageBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LanguageBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = LanguageBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = LanguageBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = LanguageBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = LanguageBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = LanguageBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Libraries
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Admin },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Read },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Write },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Create },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Update },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Materials
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = MaterialsOperationClaims.Admin },
                new() { Id = ++lastId, Name = MaterialsOperationClaims.Read },
                new() { Id = ++lastId, Name = MaterialsOperationClaims.Write },
                new() { Id = ++lastId, Name = MaterialsOperationClaims.Create },
                new() { Id = ++lastId, Name = MaterialsOperationClaims.Update },
                new() { Id = ++lastId, Name = MaterialsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Publishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region PublisherBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublisherBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublisherBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = PublisherBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = PublisherBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = PublisherBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = PublisherBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Subjects
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = SubjectsOperationClaims.Admin },
                new() { Id = ++lastId, Name = SubjectsOperationClaims.Read },
                new() { Id = ++lastId, Name = SubjectsOperationClaims.Write },
                new() { Id = ++lastId, Name = SubjectsOperationClaims.Create },
                new() { Id = ++lastId, Name = SubjectsOperationClaims.Update },
                new() { Id = ++lastId, Name = SubjectsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region SubjectBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = SubjectBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = SubjectBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = SubjectBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = SubjectBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = SubjectBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = SubjectBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Translators
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = TranslatorsOperationClaims.Admin },
                new() { Id = ++lastId, Name = TranslatorsOperationClaims.Read },
                new() { Id = ++lastId, Name = TranslatorsOperationClaims.Write },
                new() { Id = ++lastId, Name = TranslatorsOperationClaims.Create },
                new() { Id = ++lastId, Name = TranslatorsOperationClaims.Update },
                new() { Id = ++lastId, Name = TranslatorsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region TranslatorBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = TranslatorBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = TranslatorBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = TranslatorBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = TranslatorBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = TranslatorBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = TranslatorBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Writers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = WritersOperationClaims.Admin },
                new() { Id = ++lastId, Name = WritersOperationClaims.Read },
                new() { Id = ++lastId, Name = WritersOperationClaims.Write },
                new() { Id = ++lastId, Name = WritersOperationClaims.Create },
                new() { Id = ++lastId, Name = WritersOperationClaims.Update },
                new() { Id = ++lastId, Name = WritersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region WriterBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = WriterBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = WriterBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = WriterBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = WriterBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = WriterBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = WriterBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
