using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplex.IdentityServer
{
    public class PermissionHandler :
    AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var permissionsClaim = context.User.Claims
                .SingleOrDefault(c =>
                     c.Type == "Permissions");
            if (permissionsClaim == null)
                return Task.CompletedTask;

            if (permissionsClaim.Value
                .ThisPermissionIsAllowed(requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
