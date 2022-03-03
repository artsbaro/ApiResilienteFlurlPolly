using ApiResilienteFlurlPolly.Core.Models;
using Flurl.Http;

namespace BffResilienteFlurlPolly.Services
{
    public class PessoaService : DataServiceBase,IPessoaService
    {
        public async Task<IEnumerable<Pessoa>> GetPessoas()
        {
            return await BuildRetryPolicy().ExecuteAsync(() => 
                         "http://localhost:5212/Pessoa"
                        .WithHeader("accept", "application/json")
                        .WithHeader("content-type", "application/json")
                        //.WithBasicAuth("username", "password")
                        //.WithOAuthBearerToken("mytoken")
                        .GetJsonAsync<IEnumerable<Pessoa>>());
        }
    }
}
