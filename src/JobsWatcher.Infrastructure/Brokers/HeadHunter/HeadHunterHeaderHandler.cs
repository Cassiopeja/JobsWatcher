using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter
{
    public class HeadHunterHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("HH-User-Agent", "HH-User-Agent");
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}