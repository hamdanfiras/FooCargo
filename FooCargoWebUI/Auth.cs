using FooCargo.CoreModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargoWebUI
{
    public class Auth
    {
        private FooCargoAuthenticationStateProvider authenticationStateProvider;

        public event EventHandler AuthChanged;

        public Auth(AuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider as FooCargoAuthenticationStateProvider;

            // we are subscribing to the event AuthenticationStateChanged using += c# syntax
            this.authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;
        }

        private void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            // publishing the even AuthChanged, so that components that are using Auth, do not need to subscribe to the authenticationStateProvider
            AuthChanged?.Invoke(this, new EventArgs());

            // line above is a shortcut for the code below ?. protects from null value exception.
            //if (AuthChanged != null)
            //{
            //    AuthChanged(this, new EventArgs());
            //}
        }

        public async Task<bool> IsAdmin()
        {
            return await HasClaimType(Claims.ADMIN);
        }

        public async Task<bool> IsStaff()
        {
            return await HasClaimType(Claims.STAFF);
        }

        public async Task<string> Name()
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            if (!state.User.Identity.IsAuthenticated)
            {
                return "Anonymous";
            }
            else return state?.User?.Identity?.Name;
        }

        private async Task<bool> HasClaimType(string claimType)
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            return state?.User?.Claims?.Any(x => x.Type == claimType) ?? false;
        }
    }
}
