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

namespace URLShortener.Service.Functions
{
    public class Get
    {
        private readonly IUrlMappinRepository _repository;
        public Get(IUrlMappinRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("Get")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{path}")] HttpRequest req,
            string path,
            ILogger log)
        {
            var result = await _repository.Get(path);
            return new RedirectResult(result.Url);
        }
    }
}
