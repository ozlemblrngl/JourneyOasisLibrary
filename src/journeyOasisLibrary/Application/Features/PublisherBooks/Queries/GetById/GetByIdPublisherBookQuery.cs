using Application.Features.PublisherBooks.Constants;
using Application.Features.PublisherBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.PublisherBooks.Constants.PublisherBooksOperationClaims;

namespace Application.Features.PublisherBooks.Queries.GetById;

public class GetByIdPublisherBookQuery : IRequest<GetByIdPublisherBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdPublisherBookQueryHandler : IRequestHandler<GetByIdPublisherBookQuery, GetByIdPublisherBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBookRepository _publisherBookRepository;
        private readonly PublisherBookBusinessRules _publisherBookBusinessRules;

        public GetByIdPublisherBookQueryHandler(IMapper mapper, IPublisherBookRepository publisherBookRepository, PublisherBookBusinessRules publisherBookBusinessRules)
        {
            _mapper = mapper;
            _publisherBookRepository = publisherBookRepository;
            _publisherBookBusinessRules = publisherBookBusinessRules;
        }

        public async Task<GetByIdPublisherBookResponse> Handle(GetByIdPublisherBookQuery request, CancellationToken cancellationToken)
        {
            PublisherBook? publisherBook = await _publisherBookRepository.GetAsync(predicate: pb => pb.Id == request.Id, cancellationToken: cancellationToken);
            await _publisherBookBusinessRules.PublisherBookShouldExistWhenSelected(publisherBook);

            GetByIdPublisherBookResponse response = _mapper.Map<GetByIdPublisherBookResponse>(publisherBook);
            return response;
        }
    }
}