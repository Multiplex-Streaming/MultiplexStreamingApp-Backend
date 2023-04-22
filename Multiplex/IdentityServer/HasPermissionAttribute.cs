using Multiplex.Business.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Multiplex.IdentityServer
{
    [AttributeUsage(AttributeTargets.Method
    | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionsEnum permission)
           : base(permission.ToString()) { }
    }
}
