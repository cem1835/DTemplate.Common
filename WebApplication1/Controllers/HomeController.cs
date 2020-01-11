using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DTemplate.Common.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRequestCreator _request;

        public HomeController(ILogger<HomeController> logger, IRequestCreator request)
        {
            _logger = logger;
            _request = request;
        }

        public async Task<IActionResult> Index()
        {
            var product = new Product();
            product.Name = "PC";

            var res  =  await _request.RequestResponse<Product, ProductResult>(product);

            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
