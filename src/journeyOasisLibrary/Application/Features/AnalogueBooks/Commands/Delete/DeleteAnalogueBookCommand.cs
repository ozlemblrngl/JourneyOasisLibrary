using Application.Features.AnalogueBooks.Constants;
using Application.Features.AnalogueBooks.Constants;
using Application.Features.AnalogueBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AnalogueBooks.Constants.AnalogueBooksOperationClaims;

namespace Application.Features.AnalogueBooks.Commands.Delete;

public class DeleteAnalogueBookCommand : IRequest<DeletedAnalogueBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, AnalogueBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAnalogueBooks"];

    public class DeleteAnalogueBookCommandHandler : IRequestHandler<DeleteAnalogueBookCommand, DeletedAnalogueBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnalogueBookRepository _analogueBookRepository;
        private readonly AnalogueBookBusinessRules _analogueBookBusinessRules;

        public DeleteAnalogueBookCommandHandler(IMapper mapper, IAnalogueBookRepository analogueBookRepository,
                                         AnalogueBookBusinessRules analogueBookBusinessRules)
        {
            _mapper = mapper;
            _analogueBookRepository = analogueBookRepository;
            _analogueBookBusinessRules = analogueBookBusinessRules;
        }

        public async Task<DeletedAnalogueBookResponse> Handle(DeleteAnalogueBookCommand request, CancellationToken cancellationToken)
        {
            AnalogueBook? analogueBook = await _analogueBookRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _analogueBookBusinessRules.AnalogueBookShouldExistWhenSelected(analogueBook);

            await _analogueBookRepository.DeleteAsync(analogueBook!);

            DeletedAnalogueBookResponse response = _mapper.Map<DeletedAnalogueBookResponse>(analogueBook);
            return response;
        }
    }
}