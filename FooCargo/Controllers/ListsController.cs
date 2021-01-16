using FooCargo.CoreModels;
using FooCargo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ListsController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ListsController(CargoDb context, IWebHostEnvironment hostingEnvironment) : base(context)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("airports")]
        public ActionResult<List<Airport>> Airports()
        {
            return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lists", "airports.json"), "application/json");
        }
    }
}
