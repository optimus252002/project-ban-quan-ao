using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Product_Store.Models;
using Product_Store.Models.Tables;


namespace Product_Store.Controllers;

public class UserController : Controller
{
    private readonly ILogger<ProductstoreController> _logger;
    private readonly product_storeContext _context;
    public UserController(ILogger<ProductstoreController> logger, product_storeContext context)
    {
        _logger = logger;
        _context = context;
    }


    public IActionResult Login()
    {
        return View();
    }
    public async Task<ActionResult> XuLyLogin(UserViewModel formdata, int Phone, string Password)
    {


        var user = _context.Users.FirstOrDefault(c => c.PhoneNumber == Phone);
        if (user.Password == Password && user.Role == 1)
        {

            //  HttpContext.Session.SetString("nameUser", user.Name);
            // User.Identity.Name("nameUser");
            return new RedirectResult(url: "/admin/categories");
        }
        else
        {
            return new RedirectResult(url: "/Productstore");
        }


        
    }

    public partial class UserViewModel
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Role { get; set; }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
