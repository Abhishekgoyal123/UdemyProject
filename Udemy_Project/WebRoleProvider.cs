using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Udemy_Project.Models;
namespace Udemy_Project
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string UserName)
        {
            
            UdemyEntities4 context = new UdemyEntities4();
            
                var result = (from user in context.Users
                              join rolemapping in context.RoleMappings on user.UserId equals rolemapping.UserId
                              join roles in context.Roles on rolemapping.RoleId equals roles.RoleId
                              where user.UserName == UserName
                              select roles.RoleName).ToArray();
            return result;
            
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] abc = GetRolesForUser(username);

            if (roleName == abc[0])
            {
                return true;
            }
            else
                return false;
            
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}