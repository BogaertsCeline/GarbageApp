using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FirstProject.Model;

namespace FirstProject
{
    public static class RekenmachineFunction
    {
        [FunctionName("RekenmachineFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Rekenmachine/som")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SomRequest somRequest = JsonConvert.DeserializeObject<SomRequest>(requestBody);
                int resultaat = somRequest.Getal1 + somRequest.Getal2;
                SomResponse sr = new SomResponse()
                {
                    Getal1 = somRequest.Getal1,
                    Getal2 = somRequest.Getal2,
                    Resultaat = resultaat
                };
                return new OkObjectResult(sr);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "RekenmachineFuctions");
                return new StatusCodeResult (500);
            }
            

        }
    }
}
