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
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            await Task.Delay(31000);
            var pessoas = await Task.FromResult(_service.GetPessoas());
            return new StatusCodeResult(503);
            //return Ok(pessoas);
        }
    }
}