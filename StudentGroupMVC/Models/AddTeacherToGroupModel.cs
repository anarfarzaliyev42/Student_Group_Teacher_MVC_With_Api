using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentGroupMVC.Models
{
    public class AddTeacherToGroupModel
    {
        public List<Group> Groups { get; set; }

        public int GroupId { get; set; }
        public List<Teacher> Teachers { get; set; }

        public int TeacherId { get; set; }
    }
}
