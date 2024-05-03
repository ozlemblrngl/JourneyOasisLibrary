using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EBookRepository : EfRepositoryBase<EBook, Guid, BaseDbContext>, IEBookRepository
{
    public EBookRepository(BaseDbContext context) : base(context)
    {
    }
}