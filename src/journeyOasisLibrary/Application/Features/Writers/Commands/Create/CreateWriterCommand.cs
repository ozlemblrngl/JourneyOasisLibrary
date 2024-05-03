using Application.Features.Writers.Constants;
using Application.Features.Writers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Writers.Constants.WritersOperationClaims;

namespace Application.Features.Writers.Commands.Create;

public class CreateWriterCommand : IRequest<CreatedWriterResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string[] Roles => [Admin, Write, WritersOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWriters"];

    public class CreateWriterCommandHandler : IRequestHandler<CreateWriterCommand, CreatedWriterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterRepository _writerRepository;
        private readonly WriterBusinessRules _writerBusinessRules;

        public CreateWriterCommandHandler(IMapper mapper, IWriterRepository writerRepository,
                                         WriterBusinessRules writerBusinessRules)
        {
            _mapper = mapper;
            _writerRepository = writerRepository;
            _writerBusinessRules = writerBusinessRules;
        }

        public async Task<CreatedWriterResponse> Handle(CreateWriterCommand request, CancellationToken cancellationToken)
        {
            Writer writer = _mapper.Map<Writer>(request);

            await _writerRepository.AddAsync(writer);

            CreatedWriterResponse response = _mapper.Map<CreatedWriterResponse>(writer);
            return response;
        }
    }
}