using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Product_Store.Models;

namespace Product_Store.Controllers;

public class OrderController : Controller
{
    private readonly ILogger<ProductstoreController> _logger;

    public OrderController(ILogger<ProductstoreController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}