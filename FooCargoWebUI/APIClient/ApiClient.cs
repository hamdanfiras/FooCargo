using FooCargo.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FooCargoWebUI.APIClient
{
    public partial class ApiClient
    {
        private HttpClient httpClient;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("ServerAPI");
        }
    }
}
