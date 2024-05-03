using Application.Features.Formats.Constants;
using Application.Features.Formats.Constants;
using Application.Features.Formats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Formats.Constants.FormatsOperationClaims;

namespace Application.Features.Formats.Commands.Delete;

public class DeleteFormatCommand : IRequest<DeletedFormatResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, FormatsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFormats"];

    public class DeleteFormatCommandHandler : IRequestHandler<DeleteFormatCommand, DeletedFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFormatRepository _formatRepository;
        private readonly FormatBusinessRules _formatBusinessRules;

        public DeleteFormatCommandHandler(IMapper mapper, IFormatRepository formatRepository,
                                         FormatBusinessRules formatBusinessRules)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _formatBusinessRules = formatBusinessRules;
        }

        public async Task<DeletedFormatResponse> Handle(DeleteFormatCommand request, CancellationToken cancellationToken)
        {
            Format? format = await _formatRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _formatBusinessRules.FormatShouldExistWhenSelected(format);

            await _formatRepository.DeleteAsync(format!);

            DeletedFormatResponse response = _mapper.Map<DeletedFormatResponse>(format);
            return response;
        }
    }
}