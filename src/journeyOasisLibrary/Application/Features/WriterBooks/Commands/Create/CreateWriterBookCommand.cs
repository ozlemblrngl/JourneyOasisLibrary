using Application.Features.WriterBooks.Constants;
using Application.Features.WriterBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.WriterBooks.Constants.WriterBooksOperationClaims;

namespace Application.Features.WriterBooks.Commands.Create;

public class CreateWriterBookCommand : IRequest<CreatedWriterBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid WriterId { get; set; }
    public Book? Book { get; set; }
    public Writer? Writer { get; set; }

    public string[] Roles => [Admin, Write, WriterBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWriterBooks"];

    public class CreateWriterBookCommandHandler : IRequestHandler<CreateWriterBookCommand, CreatedWriterBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterBookRepository _writerBookRepository;
        private readonly WriterBookBusinessRules _writerBookBusinessRules;

        public CreateWriterBookCommandHandler(IMapper mapper, IWriterBookRepository writerBookRepository,
                                         WriterBookBusinessRules writerBookBusinessRules)
        {
            _mapper = mapper;
            _writerBookRepository = writerBookRepository;
            _writerBookBusinessRules = writerBookBusinessRules;
        }

        public async Task<CreatedWriterBookResponse> Handle(CreateWriterBookCommand request, CancellationToken cancellationToken)
        {
            WriterBook writerBook = _mapper.Map<WriterBook>(request);

            await _writerBookRepository.AddAsync(writerBook);

            CreatedWriterBookResponse response = _mapper.Map<CreatedWriterBookResponse>(writerBook);
            return response;
        }
    }
}