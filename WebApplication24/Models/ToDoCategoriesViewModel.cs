using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication24.Models
{
    public class ToDoCategoriesViewModel
    {
        public IEnumerable<ToDoCategory> Categories { get; set; }
    }
}