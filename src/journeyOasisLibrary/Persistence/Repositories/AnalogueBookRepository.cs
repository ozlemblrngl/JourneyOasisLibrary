using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AnalogueBookRepository : EfRepositoryBase<AnalogueBook, Guid, BaseDbContext>, IAnalogueBookRepository
{
    public AnalogueBookRepository(BaseDbContext context) : base(context)
    {
    }
}