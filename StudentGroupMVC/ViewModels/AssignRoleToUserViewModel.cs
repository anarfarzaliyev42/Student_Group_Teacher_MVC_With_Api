using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroupMVC.ViewModels
{
    public class AssignRoleToUserViewModel
    {
        public List<IdentityUser> Users { get; set; }
        public string UserId { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string RoleId { get; set; }
    }
}
