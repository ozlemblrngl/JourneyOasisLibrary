using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFormatRepository : IAsyncRepository<Format, Guid>, IRepository<Format, Guid>
{
}