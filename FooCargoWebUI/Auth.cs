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

        public Auth(AuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider as FooCargoAuthenticationStateProvider;
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
            if (state.User.Identity.IsAuthenticated)
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
