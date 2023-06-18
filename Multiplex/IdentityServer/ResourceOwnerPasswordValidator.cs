using Multiplex.Business.Interfaces;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Multiplex.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    { 
        private readonly IUsuariosService usuariosService;
        public ResourceOwnerPasswordValidator(IUsuariosService usuariosService)
        {
            this.usuariosService = usuariosService;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = usuariosService.UserExists(context.UserName, context.Password);
            if (user != null)
            {
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "admin":"abonado")
                };
                context.Result = new GrantValidationResult(user.Id.ToString(),"password",claims);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Usuario no encontrado o contraseña incorrecta.");
            return Task.FromResult(context.Result);
        }
    }
}
