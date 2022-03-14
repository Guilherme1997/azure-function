using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NotificacaoAzuereFunction
{

    public static class NotificacaoAzuereFunction
    {

        [FunctionName("NotificacaoAzuereFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
   
            HttpClientHandler clientHandler = new HttpClientHandler();
            
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            dynamic jsonObject = new {msg: "Estado do Paciente atual: Estável"};

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5084/notificacao", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return new OkObjectResult(responseString);
        }
    }
}
