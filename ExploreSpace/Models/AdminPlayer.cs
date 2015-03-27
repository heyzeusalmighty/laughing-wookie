using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreSpace.Models
{
    public class AdminPlayer
    {
        public int PlayerId { get; set; }
        public string UserName { get; set; }
        public string ContactEmail { get; set; }
        public bool IsActive { get; set; }
        public string Roles { get; set; }

    }
}