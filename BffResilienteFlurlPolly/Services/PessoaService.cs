using ApiResilienteFlurlPolly.Core.Models;
using Flurl.Http;

namespace BffResilienteFlurlPolly.Services
{
    public class PessoaService : DataServiceBase,IPessoaService
    {
        public async Task<IEnumerable<Pessoa>> GetPessoas()
        {
            var result = await
                         "http://localhost:5212/Pessoa"
                        .WithHeader("accept", "application/json")
                        .WithHeader("content-type", "application/json")
                        //.WithBasicAuth("username", "password")
                        //.WithOAuthBearerToken("mytoken")
                        .GetAsync();

           return await result.GetJsonAsync<IEnumerable<Pessoa>>();
            //return result.ReceiveJson<IEnumerable<Pessoa>>();
        }
    }
}
