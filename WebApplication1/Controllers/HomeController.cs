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

        private readonly IRequestCreator _requestCreator;

        public HomeController(ILogger<HomeController> logger, IRequestCreator requestCreator)
        {
            _logger = logger;
            _requestCreator = requestCreator;
        }

        public async Task<IActionResult> Index()
        {
            var product = new Product();

            var res  =  await _requestCreator.CreateRequest<Product, string>(product);

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
