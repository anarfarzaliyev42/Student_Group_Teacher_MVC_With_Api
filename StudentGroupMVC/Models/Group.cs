using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroupMVC.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; }
        public ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}
