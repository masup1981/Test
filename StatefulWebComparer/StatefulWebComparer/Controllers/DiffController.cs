using Microsoft.AspNetCore.Mvc;
using StatefulWebComparer.Formatter;
using StatefulWebComparer.Model;

namespace StatefulWebComparer.Controllers
{
    [ApiController]
    [Route("v1/diff")]
    public class DiffController : Controller
    {
        [HttpPost]
        [Route("{id}/left")]
        public void Left([FromRoute]string id, [FromBody]CustomType customType)
        {
            // Keep left string to compare in session with key left and ID to distinguish it by ID
            HttpContext.Session.SetString($"left-{id}", customType.Input);
        }

        [HttpPost]
        [Route("{id}/right")]
        public void Right([FromRoute]string id, [FromBody]CustomType customType)
        {
            // Keep left string to compare in session with key left and ID to distinguish it by ID
            HttpContext.Session.SetString($"right-{id}", customType.Input);
        }

        [HttpGet]
        [Route("{id}")]
        public string Get ([FromRoute]string id)
        {
            var left = HttpContext.Session.GetString($"left-{id}");
            var right = HttpContext.Session.GetString($"right-{id}");
                        
            return Diff.Compare(left, right);
        }
    }
}