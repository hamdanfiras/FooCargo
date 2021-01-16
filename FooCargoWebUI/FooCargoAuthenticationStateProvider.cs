using Blazored.LocalStorage;
using FooCargo.CoreModels;
using FooCargoWebUI.APIClient;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FooCargoWebUI
{
    public class FooCargoAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ApiClient apiClient;
        private readonly ILocalStorageService localStorage;
        private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        public FooCargoAuthenticationStateProvider(ApiClient apiClient, ILocalStorageService localStorage)
        {
            this.apiClient = apiClient;
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var loginResult = await localStorage.GetItemAsync<LoginResult>("LOGIN_RESULT");
            if (loginResult == null)
            {
                // ClaimsIdentity does not contain any claims, thus it is considered an anonymous user
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            }
            else
            {
                var claims = JwtParser.ParseClaimsFromJwt(loginResult.Token);
                var identity = new ClaimsIdentity(claims, "JWTAuthentication");
                claimsPrincipal = new ClaimsPrincipal(identity);
            }
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task Login(LoginInfo loginInfo)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

            var res = await apiClient.Login(loginInfo);

            if (res != null)
            {
                await localStorage.SetItemAsync("LOGIN_RESULT", res);
               
            }
            else
            {
                await localStorage.RemoveItemAsync("LOGIN_RESULT");
            }


            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("LOGIN_RESULT");
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
