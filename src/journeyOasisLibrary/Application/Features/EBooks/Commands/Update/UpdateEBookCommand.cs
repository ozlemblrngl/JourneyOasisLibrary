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

namespace Application.Features.EBooks.Commands.Update;

public class UpdateEBookCommand : IRequest<UpdatedEBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }

    public string[] Roles => [Admin, Write, EBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEBooks"];

    public class UpdateEBookCommandHandler : IRequestHandler<UpdateEBookCommand, UpdatedEBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEBookRepository _eBookRepository;
        private readonly EBookBusinessRules _eBookBusinessRules;

        public UpdateEBookCommandHandler(IMapper mapper, IEBookRepository eBookRepository,
                                         EBookBusinessRules eBookBusinessRules)
        {
            _mapper = mapper;
            _eBookRepository = eBookRepository;
            _eBookBusinessRules = eBookBusinessRules;
        }

        public async Task<UpdatedEBookResponse> Handle(UpdateEBookCommand request, CancellationToken cancellationToken)
        {
            EBook? eBook = await _eBookRepository.GetAsync(predicate: eb => eb.Id == request.Id, cancellationToken: cancellationToken);
            await _eBookBusinessRules.EBookShouldExistWhenSelected(eBook);
            eBook = _mapper.Map(request, eBook);

            await _eBookRepository.UpdateAsync(eBook!);

            UpdatedEBookResponse response = _mapper.Map<UpdatedEBookResponse>(eBook);
            return response;
        }
    }
}