﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
