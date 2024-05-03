using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WriterRepository : EfRepositoryBase<Writer, Guid, BaseDbContext>, IWriterRepository
{
    public WriterRepository(BaseDbContext context) : base(context)
    {
    }
}