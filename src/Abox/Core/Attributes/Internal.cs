namespace Abox.Security.Attributes
{
    public class Internal : System.Attribute
    {
        public bool Export { get; set; }

        public Internal(bool export = false)
        {
            Export = export;
        }
    }
}