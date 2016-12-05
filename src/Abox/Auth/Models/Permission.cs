using System.Collections.Generic;

namespace Abox.Auth.Models
{
    public class Permission
    {
        public bool Anonymous { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}