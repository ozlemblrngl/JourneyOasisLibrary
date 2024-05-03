using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITranslatorBookRepository : IAsyncRepository<TranslatorBook, Guid>, IRepository<TranslatorBook, Guid>
{
}