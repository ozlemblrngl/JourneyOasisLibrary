using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TranslatorBookRepository : EfRepositoryBase<TranslatorBook, Guid, BaseDbContext>, ITranslatorBookRepository
{
    public TranslatorBookRepository(BaseDbContext context) : base(context)
    {
    }
}