using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentGroupMVC.Models;
using StudentGroupMVC.ViewModels;
namespace StudentGroupMVC.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        [HttpGet]
        public IActionResult CreateTeacher()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTeacher(CreateTeacherViewModel createTeacher)
        {
            using (HttpClient client = new HttpClient())
            {
                Teacher teacher = new Teacher()
                {
                    Name =createTeacher.Name,
                    Email = createTeacher.Email
                };
                var data = JsonConvert.SerializeObject(teacher);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var resp = client.PostAsync("http://localhost:56825/api/teacher/CreateTeacher", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
