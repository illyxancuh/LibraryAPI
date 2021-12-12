using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class APIControllerBase : ControllerBase
    {
    }
}
