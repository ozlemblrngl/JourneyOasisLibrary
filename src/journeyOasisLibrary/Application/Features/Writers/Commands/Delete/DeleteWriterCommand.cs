using Application.Features.Writers.Constants;
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

namespace Application.Features.Writers.Commands.Delete;

public class DeleteWriterCommand : IRequest<DeletedWriterResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, WritersOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWriters"];

    public class DeleteWriterCommandHandler : IRequestHandler<DeleteWriterCommand, DeletedWriterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterRepository _writerRepository;
        private readonly WriterBusinessRules _writerBusinessRules;

        public DeleteWriterCommandHandler(IMapper mapper, IWriterRepository writerRepository,
                                         WriterBusinessRules writerBusinessRules)
        {
            _mapper = mapper;
            _writerRepository = writerRepository;
            _writerBusinessRules = writerBusinessRules;
        }

        public async Task<DeletedWriterResponse> Handle(DeleteWriterCommand request, CancellationToken cancellationToken)
        {
            Writer? writer = await _writerRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);
            await _writerBusinessRules.WriterShouldExistWhenSelected(writer);

            await _writerRepository.DeleteAsync(writer!);

            DeletedWriterResponse response = _mapper.Map<DeletedWriterResponse>(writer);
            return response;
        }
    }
}