using Abox.Data.Models;

namespace Abox.Auth.Models
{
    public class User : Document
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}