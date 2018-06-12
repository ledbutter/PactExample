using System.Web.Http;

namespace Producer.Controllers
{
    public class TheThingController : ApiController
    {
        private readonly IThingRetriever _thingRetriever;

        public TheThingController(IThingRetriever thingRetriever)
        {
            _thingRetriever = thingRetriever;
        }

        [HttpGet]
        [Route("api/theThing/{id}")]
        public IHttpActionResult DoTheThing(string id)
        {
            var theThing = _thingRetriever.Get(id);
            return Ok(theThing);
        }
    }

    public class TheThing
    {
        public string Value { get; set; }
    }

    public interface IThingRetriever
    {
        TheThing Get(string id);
    }

    internal class RealThingRetriever : IThingRetriever
    {
        public TheThing Get(string id)
        {
            return new TheThing { Value = "not bar" };
        }
    }
}