using Application.Features.Writers.Constants;
using Application.Features.Writers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Writers.Constants.WritersOperationClaims;

namespace Application.Features.Writers.Queries.GetById;

public class GetByIdWriterQuery : IRequest<GetByIdWriterResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdWriterQueryHandler : IRequestHandler<GetByIdWriterQuery, GetByIdWriterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterRepository _writerRepository;
        private readonly WriterBusinessRules _writerBusinessRules;

        public GetByIdWriterQueryHandler(IMapper mapper, IWriterRepository writerRepository, WriterBusinessRules writerBusinessRules)
        {
            _mapper = mapper;
            _writerRepository = writerRepository;
            _writerBusinessRules = writerBusinessRules;
        }

        public async Task<GetByIdWriterResponse> Handle(GetByIdWriterQuery request, CancellationToken cancellationToken)
        {
            Writer? writer = await _writerRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);
            await _writerBusinessRules.WriterShouldExistWhenSelected(writer);

            GetByIdWriterResponse response = _mapper.Map<GetByIdWriterResponse>(writer);
            return response;
        }
    }
}