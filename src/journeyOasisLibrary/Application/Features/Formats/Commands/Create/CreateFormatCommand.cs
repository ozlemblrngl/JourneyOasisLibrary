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

namespace Application.Features.Formats.Commands.Create;

public class CreateFormatCommand : IRequest<CreatedFormatResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => [Admin, Write, FormatsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFormats"];

    public class CreateFormatCommandHandler : IRequestHandler<CreateFormatCommand, CreatedFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFormatRepository _formatRepository;
        private readonly FormatBusinessRules _formatBusinessRules;

        public CreateFormatCommandHandler(IMapper mapper, IFormatRepository formatRepository,
                                         FormatBusinessRules formatBusinessRules)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _formatBusinessRules = formatBusinessRules;
        }

        public async Task<CreatedFormatResponse> Handle(CreateFormatCommand request, CancellationToken cancellationToken)
        {
            Format format = _mapper.Map<Format>(request);

            await _formatRepository.AddAsync(format);

            CreatedFormatResponse response = _mapper.Map<CreatedFormatResponse>(format);
            return response;
        }
    }
}