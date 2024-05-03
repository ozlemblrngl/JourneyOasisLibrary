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

namespace Application.Features.EBooks.Commands.Create;

public class CreateEBookCommand : IRequest<CreatedEBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }

    public string[] Roles => [Admin, Write, EBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEBooks"];

    public class CreateEBookCommandHandler : IRequestHandler<CreateEBookCommand, CreatedEBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEBookRepository _eBookRepository;
        private readonly EBookBusinessRules _eBookBusinessRules;

        public CreateEBookCommandHandler(IMapper mapper, IEBookRepository eBookRepository,
                                         EBookBusinessRules eBookBusinessRules)
        {
            _mapper = mapper;
            _eBookRepository = eBookRepository;
            _eBookBusinessRules = eBookBusinessRules;
        }

        public async Task<CreatedEBookResponse> Handle(CreateEBookCommand request, CancellationToken cancellationToken)
        {
            EBook eBook = _mapper.Map<EBook>(request);

            await _eBookRepository.AddAsync(eBook);

            CreatedEBookResponse response = _mapper.Map<CreatedEBookResponse>(eBook);
            return response;
        }
    }
}