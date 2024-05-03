using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Subjects;

public class SubjectManager : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly SubjectBusinessRules _subjectBusinessRules;

    public SubjectManager(ISubjectRepository subjectRepository, SubjectBusinessRules subjectBusinessRules)
    {
        _subjectRepository = subjectRepository;
        _subjectBusinessRules = subjectBusinessRules;
    }

    public async Task<Subject?> GetAsync(
        Expression<Func<Subject, bool>> predicate,
        Func<IQueryable<Subject>, IIncludableQueryable<Subject, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Subject? subject = await _subjectRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return subject;
    }

    public async Task<IPaginate<Subject>?> GetListAsync(
        Expression<Func<Subject, bool>>? predicate = null,
        Func<IQueryable<Subject>, IOrderedQueryable<Subject>>? orderBy = null,
        Func<IQueryable<Subject>, IIncludableQueryable<Subject, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Subject> subjectList = await _subjectRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return subjectList;
    }

    public async Task<Subject> AddAsync(Subject subject)
    {
        Subject addedSubject = await _subjectRepository.AddAsync(subject);

        return addedSubject;
    }

    public async Task<Subject> UpdateAsync(Subject subject)
    {
        Subject updatedSubject = await _subjectRepository.UpdateAsync(subject);

        return updatedSubject;
    }

    public async Task<Subject> DeleteAsync(Subject subject, bool permanent = false)
    {
        Subject deletedSubject = await _subjectRepository.DeleteAsync(subject);

        return deletedSubject;
    }
}
