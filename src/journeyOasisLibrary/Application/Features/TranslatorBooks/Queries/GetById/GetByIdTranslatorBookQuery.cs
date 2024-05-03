using Application.Features.TranslatorBooks.Constants;
using Application.Features.TranslatorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TranslatorBooks.Constants.TranslatorBooksOperationClaims;

namespace Application.Features.TranslatorBooks.Queries.GetById;

public class GetByIdTranslatorBookQuery : IRequest<GetByIdTranslatorBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdTranslatorBookQueryHandler : IRequestHandler<GetByIdTranslatorBookQuery, GetByIdTranslatorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITranslatorBookRepository _translatorBookRepository;
        private readonly TranslatorBookBusinessRules _translatorBookBusinessRules;

        public GetByIdTranslatorBookQueryHandler(IMapper mapper, ITranslatorBookRepository translatorBookRepository, TranslatorBookBusinessRules translatorBookBusinessRules)
        {
            _mapper = mapper;
            _translatorBookRepository = translatorBookRepository;
            _translatorBookBusinessRules = translatorBookBusinessRules;
        }

        public async Task<GetByIdTranslatorBookResponse> Handle(GetByIdTranslatorBookQuery request, CancellationToken cancellationToken)
        {
            TranslatorBook? translatorBook = await _translatorBookRepository.GetAsync(predicate: tb => tb.Id == request.Id, cancellationToken: cancellationToken);
            await _translatorBookBusinessRules.TranslatorBookShouldExistWhenSelected(translatorBook);

            GetByIdTranslatorBookResponse response = _mapper.Map<GetByIdTranslatorBookResponse>(translatorBook);
            return response;
        }
    }
}