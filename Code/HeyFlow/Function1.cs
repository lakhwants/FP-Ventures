using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;

namespace HeyFlow
{
	public static class Function1
    {
        [Function("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpResponseMessage req,
            FunctionContext executionContext)
        {
          //  var logger = executionContext.GetLogger("Function1");
           // logger.LogInformation("C# HTTP trigger function processed a request.");

            //   return Request.CreateResponse(HttpStatusCode.OK);

            //			var response = req.CreateResponse(HttpStatusCode.OK);

            return (ActionResult)new OkObjectResult("");
            //return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
