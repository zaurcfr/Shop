using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Teacher : User
    {
        public string LessonName { get; set; }
        public string Department { get; set; }
    }
}
