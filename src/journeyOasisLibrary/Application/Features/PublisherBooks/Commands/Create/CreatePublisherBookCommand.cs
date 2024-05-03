using Application.Features.PublisherBooks.Constants;
using Application.Features.PublisherBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.PublisherBooks.Constants.PublisherBooksOperationClaims;

namespace Application.Features.PublisherBooks.Commands.Create;

public class CreatePublisherBookCommand : IRequest<CreatedPublisherBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
    public Book? Book { get; set; }
    public Publisher? Publisher { get; set; }

    public string[] Roles => [Admin, Write, PublisherBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPublisherBooks"];

    public class CreatePublisherBookCommandHandler : IRequestHandler<CreatePublisherBookCommand, CreatedPublisherBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBookRepository _publisherBookRepository;
        private readonly PublisherBookBusinessRules _publisherBookBusinessRules;

        public CreatePublisherBookCommandHandler(IMapper mapper, IPublisherBookRepository publisherBookRepository,
                                         PublisherBookBusinessRules publisherBookBusinessRules)
        {
            _mapper = mapper;
            _publisherBookRepository = publisherBookRepository;
            _publisherBookBusinessRules = publisherBookBusinessRules;
        }

        public async Task<CreatedPublisherBookResponse> Handle(CreatePublisherBookCommand request, CancellationToken cancellationToken)
        {
            PublisherBook publisherBook = _mapper.Map<PublisherBook>(request);

            await _publisherBookRepository.AddAsync(publisherBook);

            CreatedPublisherBookResponse response = _mapper.Map<CreatedPublisherBookResponse>(publisherBook);
            return response;
        }
    }
}