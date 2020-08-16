using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentGroupMVC.Models;

namespace StudentGroupMVC.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(CreateRoleModel createRoleModel)
        {
            IdentityRole identityRole = new IdentityRole()
            {
                Name = createRoleModel.Name
            };
            var result = roleManager.CreateAsync(identityRole).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}