
using ApiResilienteFlurlPolly.Core.Models;

namespace ApiResilienteFlurlPolly.Services
{
    public class PessoaService : IPessoaService
    {
        public IEnumerable<Pessoa> GetPessoas()
        {
            return new List<Pessoa>() {
                new Pessoa { Id = 1, Nome = "Antonio", DataNascimento = new DateTime(1983, 6, 15)},
                new Pessoa { Id = 2, Nome = "João", DataNascimento = new DateTime(1988, 12, 12)},
                new Pessoa { Id = 3, Nome = "Pedro", DataNascimento = new DateTime(1983, 6, 15)},
                new Pessoa { Id = 4, Nome = "Maria", DataNascimento = new DateTime(1990, 2, 12)},
            };
        }
    }
}
