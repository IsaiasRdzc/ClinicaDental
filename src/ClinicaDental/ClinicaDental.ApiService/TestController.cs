using Microsoft.AspNetCore.Mvc;

namespace ClinicaDental.ApiService
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World!";
        }
    }
}
