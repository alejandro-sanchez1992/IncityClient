using System;
using System.Collections.Generic;
using IncityClient.Common.Models;

namespace IncityClient.Prism.Helpers
{
    public static class CombosHelper
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Id = 1, Name = Languages.Employee }
            };
        }
    }
}
