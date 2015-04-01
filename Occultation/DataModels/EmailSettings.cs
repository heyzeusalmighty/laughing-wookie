using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occultation.DataModels
{
    public class EmailSettings
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string AdminEmail { get; set; }

    }
}