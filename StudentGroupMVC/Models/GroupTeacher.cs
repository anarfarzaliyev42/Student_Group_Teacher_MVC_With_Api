using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroupMVC.Models
{
    public class GroupTeacher
    {
        public virtual Teacher Teacher { get; set; }

        public int? TeacherID { get; set; }

        public virtual Group Group { get; set; }

        public int? GroupID { get; set; }

    }
}
