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

namespace Application.Features.PublisherBooks.Commands.Update;

public class UpdatePublisherBookCommand : IRequest<UpdatedPublisherBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
    public Book? Book { get; set; }
    public Publisher? Publisher { get; set; }

    public string[] Roles => [Admin, Write, PublisherBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPublisherBooks"];

    public class UpdatePublisherBookCommandHandler : IRequestHandler<UpdatePublisherBookCommand, UpdatedPublisherBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBookRepository _publisherBookRepository;
        private readonly PublisherBookBusinessRules _publisherBookBusinessRules;

        public UpdatePublisherBookCommandHandler(IMapper mapper, IPublisherBookRepository publisherBookRepository,
                                         PublisherBookBusinessRules publisherBookBusinessRules)
        {
            _mapper = mapper;
            _publisherBookRepository = publisherBookRepository;
            _publisherBookBusinessRules = publisherBookBusinessRules;
        }

        public async Task<UpdatedPublisherBookResponse> Handle(UpdatePublisherBookCommand request, CancellationToken cancellationToken)
        {
            PublisherBook? publisherBook = await _publisherBookRepository.GetAsync(predicate: pb => pb.Id == request.Id, cancellationToken: cancellationToken);
            await _publisherBookBusinessRules.PublisherBookShouldExistWhenSelected(publisherBook);
            publisherBook = _mapper.Map(request, publisherBook);

            await _publisherBookRepository.UpdateAsync(publisherBook!);

            UpdatedPublisherBookResponse response = _mapper.Map<UpdatedPublisherBookResponse>(publisherBook);
            return response;
        }
    }
}