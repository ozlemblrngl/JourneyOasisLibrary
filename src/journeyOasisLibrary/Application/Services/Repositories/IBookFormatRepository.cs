using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBookFormatRepository : IAsyncRepository<BookFormat, Guid>, IRepository<BookFormat, Guid>
{
}