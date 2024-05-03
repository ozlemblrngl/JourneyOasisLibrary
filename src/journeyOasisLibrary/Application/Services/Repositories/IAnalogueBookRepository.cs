using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAnalogueBookRepository : IAsyncRepository<AnalogueBook, Guid>, IRepository<AnalogueBook, Guid>
{
}