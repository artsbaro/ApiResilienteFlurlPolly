using ApiResilienteFlurlPolly.Core.Models;

namespace BffResilienteFlurlPolly.Services
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> GetPessoas();
    }
}
