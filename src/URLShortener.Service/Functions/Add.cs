using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using URLShortener.Service.Contracts;
using URLShortener.Service.Models;

namespace URLShortener.Service.Functions
{
    public class Add
    {
        private readonly IUrlMappinRepository _repository;
        public Add(IUrlMappinRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("Add")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Add")] ShortUrlRequest request, 
            HttpRequest req,
            ILogger log)
        {
            var result = await _repository.Save(request.Url);

            return new OkObjectResult(result);
        }
    }
}
