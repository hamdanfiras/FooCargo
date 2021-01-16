using FooCargo.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FooCargoWebUI.APIClient
{
    public partial class ApiClient
    {
        public async Task<LoginResult> Login(LoginInfo loginInfo)
        {
            var res = await httpClient.PostAsJsonAsync<LoginInfo>("/api/account/login", loginInfo);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<LoginResult>();
            }
            else
                return null;
        }
    }
}
