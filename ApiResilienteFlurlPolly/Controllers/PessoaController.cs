using ApiResilienteFlurlPolly.Core.Models;
using ApiResilienteFlurlPolly.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiResilienteFlurlPolly.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _service;

        private readonly ILogger<PessoaController> _logger;

        public PessoaController(ILogger<PessoaController> logger, 
            IPessoaService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "Pessoas")]
        [ProducesResponseType(typeof(IEnumerable<Pessoa>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Pessoa>> GetPessoas()
        {
            var  pessoas = _service.GetPessoas();
            return Ok(pessoas);
        }
    }
}