using Application.Features.BookFormats.Constants;
using Application.Features.BookFormats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.BookFormats.Constants.BookFormatsOperationClaims;

namespace Application.Features.BookFormats.Queries.GetById;

public class GetByIdBookFormatQuery : IRequest<GetByIdBookFormatResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdBookFormatQueryHandler : IRequestHandler<GetByIdBookFormatQuery, GetByIdBookFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookFormatRepository _bookFormatRepository;
        private readonly BookFormatBusinessRules _bookFormatBusinessRules;

        public GetByIdBookFormatQueryHandler(IMapper mapper, IBookFormatRepository bookFormatRepository, BookFormatBusinessRules bookFormatBusinessRules)
        {
            _mapper = mapper;
            _bookFormatRepository = bookFormatRepository;
            _bookFormatBusinessRules = bookFormatBusinessRules;
        }

        public async Task<GetByIdBookFormatResponse> Handle(GetByIdBookFormatQuery request, CancellationToken cancellationToken)
        {
            BookFormat? bookFormat = await _bookFormatRepository.GetAsync(predicate: bf => bf.Id == request.Id, cancellationToken: cancellationToken);
            await _bookFormatBusinessRules.BookFormatShouldExistWhenSelected(bookFormat);

            GetByIdBookFormatResponse response = _mapper.Map<GetByIdBookFormatResponse>(bookFormat);
            return response;
        }
    }
}