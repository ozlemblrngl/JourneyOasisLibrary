using Application.Features.Formats.Constants;
using Application.Features.Formats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Formats.Constants.FormatsOperationClaims;

namespace Application.Features.Formats.Queries.GetById;

public class GetByIdFormatQuery : IRequest<GetByIdFormatResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdFormatQueryHandler : IRequestHandler<GetByIdFormatQuery, GetByIdFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFormatRepository _formatRepository;
        private readonly FormatBusinessRules _formatBusinessRules;

        public GetByIdFormatQueryHandler(IMapper mapper, IFormatRepository formatRepository, FormatBusinessRules formatBusinessRules)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _formatBusinessRules = formatBusinessRules;
        }

        public async Task<GetByIdFormatResponse> Handle(GetByIdFormatQuery request, CancellationToken cancellationToken)
        {
            Format? format = await _formatRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _formatBusinessRules.FormatShouldExistWhenSelected(format);

            GetByIdFormatResponse response = _mapper.Map<GetByIdFormatResponse>(format);
            return response;
        }
    }
}