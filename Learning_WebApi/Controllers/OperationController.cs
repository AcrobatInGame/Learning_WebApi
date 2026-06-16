using Learning_WebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Learning_WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OperationController: ControllerBase
{
    [HttpPost]
    public ActionResult CreateOperation([FromBody] Operation operation)
    {
        return Ok(operation);
    }
}