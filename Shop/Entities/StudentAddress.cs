using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class StudentAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}
