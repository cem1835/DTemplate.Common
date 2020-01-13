using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DTemplate.Common.Caching;
using DTemplate.Common.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestCreator _request;
        private readonly IProductService _productService;


        public HomeController(ILogger<HomeController> logger, IRequestCreator request,IProductService productService)
        {
            _logger = logger;
            _request = request;
            _productService = productService;
        }

        
        public async Task<IActionResult> Index()
        {
            _logger.LogWarning("My Test Logging");

            var data = _productService.Get();

            var res  =  await _request.RequestResponse<Product, ProductResult>(data.Data);

            return View(data.Data);
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
