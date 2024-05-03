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

namespace Application.Features.Formats.Commands.Update;

public class UpdateFormatCommand : IRequest<UpdatedFormatResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => [Admin, Write, FormatsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFormats"];

    public class UpdateFormatCommandHandler : IRequestHandler<UpdateFormatCommand, UpdatedFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFormatRepository _formatRepository;
        private readonly FormatBusinessRules _formatBusinessRules;

        public UpdateFormatCommandHandler(IMapper mapper, IFormatRepository formatRepository,
                                         FormatBusinessRules formatBusinessRules)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _formatBusinessRules = formatBusinessRules;
        }

        public async Task<UpdatedFormatResponse> Handle(UpdateFormatCommand request, CancellationToken cancellationToken)
        {
            Format? format = await _formatRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _formatBusinessRules.FormatShouldExistWhenSelected(format);
            format = _mapper.Map(request, format);

            await _formatRepository.UpdateAsync(format!);

            UpdatedFormatResponse response = _mapper.Map<UpdatedFormatResponse>(format);
            return response;
        }
    }
}