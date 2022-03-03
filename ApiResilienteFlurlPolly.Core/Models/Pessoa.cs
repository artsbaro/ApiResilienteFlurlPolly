using Newtonsoft.Json;

namespace ApiResilienteFlurlPolly.Core.Models
{
    public class Pessoa
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("dataNascimento")]
        public DateTime DataNascimento { get; set; }

    }
}