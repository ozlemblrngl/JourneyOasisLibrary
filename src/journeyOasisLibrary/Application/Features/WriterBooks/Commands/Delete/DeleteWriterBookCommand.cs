using Application.Features.WriterBooks.Constants;
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

namespace Application.Features.WriterBooks.Commands.Delete;

public class DeleteWriterBookCommand : IRequest<DeletedWriterBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, WriterBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWriterBooks"];

    public class DeleteWriterBookCommandHandler : IRequestHandler<DeleteWriterBookCommand, DeletedWriterBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterBookRepository _writerBookRepository;
        private readonly WriterBookBusinessRules _writerBookBusinessRules;

        public DeleteWriterBookCommandHandler(IMapper mapper, IWriterBookRepository writerBookRepository,
                                         WriterBookBusinessRules writerBookBusinessRules)
        {
            _mapper = mapper;
            _writerBookRepository = writerBookRepository;
            _writerBookBusinessRules = writerBookBusinessRules;
        }

        public async Task<DeletedWriterBookResponse> Handle(DeleteWriterBookCommand request, CancellationToken cancellationToken)
        {
            WriterBook? writerBook = await _writerBookRepository.GetAsync(predicate: wb => wb.Id == request.Id, cancellationToken: cancellationToken);
            await _writerBookBusinessRules.WriterBookShouldExistWhenSelected(writerBook);

            await _writerBookRepository.DeleteAsync(writerBook!);

            DeletedWriterBookResponse response = _mapper.Map<DeletedWriterBookResponse>(writerBook);
            return response;
        }
    }
}