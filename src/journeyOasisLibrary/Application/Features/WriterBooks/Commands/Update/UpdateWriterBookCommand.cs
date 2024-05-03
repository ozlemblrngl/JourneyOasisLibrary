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

namespace Application.Features.WriterBooks.Commands.Update;

public class UpdateWriterBookCommand : IRequest<UpdatedWriterBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid WriterId { get; set; }
    public Book? Book { get; set; }
    public Writer? Writer { get; set; }

    public string[] Roles => [Admin, Write, WriterBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWriterBooks"];

    public class UpdateWriterBookCommandHandler : IRequestHandler<UpdateWriterBookCommand, UpdatedWriterBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterBookRepository _writerBookRepository;
        private readonly WriterBookBusinessRules _writerBookBusinessRules;

        public UpdateWriterBookCommandHandler(IMapper mapper, IWriterBookRepository writerBookRepository,
                                         WriterBookBusinessRules writerBookBusinessRules)
        {
            _mapper = mapper;
            _writerBookRepository = writerBookRepository;
            _writerBookBusinessRules = writerBookBusinessRules;
        }

        public async Task<UpdatedWriterBookResponse> Handle(UpdateWriterBookCommand request, CancellationToken cancellationToken)
        {
            WriterBook? writerBook = await _writerBookRepository.GetAsync(predicate: wb => wb.Id == request.Id, cancellationToken: cancellationToken);
            await _writerBookBusinessRules.WriterBookShouldExistWhenSelected(writerBook);
            writerBook = _mapper.Map(request, writerBook);

            await _writerBookRepository.UpdateAsync(writerBook!);

            UpdatedWriterBookResponse response = _mapper.Map<UpdatedWriterBookResponse>(writerBook);
            return response;
        }
    }
}