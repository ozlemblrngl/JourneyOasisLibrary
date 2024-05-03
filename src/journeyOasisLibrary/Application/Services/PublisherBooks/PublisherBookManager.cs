using Application.Features.PublisherBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PublisherBooks;

public class PublisherBookManager : IPublisherBookService
{
    private readonly IPublisherBookRepository _publisherBookRepository;
    private readonly PublisherBookBusinessRules _publisherBookBusinessRules;

    public PublisherBookManager(IPublisherBookRepository publisherBookRepository, PublisherBookBusinessRules publisherBookBusinessRules)
    {
        _publisherBookRepository = publisherBookRepository;
        _publisherBookBusinessRules = publisherBookBusinessRules;
    }

    public async Task<PublisherBook?> GetAsync(
        Expression<Func<PublisherBook, bool>> predicate,
        Func<IQueryable<PublisherBook>, IIncludableQueryable<PublisherBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        PublisherBook? publisherBook = await _publisherBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return publisherBook;
    }

    public async Task<IPaginate<PublisherBook>?> GetListAsync(
        Expression<Func<PublisherBook, bool>>? predicate = null,
        Func<IQueryable<PublisherBook>, IOrderedQueryable<PublisherBook>>? orderBy = null,
        Func<IQueryable<PublisherBook>, IIncludableQueryable<PublisherBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<PublisherBook> publisherBookList = await _publisherBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return publisherBookList;
    }

    public async Task<PublisherBook> AddAsync(PublisherBook publisherBook)
    {
        PublisherBook addedPublisherBook = await _publisherBookRepository.AddAsync(publisherBook);

        return addedPublisherBook;
    }

    public async Task<PublisherBook> UpdateAsync(PublisherBook publisherBook)
    {
        PublisherBook updatedPublisherBook = await _publisherBookRepository.UpdateAsync(publisherBook);

        return updatedPublisherBook;
    }

    public async Task<PublisherBook> DeleteAsync(PublisherBook publisherBook, bool permanent = false)
    {
        PublisherBook deletedPublisherBook = await _publisherBookRepository.DeleteAsync(publisherBook);

        return deletedPublisherBook;
    }
}
