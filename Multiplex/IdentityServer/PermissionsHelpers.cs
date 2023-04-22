using Multiplex.Business.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplex.IdentityServer
{
    public static class PermissionPackers
    {
        public const char PackType = 'H';
        public const int PackedSize = 4;

        public static string FormDefaultPackPrefix()
        {
            return $"{PackType}{PackedSize:D1}-";
        }

        public static string PackPermissionsIntoString(this IEnumerable<PermissionsEnum> permissions)
        {
            return permissions.Aggregate(FormDefaultPackPrefix(), (s, permission) => s + ((int)permission).ToString("X4"));
        }

        public static IEnumerable<int> UnpackPermissionValuesFromString(this string packedPermissions)
        {
            var packPrefix = FormDefaultPackPrefix();
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));
            if (packedPermissions.StartsWith(packPrefix))
            {
                int index = packPrefix.Length;
                while (index < packedPermissions.Length)
                {
                    yield return int.Parse(packedPermissions.Substring(index, PackedSize), NumberStyles.HexNumber);
                    index += PackedSize;
                }
            }
            else
                throw new InvalidOperationException("The format of the packed permissions is wrong" +
                                               $" - should start with {packPrefix}");
        }

        public static IEnumerable<PermissionsEnum> UnpackPermissionsFromString(this string packedPermissions)
        {
            return packedPermissions.UnpackPermissionValuesFromString().Select(x => ((PermissionsEnum)x));
        }

        public static PermissionsEnum? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out PermissionsEnum permission)
                ? (PermissionsEnum?)permission
                : null;
        }

    }
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }

        public string PermissionName { get; }
    }
    public static class PermissionChecker
    {
        public static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();

            if (!Enum.TryParse(permissionName, true, out PermissionsEnum permissionToCheck))
                throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(PermissionsEnum)}.");

            return usersPermissions.Contains(permissionToCheck);
        }
    }
}
