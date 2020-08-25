using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Threading.Tasks;
using StudentGroupMVC.Models;
namespace StudentGroupMVC.ViewModels
{
    public class AddTeacherToGroupViewModel
    {
        public List<Group> Groups { get; set; }
        [Required(ErrorMessage ="Please select group")]
        public int? GroupId { get; set; }
        public List<Teacher> Teachers { get; set; }
        [Required(ErrorMessage = "Please select teacher")]
        public int? TeacherId { get; set; }
    }
}
