using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Abox.Auth.Attributes;
using Abox.Auth.Models;

namespace Abox.Auth.Services
{
    public class PermissionService
    {
        private readonly Dictionary<Type, Permission> permissions;

        public PermissionService()
        {
            permissions = new Dictionary<Type, Permission>();
        }

        public Permission Resolve(Type type)
        {
            if (permissions.ContainsKey(type))
                return permissions[type];

            var permisson = new Permission
            {
                Claims = type
                    .GetTypeInfo()
                    .GetCustomAttributes<AuthorizeClaims>()
                    .SelectMany(a => a.Claims)
                    .ToList(),
                Roles = type
                    .GetTypeInfo()
                    .GetCustomAttributes<AuthorizeRoles>()
                    .SelectMany(a => a.Roles)
                    .ToList(),
                Anonymous = type
                    .GetTypeInfo()
                    .GetCustomAttributes<AuthorizeAnonymous>()
                    .Any()
            };

            permissions[type] = permisson;

            return permisson;
        }
    }
}