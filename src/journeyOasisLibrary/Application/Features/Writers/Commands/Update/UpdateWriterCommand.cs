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

namespace Application.Features.Writers.Commands.Update;

public class UpdateWriterCommand : IRequest<UpdatedWriterResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string[] Roles => [Admin, Write, WritersOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWriters"];

    public class UpdateWriterCommandHandler : IRequestHandler<UpdateWriterCommand, UpdatedWriterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterRepository _writerRepository;
        private readonly WriterBusinessRules _writerBusinessRules;

        public UpdateWriterCommandHandler(IMapper mapper, IWriterRepository writerRepository,
                                         WriterBusinessRules writerBusinessRules)
        {
            _mapper = mapper;
            _writerRepository = writerRepository;
            _writerBusinessRules = writerBusinessRules;
        }

        public async Task<UpdatedWriterResponse> Handle(UpdateWriterCommand request, CancellationToken cancellationToken)
        {
            Writer? writer = await _writerRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);
            await _writerBusinessRules.WriterShouldExistWhenSelected(writer);
            writer = _mapper.Map(request, writer);

            await _writerRepository.UpdateAsync(writer!);

            UpdatedWriterResponse response = _mapper.Map<UpdatedWriterResponse>(writer);
            return response;
        }
    }
}