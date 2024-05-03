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

namespace Application.Features.AnalogueBooks.Commands.Create;

public class CreateAnalogueBookCommand : IRequest<CreatedAnalogueBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }

    public string[] Roles => [Admin, Write, AnalogueBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAnalogueBooks"];

    public class CreateAnalogueBookCommandHandler : IRequestHandler<CreateAnalogueBookCommand, CreatedAnalogueBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnalogueBookRepository _analogueBookRepository;
        private readonly AnalogueBookBusinessRules _analogueBookBusinessRules;

        public CreateAnalogueBookCommandHandler(IMapper mapper, IAnalogueBookRepository analogueBookRepository,
                                         AnalogueBookBusinessRules analogueBookBusinessRules)
        {
            _mapper = mapper;
            _analogueBookRepository = analogueBookRepository;
            _analogueBookBusinessRules = analogueBookBusinessRules;
        }

        public async Task<CreatedAnalogueBookResponse> Handle(CreateAnalogueBookCommand request, CancellationToken cancellationToken)
        {
            AnalogueBook analogueBook = _mapper.Map<AnalogueBook>(request);

            await _analogueBookRepository.AddAsync(analogueBook);

            CreatedAnalogueBookResponse response = _mapper.Map<CreatedAnalogueBookResponse>(analogueBook);
            return response;
        }
    }
}