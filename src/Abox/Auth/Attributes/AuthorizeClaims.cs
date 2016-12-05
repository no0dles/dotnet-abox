namespace Abox.Auth.Attributes
{
    public class AuthorizeClaims : System.Attribute
    {
        public string[] Claims { get; }

        public AuthorizeClaims(params string[] claims)
        {
            Claims = claims;
        }
    }
}