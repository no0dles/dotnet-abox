using System.Collections.Generic;

namespace Abox.Auth.Models
{
    public class Authorization
    {
        public bool Anonymous { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }

        public Authorization()
        {
            Anonymous = true;
            Roles = new List<string>();
            Claims = new List<string>();
        }
    }
}