using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FormatRepository : EfRepositoryBase<Format, Guid, BaseDbContext>, IFormatRepository
{
    public FormatRepository(BaseDbContext context) : base(context)
    {
    }
}