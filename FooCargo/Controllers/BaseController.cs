using FooCargo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FooCargo.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        public BaseController(CargoDb context)
        {
            Db = context;
        }

        protected CargoDb Db { get; }
    }

 
}
