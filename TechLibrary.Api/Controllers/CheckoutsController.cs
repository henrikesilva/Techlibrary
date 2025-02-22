using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Checkouts.Register;

namespace TechLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{
    [HttpPost]
    [Route("{bookId}")]
    public async Task<IActionResult> BookCheckout([FromServices] IRegisterBookCheckoutUseCase useCase,
        [FromRoute] Guid bookId)
    {
        await useCase.Execute(bookId);

        return NoContent();
    }
}
