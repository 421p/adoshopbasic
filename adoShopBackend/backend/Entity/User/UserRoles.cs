using System;
using System.Linq;

namespace backend.Entity.User
{
    public static class UserRole
    {
        public static readonly string Manager = "user_role_manager";
        public static readonly string Operator = "user_role_operator";

        public static void AssertUserRole(string role)
        {
            if (!new[] {Manager, Operator}.Contains(role))
            {
                throw new Exception("Illegal user role.");
            }
        }
    }
}
