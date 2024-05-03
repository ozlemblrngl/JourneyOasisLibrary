using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILanguageBookRepository : IAsyncRepository<LanguageBook, Guid>, IRepository<LanguageBook, Guid>
{
}