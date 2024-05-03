using Application.Features.EBooks.Constants;
using Application.Features.EBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.EBooks.Constants.EBooksOperationClaims;

namespace Application.Features.EBooks.Queries.GetById;

public class GetByIdEBookQuery : IRequest<GetByIdEBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdEBookQueryHandler : IRequestHandler<GetByIdEBookQuery, GetByIdEBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEBookRepository _eBookRepository;
        private readonly EBookBusinessRules _eBookBusinessRules;

        public GetByIdEBookQueryHandler(IMapper mapper, IEBookRepository eBookRepository, EBookBusinessRules eBookBusinessRules)
        {
            _mapper = mapper;
            _eBookRepository = eBookRepository;
            _eBookBusinessRules = eBookBusinessRules;
        }

        public async Task<GetByIdEBookResponse> Handle(GetByIdEBookQuery request, CancellationToken cancellationToken)
        {
            EBook? eBook = await _eBookRepository.GetAsync(predicate: eb => eb.Id == request.Id, cancellationToken: cancellationToken);
            await _eBookBusinessRules.EBookShouldExistWhenSelected(eBook);

            GetByIdEBookResponse response = _mapper.Map<GetByIdEBookResponse>(eBook);
            return response;
        }
    }
}