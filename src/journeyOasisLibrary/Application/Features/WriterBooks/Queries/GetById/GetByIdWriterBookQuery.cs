using Application.Features.WriterBooks.Constants;
using Application.Features.WriterBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.WriterBooks.Constants.WriterBooksOperationClaims;

namespace Application.Features.WriterBooks.Queries.GetById;

public class GetByIdWriterBookQuery : IRequest<GetByIdWriterBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdWriterBookQueryHandler : IRequestHandler<GetByIdWriterBookQuery, GetByIdWriterBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWriterBookRepository _writerBookRepository;
        private readonly WriterBookBusinessRules _writerBookBusinessRules;

        public GetByIdWriterBookQueryHandler(IMapper mapper, IWriterBookRepository writerBookRepository, WriterBookBusinessRules writerBookBusinessRules)
        {
            _mapper = mapper;
            _writerBookRepository = writerBookRepository;
            _writerBookBusinessRules = writerBookBusinessRules;
        }

        public async Task<GetByIdWriterBookResponse> Handle(GetByIdWriterBookQuery request, CancellationToken cancellationToken)
        {
            WriterBook? writerBook = await _writerBookRepository.GetAsync(predicate: wb => wb.Id == request.Id, cancellationToken: cancellationToken);
            await _writerBookBusinessRules.WriterBookShouldExistWhenSelected(writerBook);

            GetByIdWriterBookResponse response = _mapper.Map<GetByIdWriterBookResponse>(writerBook);
            return response;
        }
    }
}