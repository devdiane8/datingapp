using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("auth")]
    public IActionResult Auth()
    {
        return Unauthorized();
    }

    [HttpGet("not-found")]
    public IActionResult GotNotFound()
    {
        return NotFound();
    }
    [HttpGet("server-error")]
    public IActionResult ServerError()
    {
        return StatusCode(500);
    }
    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("This was a bad request");
    }

}
