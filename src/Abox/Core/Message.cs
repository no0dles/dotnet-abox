namespace Abox.Core
{
    public class Message<TValue>
    {
        public string Key { get; set; }
        public TValue Value { get; set; }
    }
}