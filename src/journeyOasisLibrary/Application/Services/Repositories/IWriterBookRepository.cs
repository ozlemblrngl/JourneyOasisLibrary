using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IWriterBookRepository : IAsyncRepository<WriterBook, Guid>, IRepository<WriterBook, Guid>
{
}