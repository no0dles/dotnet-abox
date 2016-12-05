namespace Abox.Auth.Attributes
{
    public class AuthorizeRoles : System.Attribute
    {
        public string[] Roles { get; }

        public AuthorizeRoles(params string[] roles)
        {
            Roles = roles;
        }
    }
}