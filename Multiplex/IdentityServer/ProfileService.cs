using Multiplex.Business.Interfaces;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Multiplex.IdentityServer
{
    public class ProfileService : IProfileService
    {
        public ProfileService() {}

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var claims = new List<Claim>();

                claims.AddRange(context.IssuedClaims.Select(x => x).ToList());
                claims.AddRange(context.Subject.Claims.Select(x => x).ToList());

                var usersRoles = claims
                    .Where(x => x.Type == ClaimTypes.Role)
                    .Select(x => x.Value)
                    .ToList();

                context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception x)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}
