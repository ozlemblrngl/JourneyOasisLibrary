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

namespace Application.Features.AnalogueBooks.Commands.Update;

public class UpdateAnalogueBookCommand : IRequest<UpdatedAnalogueBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }

    public string[] Roles => [Admin, Write, AnalogueBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAnalogueBooks"];

    public class UpdateAnalogueBookCommandHandler : IRequestHandler<UpdateAnalogueBookCommand, UpdatedAnalogueBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnalogueBookRepository _analogueBookRepository;
        private readonly AnalogueBookBusinessRules _analogueBookBusinessRules;

        public UpdateAnalogueBookCommandHandler(IMapper mapper, IAnalogueBookRepository analogueBookRepository,
                                         AnalogueBookBusinessRules analogueBookBusinessRules)
        {
            _mapper = mapper;
            _analogueBookRepository = analogueBookRepository;
            _analogueBookBusinessRules = analogueBookBusinessRules;
        }

        public async Task<UpdatedAnalogueBookResponse> Handle(UpdateAnalogueBookCommand request, CancellationToken cancellationToken)
        {
            AnalogueBook? analogueBook = await _analogueBookRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _analogueBookBusinessRules.AnalogueBookShouldExistWhenSelected(analogueBook);
            analogueBook = _mapper.Map(request, analogueBook);

            await _analogueBookRepository.UpdateAsync(analogueBook!);

            UpdatedAnalogueBookResponse response = _mapper.Map<UpdatedAnalogueBookResponse>(analogueBook);
            return response;
        }
    }
}