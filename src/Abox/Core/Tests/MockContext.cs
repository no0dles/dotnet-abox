namespace Abox.Core.Tests
{
    public class MockContext : Context
    {
        public MockContext()
            : base(new MockModule())
        {
        }
    }
}