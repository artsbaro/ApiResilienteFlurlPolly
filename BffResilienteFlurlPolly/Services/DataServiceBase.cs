using Flurl.Http;
using Polly;
using Polly.Retry;

namespace BffResilienteFlurlPolly.Services
{
    public abstract class DataServiceBase
    {
        // Esse método pode ir para uma classe base
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
                   Console.WriteLine("#########");
                   Console.WriteLine($"Exception captada {exception}");
                   Console.WriteLine("#########");
                   Console.WriteLine("");

                   Console.WriteLine($"Tentativa {retryCount} com tempo de espera de {timeSpan} para executar a próxima tentativa");

                   Console.WriteLine("");
                   Console.WriteLine("#########");
                   Console.WriteLine("");
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
    }
}
