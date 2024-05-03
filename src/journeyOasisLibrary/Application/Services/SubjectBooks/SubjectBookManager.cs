using Application.Features.SubjectBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SubjectBooks;

public class SubjectBookManager : ISubjectBookService
{
    private readonly ISubjectBookRepository _subjectBookRepository;
    private readonly SubjectBookBusinessRules _subjectBookBusinessRules;

    public SubjectBookManager(ISubjectBookRepository subjectBookRepository, SubjectBookBusinessRules subjectBookBusinessRules)
    {
        _subjectBookRepository = subjectBookRepository;
        _subjectBookBusinessRules = subjectBookBusinessRules;
    }

    public async Task<SubjectBook?> GetAsync(
        Expression<Func<SubjectBook, bool>> predicate,
        Func<IQueryable<SubjectBook>, IIncludableQueryable<SubjectBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SubjectBook? subjectBook = await _subjectBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return subjectBook;
    }

    public async Task<IPaginate<SubjectBook>?> GetListAsync(
        Expression<Func<SubjectBook, bool>>? predicate = null,
        Func<IQueryable<SubjectBook>, IOrderedQueryable<SubjectBook>>? orderBy = null,
        Func<IQueryable<SubjectBook>, IIncludableQueryable<SubjectBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SubjectBook> subjectBookList = await _subjectBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return subjectBookList;
    }

    public async Task<SubjectBook> AddAsync(SubjectBook subjectBook)
    {
        SubjectBook addedSubjectBook = await _subjectBookRepository.AddAsync(subjectBook);

        return addedSubjectBook;
    }

    public async Task<SubjectBook> UpdateAsync(SubjectBook subjectBook)
    {
        SubjectBook updatedSubjectBook = await _subjectBookRepository.UpdateAsync(subjectBook);

        return updatedSubjectBook;
    }

    public async Task<SubjectBook> DeleteAsync(SubjectBook subjectBook, bool permanent = false)
    {
        SubjectBook deletedSubjectBook = await _subjectBookRepository.DeleteAsync(subjectBook);

        return deletedSubjectBook;
    }
}
