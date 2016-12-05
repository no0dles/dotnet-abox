namespace Abox.Core.Attributes
{
    public class Message : System.Attribute
    {
        public string Name { get; }

        public Message(string name)
        {
            Name = name;
        }
    }
}