using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISubjectRepository : IAsyncRepository<Subject, Guid>, IRepository<Subject, Guid>
{
}