using Flurl.Http;
using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace BffResilienteFlurlPolly.Services
{
    public abstract class DataServiceBase
    {
        protected AsyncRetryPolicy BuildRetryPolicy()
        {
            var retryPolicy = Policy
               .Handle<FlurlHttpException>(IsTransientError)
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"TENTATIVA: {retryCount}");
                        Console.WriteLine($"TEMPO: {timeSpan}");
                        Console.WriteLine($"CONTEXT: {context}");
                        Console.WriteLine("##########");
                        Console.WriteLine(exception);
                        Console.WriteLine("##########");
                        Console.WriteLine("");
                        Console.WriteLine("");
                    }
                  );

            return retryPolicy;
        }

        protected AsyncTimeoutPolicy BuildTimeoutPolicy()
        {
            var timeoutPolicy = Policy
                .TimeoutAsync(30,
                        onTimeoutAsync: (context, timespan, task) =>
                        {
                            Console.WriteLine($"TIMESPAN: {timespan}");
                            return Task.CompletedTask;
                        });

            return timeoutPolicy;
        }



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
