# ApiResilienteFlurlPolly
Pequeno projeto que mostra como aplicar toques de resiliência e tolerância a falhas ao seu projeto .Net 6.

## Flurl
https://flurl.dev/

Mão na roda para realizar consumo de APIs. Fluent
>> Ex.
```C#
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
```                

## Polly
https://github.com/App-vNext/Polly#retry

```C#
protected AsyncRetryPolicy BuildRetryPolicy()
        {
            var retryPolicy = Policy
               .Handle<FlurlHttpException>(IsTransientError)
               .WaitAndRetryAsync(new[]
               {
                   TimeSpan.FromSeconds(1),
                   TimeSpan.FromSeconds(2),
                   TimeSpan.FromSeconds(3)
               }, (exception, timeSpan, retryCount, context) =>
               {
                   Console.WriteLine($"Tentativa {retryCount} com tempo de espera de {timeSpan} para executar a próxima tentativa"); 
               });

            return retryPolicy;
        }

        // Esse método pode ir para uma classe base
        private bool IsTransientError(FlurlHttpException exception)
        {
            int[] httpStatusCodesWorthRetrying =
            {
                StatusCodes.Status408RequestTimeout, // 408
                StatusCodes.Status502BadGateway, // 502
                StatusCodes.Status503ServiceUnavailable, // 503
                StatusCodes.Status504GatewayTimeout // 504
            };

            return exception.StatusCode.HasValue && httpStatusCodesWorthRetrying.Contains(exception.StatusCode.Value);
        }
```
