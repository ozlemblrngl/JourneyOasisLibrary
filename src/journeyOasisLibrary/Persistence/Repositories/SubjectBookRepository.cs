using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubjectBookRepository : EfRepositoryBase<SubjectBook, Guid, BaseDbContext>, ISubjectBookRepository
{
    public SubjectBookRepository(BaseDbContext context) : base(context)
    {
    }
}