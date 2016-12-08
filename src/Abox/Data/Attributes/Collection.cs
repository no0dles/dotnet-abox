namespace Abox.Data.Attributes
{
    public class Collection : System.Attribute
    {
        public string Name { get; set; }

        public Collection(string name)
        {
            Name = name;
        }
    }
}