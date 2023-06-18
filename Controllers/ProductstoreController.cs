using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Store.Models;
using Product_Store.Models.Tables;
using Product_Store.ViewModel;

namespace Product_Store.Controllers;

public class ProductstoreController : Controller
{
    private readonly product_storeContext _context;
    private readonly ILogger<ProductstoreController> _logger;

    public ProductstoreController(ILogger<ProductstoreController> logger, product_storeContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        //b1 tao doi tuong
        var categoriesproducersViewModel = new CategoriesProducersProductsViewModel
        {
            products = _context.Products.ToList(),
            categories = _context.Categories.ToList(),
            producers = _context.Producers.ToList()
        };
        return View(categoriesproducersViewModel);
    }


    public async Task<IActionResult> ChiTiet(int id)
    {

        // var Product = _context.Products.Where(c => c.Id == id).FirstOrDefault();
        try
        {
            var categoriesproducersViewModel = new CategoriesProducersProductsViewModel
            {
                product = await _context.Products.Where(c => c.Id == id).FirstOrDefaultAsync(),
                categories = await _context.Categories.ToListAsync(),
                producers = await _context.Producers.ToListAsync()
            };
            return View(categoriesproducersViewModel);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }      
    }
    public IActionResult Search(string s, int idc, int ida)
    {
        try
        {
            var products = _context.Products.Where(c => c.Name == s || c.CategoryId == idc || c.ProducerId == ida).ToList();
            // var category = _context.Books.Where(c => c.CategoryId == id).ToList();

            var CategoriesProducersProductsViewModel = new CategoriesProducersProductsViewModel
            {
                products = products,
                categories = _context.Categories.ToList(),
                producers = _context.Producers.ToList()
            };
            // categoriesauthorsViewModel.books = books;

            return View(CategoriesProducersProductsViewModel);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }
}