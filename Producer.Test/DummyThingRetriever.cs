using Producer.Controllers;

namespace Producer.Test
{
    internal class DummyThingRetriever : IThingRetriever
    {
        public TheThing Get(string id)
        {
            if (id == "foo")
            {
                return new TheThing {Value = "bar"};
            }

            return null;
        }
    }
}