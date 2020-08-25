using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentGroupMVC.Models;
using StudentGroupMVC.ViewModels;

namespace StudentGroupMVC.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            List<StudentGroup> obj = null;
          
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost:56825/api/student/GetStudentsWithGroups").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<List<StudentGroup>>(jsonString);

            }
            return View(obj);
        }


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
        public IActionResult CreateStudent(CreateStudentViewModel createStudent)
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            object obj = null;
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync($"http://localhost:56825/api/student/getstudentbyid/{id}").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<Student>(jsonString);
                ViewBag.Students = obj;
            }
            
            return View(obj);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            using (HttpClient client = new HttpClient())
            {
               
                var data = JsonConvert.SerializeObject(student);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var resp = client.PostAsync($"http://localhost:56825/api/student/EditStudent/{student.Id}", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
;
            }

            return View(student);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {
            object obj = null;
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync($"http://localhost:56825/api/student/getstudentbyid/{id}").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<Student>(jsonString);
                ViewBag.Students = obj;
            }

            return View(obj);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteStudent(Student student)
        {
            using (HttpClient client = new HttpClient())
            {

                var data = JsonConvert.SerializeObject(student);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var resp = client.PostAsync($"http://localhost:56825/api/student/deletestudent/{student.Id}", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllStudent");
                }
            }
            return View();
        }
    }
}