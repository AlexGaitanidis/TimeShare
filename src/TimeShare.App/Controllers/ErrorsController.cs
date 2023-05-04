using Microsoft.AspNetCore.Mvc;

namespace TimeShare.App.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpPost("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}