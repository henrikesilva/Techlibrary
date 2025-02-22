using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Books.Filter;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet("Filter")]
    [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Filter(
        [FromServices] IFilterBooksUseCase useCase,
        [FromQuery] int pageNumber, [FromQuery] string? title)
    {
        var result = await useCase.Execute(new RequestFilterBooksJson
        {
            PageNumber = pageNumber,
            Title = title
        });


        return Ok(result);
    }
}
