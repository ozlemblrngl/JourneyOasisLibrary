using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BookFormatRepository : EfRepositoryBase<BookFormat, Guid, BaseDbContext>, IBookFormatRepository
{
    public BookFormatRepository(BaseDbContext context) : base(context)
    {
    }
}