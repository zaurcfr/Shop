using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentAddress StudentAddress { get; set; }
    }
}
