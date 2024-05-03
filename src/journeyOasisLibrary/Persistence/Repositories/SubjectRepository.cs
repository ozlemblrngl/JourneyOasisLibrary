using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubjectRepository : EfRepositoryBase<Subject, Guid, BaseDbContext>, ISubjectRepository
{
    public SubjectRepository(BaseDbContext context) : base(context)
    {
    }
}