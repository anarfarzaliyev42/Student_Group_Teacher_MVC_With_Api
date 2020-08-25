using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroupMVC.Models
{
    public class StudentGroup
    {
        public Student Student { get; set; }
        public Group Group { get; set; }
    }
}
