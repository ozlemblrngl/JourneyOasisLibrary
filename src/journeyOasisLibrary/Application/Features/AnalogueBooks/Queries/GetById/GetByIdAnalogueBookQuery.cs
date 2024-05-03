using Application.Features.AnalogueBooks.Constants;
using Application.Features.AnalogueBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.AnalogueBooks.Constants.AnalogueBooksOperationClaims;

namespace Application.Features.AnalogueBooks.Queries.GetById;

public class GetByIdAnalogueBookQuery : IRequest<GetByIdAnalogueBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAnalogueBookQueryHandler : IRequestHandler<GetByIdAnalogueBookQuery, GetByIdAnalogueBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnalogueBookRepository _analogueBookRepository;
        private readonly AnalogueBookBusinessRules _analogueBookBusinessRules;

        public GetByIdAnalogueBookQueryHandler(IMapper mapper, IAnalogueBookRepository analogueBookRepository, AnalogueBookBusinessRules analogueBookBusinessRules)
        {
            _mapper = mapper;
            _analogueBookRepository = analogueBookRepository;
            _analogueBookBusinessRules = analogueBookBusinessRules;
        }

        public async Task<GetByIdAnalogueBookResponse> Handle(GetByIdAnalogueBookQuery request, CancellationToken cancellationToken)
        {
            AnalogueBook? analogueBook = await _analogueBookRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _analogueBookBusinessRules.AnalogueBookShouldExistWhenSelected(analogueBook);

            GetByIdAnalogueBookResponse response = _mapper.Map<GetByIdAnalogueBookResponse>(analogueBook);
            return response;
        }
    }
}