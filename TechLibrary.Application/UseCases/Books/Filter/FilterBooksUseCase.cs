using AutoMapper;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Repositories.Books;

namespace TechLibrary.Application.UseCases.Books.Filter;
public class FilterBooksUseCase : IFilterBooksUseCase
{
    private const int PAGE_SIZE = 10;

    private readonly IBookReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public FilterBooksUseCase(IBookReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBooksJson> Execute(RequestFilterBooksJson request)
    {
        var books = await _repository.GetFiltrededBooks(request.PageNumber, request.Title, PAGE_SIZE);

        return new ResponseBooksJson
        {
            Pagination = _mapper.Map<ResponsePaginationJson>(books.Item2),
            Books = _mapper.Map<List<ResponseBookJson>>(books.Item1)
        };
    }
}
