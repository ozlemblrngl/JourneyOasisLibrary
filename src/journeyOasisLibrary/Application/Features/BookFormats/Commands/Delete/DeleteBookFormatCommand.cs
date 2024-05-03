using Application.Features.BookFormats.Constants;
using Application.Features.BookFormats.Constants;
using Application.Features.BookFormats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.BookFormats.Constants.BookFormatsOperationClaims;

namespace Application.Features.BookFormats.Commands.Delete;

public class DeleteBookFormatCommand : IRequest<DeletedBookFormatResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, BookFormatsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBookFormats"];

    public class DeleteBookFormatCommandHandler : IRequestHandler<DeleteBookFormatCommand, DeletedBookFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookFormatRepository _bookFormatRepository;
        private readonly BookFormatBusinessRules _bookFormatBusinessRules;

        public DeleteBookFormatCommandHandler(IMapper mapper, IBookFormatRepository bookFormatRepository,
                                         BookFormatBusinessRules bookFormatBusinessRules)
        {
            _mapper = mapper;
            _bookFormatRepository = bookFormatRepository;
            _bookFormatBusinessRules = bookFormatBusinessRules;
        }

        public async Task<DeletedBookFormatResponse> Handle(DeleteBookFormatCommand request, CancellationToken cancellationToken)
        {
            BookFormat? bookFormat = await _bookFormatRepository.GetAsync(predicate: bf => bf.Id == request.Id, cancellationToken: cancellationToken);
            await _bookFormatBusinessRules.BookFormatShouldExistWhenSelected(bookFormat);

            await _bookFormatRepository.DeleteAsync(bookFormat!);

            DeletedBookFormatResponse response = _mapper.Map<DeletedBookFormatResponse>(bookFormat);
            return response;
        }
    }
}