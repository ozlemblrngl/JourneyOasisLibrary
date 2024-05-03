using NArchitecture.Core.Application.Responses;

namespace Application.Features.LanguageBooks.Commands.Delete;

public class DeletedLanguageBookResponse : IResponse
{
    public Guid Id { get; set; }
}