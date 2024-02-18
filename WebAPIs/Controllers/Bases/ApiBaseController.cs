using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.Bases;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiBaseController : ControllerBase
{
}
