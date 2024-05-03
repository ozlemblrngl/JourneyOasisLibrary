using Application.Features.EBooks.Constants;
using Application.Features.EBooks.Constants;
using Application.Features.EBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.EBooks.Constants.EBooksOperationClaims;

namespace Application.Features.EBooks.Commands.Delete;

public class DeleteEBookCommand : IRequest<DeletedEBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, EBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEBooks"];

    public class DeleteEBookCommandHandler : IRequestHandler<DeleteEBookCommand, DeletedEBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEBookRepository _eBookRepository;
        private readonly EBookBusinessRules _eBookBusinessRules;

        public DeleteEBookCommandHandler(IMapper mapper, IEBookRepository eBookRepository,
                                         EBookBusinessRules eBookBusinessRules)
        {
            _mapper = mapper;
            _eBookRepository = eBookRepository;
            _eBookBusinessRules = eBookBusinessRules;
        }

        public async Task<DeletedEBookResponse> Handle(DeleteEBookCommand request, CancellationToken cancellationToken)
        {
            EBook? eBook = await _eBookRepository.GetAsync(predicate: eb => eb.Id == request.Id, cancellationToken: cancellationToken);
            await _eBookBusinessRules.EBookShouldExistWhenSelected(eBook);

            await _eBookRepository.DeleteAsync(eBook!);

            DeletedEBookResponse response = _mapper.Map<DeletedEBookResponse>(eBook);
            return response;
        }
    }
}