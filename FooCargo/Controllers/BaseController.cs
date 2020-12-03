using Microsoft.AspNetCore.Mvc;

namespace FooCargo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BaseController : ControllerBase
    {
    }

    public class FOoController : BaseController
    {
        public ActionResult Foo()
        {
            var X = "";

            return null;
        }
    }
}