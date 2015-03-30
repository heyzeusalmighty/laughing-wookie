using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreSpace.Models
{
    public class SimpleUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}