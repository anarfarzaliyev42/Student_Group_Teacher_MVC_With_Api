using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentGroupMVC.Models;
using StudentGroupMVC.ViewModels;

namespace StudentGroupMVC.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel createRoleModel)
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
        [HttpGet]
        public IActionResult AssignRoleToUser()
        {
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            AssignRoleToUserViewModel model = new AssignRoleToUserViewModel()
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult AssignRoleToUser(AssignRoleToUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByIdAsync(model.UserId).Result;
                var role = roleManager.FindByIdAsync(model.RoleId).Result;
                
                if (user != null && role != null)
                {
                    var result=userManager.AddToRoleAsync(user,role.Name).Result;
                    if (result.Succeeded)
                    {

                    return RedirectToAction("Index", "Home");
                    }

                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(model.Username, model.Password, false, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser() { UserName = model.Username };
                var result = userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Logout()
        {

            signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }
    }
}