using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using MovieTaskPart1.Models;
using System.Diagnostics;
using System.Text;

namespace MovieTaskPart1.Controllers
{
    public class HomeController : Controller
    {

        private readonly QueueClient _queueClient;
        public HomeController()
        {
            _queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=movietask;AccountKey=E0rbZ/m2jGG8L1i/9wD5vPdvZJgRxQ8ISSbb9qrA/HDjNSV9HHXJQKZ9tm3JqXtT14SvN5+22EG7+AStqL1q7Q==;EndpointSuffix=core.windows.net", "movie");
            _queueClient.CreateIfNotExists();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string message)
        {
            var m = Convert.ToBase64String(Encoding.UTF8.GetBytes(message));        
            await _queueClient.SendMessageAsync(m);

            return View();  
        }
    }
}