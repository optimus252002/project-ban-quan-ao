using Microsoft.AspNetCore.Mvc;
using Product_Store.Infratructure;
using Product_Store.Models.Tables;
using Product_Store.ViewModel.Cart;

namespace Product_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly product_storeContext _context;
        public CartController(product_storeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            CartViewModel cartViewModel = new CartViewModel()
            {
                Items = cart,
                GrandTotal = cart.Sum(x => x.Number * x.Price),
            };
            return View(cartViewModel);
        }
        public async Task<IActionResult> Add(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            CartItems cartItem = cart.Where(c=>c.ProductId == id).FirstOrDefault();
            if(cartItem == null)
            {
                cart.Add(new CartItems()
                {
                    ProductId = id,
                    ProductName = product.Name,
                    Number = 1,
                    Price = Convert.ToDecimal(product.Price),
                    Image = product.Image
                });
            }
            else
            {
                cartItem.Number += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart");

            CartItems cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Number > 1)
            {
                --cartItem.Number;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

    }
}
