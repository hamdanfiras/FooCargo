using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddCargoAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(autorizationOptions =>
            {
                autorizationOptions.AddPolicy(Policies.MANAGE_RATE, policy => policy.RequireClaim(Claims.ADMIN));

                autorizationOptions.AddPolicy(Policies.VIEW_RATE, policy =>
                {
                    policy.Requirements.Add(new AnyClaimRequirement(Claims.ADMIN, Claims.STAFF));
                    //policy.Requirements.Add(new MinimumAgeRequirement(32));
                });

                autorizationOptions.AddPolicy(Policies.MANAGE_MANIFEST, policy =>
                {
                    policy.Requirements.Add(new AnyClaimRequirement(Claims.ADMIN, Claims.STAFF));
                });
            });

            services.AddSingleton<IAuthorizationHandler, AnyClaimHandler>();
        }
    }

    public class AnyClaimRequirement : IAuthorizationRequirement
    {
        public string[] ClaimTypes { get; }

        public AnyClaimRequirement(params string[] claimTypes)
        {
            ClaimTypes = claimTypes;
        }
    }

    //public class MinimumAgeRequirement : IAuthorizationRequirement
    //{
    //    public int MinimumAge { get; }

    //    public MinimumAgeRequirement(int minimumAge)
    //    {
    //        MinimumAge = minimumAge;
    //    }
    //}

    public class AnyClaimHandler : AuthorizationHandler<AnyClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AnyClaimRequirement requirement)
        {
            var claims = context.User.Claims.ToList();
            if (requirement.ClaimTypes.Any(claimType => context.User.Claims.Any(c => c.Type == claimType)))
            {
                context.Succeed(requirement);
            }

            // linq to objects classical loop alternative for the above
            //foreach (var claimType in requirement.ClaimTypes)
            //{
            //    foreach (var c in context.User.Claims)
            //    {
            //        if (c.Type == claimType)
            //        {
            //            context.Succeed(requirement);
            //            break;
            //        }
            //    }
            //}

            return Task.CompletedTask;
        }
    }
}
