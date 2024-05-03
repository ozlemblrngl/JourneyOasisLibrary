using Application.Features.PublisherBooks.Constants;
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

namespace Application.Features.PublisherBooks.Commands.Delete;

public class DeletePublisherBookCommand : IRequest<DeletedPublisherBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, PublisherBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPublisherBooks"];

    public class DeletePublisherBookCommandHandler : IRequestHandler<DeletePublisherBookCommand, DeletedPublisherBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBookRepository _publisherBookRepository;
        private readonly PublisherBookBusinessRules _publisherBookBusinessRules;

        public DeletePublisherBookCommandHandler(IMapper mapper, IPublisherBookRepository publisherBookRepository,
                                         PublisherBookBusinessRules publisherBookBusinessRules)
        {
            _mapper = mapper;
            _publisherBookRepository = publisherBookRepository;
            _publisherBookBusinessRules = publisherBookBusinessRules;
        }

        public async Task<DeletedPublisherBookResponse> Handle(DeletePublisherBookCommand request, CancellationToken cancellationToken)
        {
            PublisherBook? publisherBook = await _publisherBookRepository.GetAsync(predicate: pb => pb.Id == request.Id, cancellationToken: cancellationToken);
            await _publisherBookBusinessRules.PublisherBookShouldExistWhenSelected(publisherBook);

            await _publisherBookRepository.DeleteAsync(publisherBook!);

            DeletedPublisherBookResponse response = _mapper.Map<DeletedPublisherBookResponse>(publisherBook);
            return response;
        }
    }
}