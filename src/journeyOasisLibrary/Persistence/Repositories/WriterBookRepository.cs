using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WriterBookRepository : EfRepositoryBase<WriterBook, Guid, BaseDbContext>, IWriterBookRepository
{
    public WriterBookRepository(BaseDbContext context) : base(context)
    {
    }
}