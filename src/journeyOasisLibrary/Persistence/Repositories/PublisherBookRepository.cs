using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PublisherBookRepository : EfRepositoryBase<PublisherBook, Guid, BaseDbContext>, IPublisherBookRepository
{
    public PublisherBookRepository(BaseDbContext context) : base(context)
    {
    }
}