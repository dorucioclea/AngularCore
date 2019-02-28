using AngularCore.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCore.Helpers.Security
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "isAdmin"))
            {
                var isAdmin = context.User.Claims.FirstOrDefault(c => c.Type == "isAdmin");
                if (bool.Parse(isAdmin.Value))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
