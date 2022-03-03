

using ApiResilienteFlurlPolly.Core.Models;

namespace ApiResilienteFlurlPolly.Services
{
    public interface IPessoaService
    {
        IEnumerable<Pessoa> GetPessoas();
    }
}
