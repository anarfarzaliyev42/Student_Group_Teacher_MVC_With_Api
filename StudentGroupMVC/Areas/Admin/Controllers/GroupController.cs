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
    [Authorize(Roles ="Admin,Manager")]
    public class GroupController : Controller
    {

        [HttpGet]
        public IActionResult CreateGroup()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult CreateGroup(CreateGroupViewModel createGroupModel)
        {
            using (HttpClient client = new HttpClient())
            {
                Group group = new Group()
                {
                    Name=createGroupModel.Name
                };
                var data = JsonConvert.SerializeObject(group);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var resp = client.PostAsync("http://localhost:56825/api/group/CreateGroup", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
;
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddTeacherToGroup()
        {
            object objGroup = null;
            object objTeacher = null;
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost:56825/api/Group/GetAllGroups").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                objGroup = JsonConvert.DeserializeObject<List<Group>>(jsonString);
                var result2 = client.GetAsync("http://localhost:56825/api/teacher/getallteachers").Result;
                var jsonString2 = result2.Content.ReadAsStringAsync().Result;
                objTeacher = JsonConvert.DeserializeObject<List<Teacher>>(jsonString2);
                ViewBag.Groups = objGroup;
                ViewBag.Teachers = objTeacher;
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddTeacherToGroup(AddTeacherToGroupViewModel addTeacher)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    GroupTeacher groupTeacher = new GroupTeacher()
                    {
                        GroupID = addTeacher.GroupId,
                        TeacherID = addTeacher.TeacherId
                    };
                    var data = JsonConvert.SerializeObject(groupTeacher);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var url = ($"http://localhost:56825/api/Group/AddTeacherToGroup?groupId={groupTeacher.GroupID}&teacherId={groupTeacher.TeacherID}");
                    var resp = client.PostAsync($"{url}", content).Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
           
           
            return View(addTeacher);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}