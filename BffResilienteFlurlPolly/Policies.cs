using Polly;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace BffResilienteFlurlPolly
{
    public static class Policies
    {
        private static AsyncTimeoutPolicy<HttpResponseMessage> TimeoutPolicy
        {
            get
            {
                return Policy.TimeoutAsync<HttpResponseMessage>(2, (context, timeSpan, task) =>
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("############");
                    Console.WriteLine($"[App|Policy]: Timeout delegate fired after {timeSpan.Seconds} seconds");

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("############");

                    return Task.CompletedTask;
                });
            }
        }

        private static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy
        {
            get
            {
                return Policy
                    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .Or<TimeoutRejectedException>()
                    .WaitAndRetryAsync(new[]
                        {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(5)
                        },
                        (delegateResult, retryCount) =>
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("############");

                            Console.WriteLine($"[App|Policy]: Retry delegate fired, attempt {retryCount}");

                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("############");
                        });
            }
        }

        public static AsyncPolicyWrap<HttpResponseMessage> PolicyStrategy => Policy.WrapAsync(RetryPolicy, TimeoutPolicy);
    }
}
