using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LanguageBookRepository : EfRepositoryBase<LanguageBook, Guid, BaseDbContext>, ILanguageBookRepository
{
    public LanguageBookRepository(BaseDbContext context) : base(context)
    {
    }
}