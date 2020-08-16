using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentGroupMVC.Models;

namespace StudentGroupMVC.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult CreateStudent()
        {
            object obj = null;
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost:56825/api/Group/GetAllGroups").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<List<Group>>(jsonString);
                ViewBag.Groups = obj;
            }

            return View();
        }
        [HttpPost]
        public IActionResult CreateStudent(CreateStudentModel createStudent)
        {
            using (HttpClient client =new HttpClient())
            {
                Student student = new Student()
                {
                    Name = createStudent.Name,
                    Email = createStudent.Email,
                    GroupId = createStudent.GroupId
                };
                var data = JsonConvert.SerializeObject(student);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var resp = client.PostAsync("http://localhost:56825/api/student/createstudent", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
;            }
            return View();
        }
    }
}