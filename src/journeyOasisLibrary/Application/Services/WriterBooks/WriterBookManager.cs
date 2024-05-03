using Application.Features.WriterBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.WriterBooks;

public class WriterBookManager : IWriterBookService
{
    private readonly IWriterBookRepository _writerBookRepository;
    private readonly WriterBookBusinessRules _writerBookBusinessRules;

    public WriterBookManager(IWriterBookRepository writerBookRepository, WriterBookBusinessRules writerBookBusinessRules)
    {
        _writerBookRepository = writerBookRepository;
        _writerBookBusinessRules = writerBookBusinessRules;
    }

    public async Task<WriterBook?> GetAsync(
        Expression<Func<WriterBook, bool>> predicate,
        Func<IQueryable<WriterBook>, IIncludableQueryable<WriterBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        WriterBook? writerBook = await _writerBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return writerBook;
    }

    public async Task<IPaginate<WriterBook>?> GetListAsync(
        Expression<Func<WriterBook, bool>>? predicate = null,
        Func<IQueryable<WriterBook>, IOrderedQueryable<WriterBook>>? orderBy = null,
        Func<IQueryable<WriterBook>, IIncludableQueryable<WriterBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<WriterBook> writerBookList = await _writerBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return writerBookList;
    }

    public async Task<WriterBook> AddAsync(WriterBook writerBook)
    {
        WriterBook addedWriterBook = await _writerBookRepository.AddAsync(writerBook);

        return addedWriterBook;
    }

    public async Task<WriterBook> UpdateAsync(WriterBook writerBook)
    {
        WriterBook updatedWriterBook = await _writerBookRepository.UpdateAsync(writerBook);

        return updatedWriterBook;
    }

    public async Task<WriterBook> DeleteAsync(WriterBook writerBook, bool permanent = false)
    {
        WriterBook deletedWriterBook = await _writerBookRepository.DeleteAsync(writerBook);

        return deletedWriterBook;
    }
}
