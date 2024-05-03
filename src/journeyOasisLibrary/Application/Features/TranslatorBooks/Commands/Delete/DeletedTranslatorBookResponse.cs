using NArchitecture.Core.Application.Responses;

namespace Application.Features.TranslatorBooks.Commands.Delete;

public class DeletedTranslatorBookResponse : IResponse
{
    public Guid Id { get; set; }
}