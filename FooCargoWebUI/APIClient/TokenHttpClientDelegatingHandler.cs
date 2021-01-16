using FooCargo.CoreModels;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blazored;
using Blazored.LocalStorage;

namespace FooCargoWebUI
{
    public class TokenHttpClientDelegatingHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorage;

        public TokenHttpClientDelegatingHandler(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var loginResult = await localStorage.GetItemAsync<LoginResult>("LOGIN_RESULT");
            if (loginResult != null)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
            }
            else
            {
                request.Headers.Authorization = null;
            }
         
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
