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

namespace Application.Features.BookFormats.Commands.Create;

public class CreateBookFormatCommand : IRequest<CreatedBookFormatResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid FormatId { get; set; }
    public Book? Book { get; set; }
    public Format? Format { get; set; }

    public string[] Roles => [Admin, Write, BookFormatsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBookFormats"];

    public class CreateBookFormatCommandHandler : IRequestHandler<CreateBookFormatCommand, CreatedBookFormatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookFormatRepository _bookFormatRepository;
        private readonly BookFormatBusinessRules _bookFormatBusinessRules;

        public CreateBookFormatCommandHandler(IMapper mapper, IBookFormatRepository bookFormatRepository,
                                         BookFormatBusinessRules bookFormatBusinessRules)
        {
            _mapper = mapper;
            _bookFormatRepository = bookFormatRepository;
            _bookFormatBusinessRules = bookFormatBusinessRules;
        }

        public async Task<CreatedBookFormatResponse> Handle(CreateBookFormatCommand request, CancellationToken cancellationToken)
        {
            BookFormat bookFormat = _mapper.Map<BookFormat>(request);

            await _bookFormatRepository.AddAsync(bookFormat);

            CreatedBookFormatResponse response = _mapper.Map<CreatedBookFormatResponse>(bookFormat);
            return response;
        }
    }
}