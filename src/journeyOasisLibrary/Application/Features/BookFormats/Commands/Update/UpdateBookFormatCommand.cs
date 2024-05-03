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

namespace Application.Features.BookFormats.Commands.Update;

public class UpdateBookFormatCommand : IRequest<UpdatedBookFormatResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid FormatId { get; set; }
    public Book? Book { get; set; }
    public Format? Format { get; set; }

    public string[] Roles => [Admin, Write, BookFormatsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBookFormats"];

    public class UpdateBookFormatCommandHandler : IRequestHandler<UpdateBookFormatCommand, UpdatedBookFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookFormatRepository _bookFormatRepository;
        private readonly BookFormatBusinessRules _bookFormatBusinessRules;

        public UpdateBookFormatCommandHandler(IMapper mapper, IBookFormatRepository bookFormatRepository,
                                         BookFormatBusinessRules bookFormatBusinessRules)
        {
            _mapper = mapper;
            _bookFormatRepository = bookFormatRepository;
            _bookFormatBusinessRules = bookFormatBusinessRules;
        }

        public async Task<UpdatedBookFormatResponse> Handle(UpdateBookFormatCommand request, CancellationToken cancellationToken)
        {
            BookFormat? bookFormat = await _bookFormatRepository.GetAsync(predicate: bf => bf.Id == request.Id, cancellationToken: cancellationToken);
            await _bookFormatBusinessRules.BookFormatShouldExistWhenSelected(bookFormat);
            bookFormat = _mapper.Map(request, bookFormat);

            await _bookFormatRepository.UpdateAsync(bookFormat!);

            UpdatedBookFormatResponse response = _mapper.Map<UpdatedBookFormatResponse>(bookFormat);
            return response;
        }
    }
}