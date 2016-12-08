using Abox.Auth.Models;

namespace Abox.Data.Models
{
    public class DataPermission : Permission
    {
        public bool Owner { get; set; }
    }
}