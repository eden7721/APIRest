using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBGList_ApiVersion.Controllers.v3
{
    [Route("v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class CodeOnDemand : ControllerBase
    {

        [HttpGet(Name = "Test2")]
        [EnableCors("AnyOrigin")]
        [ResponseCache(NoStore = true)]
        public ContentResult Test2(int? minutesToAdd = null)
        {

            DateTime ahora = DateTime.UtcNow;
            if (minutesToAdd.HasValue)
                ahora = ahora.AddMinutes(minutesToAdd.Value);
            return Content("<script>" +
            "window.alert('Your client supports JavaScript!" +
            "\\r\\n\\r\\n" +
            $"Server Time (UTC): {ahora.ToString("o")}" +
            "\\r\\n" +
            "Client time (UTC): ' + new Date().toISOString());" +
            "</script>" +
            "<noscript>Your client does not support Javascript</noscript>",
            "text/html");
        }
    }
}
