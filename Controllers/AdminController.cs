using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Product_Store.Models;
using Product_Store.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Product_Store.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<ProductstoreController> _logger;
    private readonly product_storeContext _context;

    public AdminController(ILogger<ProductstoreController> logger, product_storeContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    //category
    public IActionResult Categories()
    {
        try
        {
            //b1 tao doi tuong
            var categoriesViewModel = new CategoriesViewModel();
            //b2 load du lieu tu db
            var categories = _context.Categories.ToList();
            //b3 ganw laij duwx lieeuj vao doi tuong
            categoriesViewModel.categories = categories;
            //b4 return l
            return View(categoriesViewModel);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }
    public IActionResult CreateCategory()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SaveNewCategory(FormSaveNewCategory formData)
    {
        try
        {
            string url = "";
            if (formData.name != null)
            {

                // push data vào database
                _context.Categories.Add(new Category
                {
                    Name = formData.name,
                    Description = formData.description
                });
                await _context.SaveChangesAsync();
                url = "/admin/categories";
                // back to categories views
                return Redirect(url);

            }
            url = "/admin/createCategories";
            return Redirect(url);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }

    }


    public IActionResult UpdateCategory(int id)
    {

        var category = _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        if (category != null)
        {
            var viewModel = new UpdateCategoryviewmodel();
            viewModel.category = category;
            // viewModel.Description = Description;

            return View(viewModel);
        }
        return new RedirectResult(url: "/Admin/Categories");

    }
    [HttpPost]
    public async Task<IActionResult> SaveUpdateCategoryAsync(FormSaveNewCategory formData, int id)
    {
        if (formData.name != null)
        {

            var newCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            newCategory.Name = formData.name;
            newCategory.Description = formData.description;

            _context.Categories.Attach(newCategory);
            await _context.SaveChangesAsync();
            return Redirect(url: "/admin/categories");

        }
        return Redirect(url: "/admin/UpdateCategories");
    }

    public async Task<IActionResult> DeleteCategory(int id)
    {

        var category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return Redirect(url: "/Admin/Categories");

    }




    //Producer
    public async Task<IActionResult> ProducersAsync()
    {
        try
        {
            //b1 tao doi tuong
            var producersViewModel = new ProducersViewModel();
            //b2 load du lieu tu db
            var Producers = await _context.Producers.ToListAsync();
            //b3 ganw laij duwx lieeuj vao doi tuong
            producersViewModel.producers = Producers;
            //b4 return l
            return View(producersViewModel);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
        

    }

    public IActionResult CreateProducer()
    {
        return View();
    }
    [HttpPost]
    public async Task<RedirectResult> SaveNewProducerAsync(FormSaveNewProducer formData)
    {
        try
        {
            if (formData.name != null)
            {

                // push data vào database
                _context.Producers.Add(new Producer
                {
                    Name = formData.name,
                    Description = formData.description
                });
                await _context.SaveChangesAsync();
                // back to categories views
                return new RedirectResult(url: "/admin/Producers");

            }
            return new RedirectResult(url: "/admin/createProducers");
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
        
    }
    public async Task<IActionResult> UpdateProducer(int id)
    {
        
            var Producer = await _context.Producers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (Producer != null)
            {
                var viewModel = new UpdateProducerviewmodel();
                viewModel.producer = Producer;
                return View(viewModel);
            }
            return new RedirectResult(url: "/Admin/Producers");
        

    }
    public async Task<IActionResult> SaveUpdateProducer(FormSaveNewProducer formData, int id)
    {
        if (formData.name != null)
        {
            
                var newProducer = _context.Producers.FirstOrDefault(c => c.Id == id);
                newProducer.Name = formData.name;
                newProducer.Description = formData.description;

                _context.Producers.Attach(newProducer);
                _context.SaveChanges();
                return new RedirectResult(url: "/admin/producers");
            
        }
        return new RedirectResult(url: "/admin/updateproducer");
    }
    public RedirectResult DeleteProducer(int id)
    {
        
            var producer = _context.Producers.Where(c => c.Id == id).FirstOrDefault();
            if (producer != null)
            {

                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
            return new RedirectResult(url: "/Admin/Producers");
        
    }






    public IActionResult Products()
    {
        
            //b1 tao doi tuong
            var productsViewModel = new ProductsViewModel();
            //b2 load du lieu tu db
            var products = _context.Products.ToList();
            //b3 ganw laij duwx lieeuj vao doi tuong
            productsViewModel.products = products;
            productsViewModel.categories = _context.Categories.ToList();
            productsViewModel.producers = _context.Producers.ToList();
            //b4 return l
            return View(productsViewModel);
        
    }
    //Product
    public IActionResult Product()
    {
        
            //b1 tao doi tuong
            var productsViewModel = new ProductsViewModel();
            //b2 load du lieu tu db
            var products = _context.Products.ToList();
            //b3 ganw laij duwx lieeuj vao doi tuong
            productsViewModel.products = products;
            productsViewModel.categories = _context.Categories.ToList();
            productsViewModel.producers = _context.Producers.ToList();
            //b4 return l
            return View(productsViewModel);
       
    }
    public IActionResult CreateProduct()
    {
       
            var categoriesproducersViewModel = new CategoriesProducersViewModel
            {
                categories = _context.Categories.ToList(),
                producers = _context.Producers.ToList(),
                products = _context.Products.ToList()
            };
            return View(categoriesproducersViewModel);
        
    }
    [HttpPost]
    public async Task<IActionResult> SaveNewProduct(FormSaveNewProduct formData)
    {
        if (formData.Name != null)
        {
            
                // push data vào database
                _context.Products.Add(new Product
                {
                    Name = formData.Name,
                    CategoryId = formData.CategoryId,
                    ProducerId = formData.ProducerId,
                    Image = formData.Image,
                    Price = formData.Price,
                    Number = formData.Number,
                    Discription = formData.Discription
                });
                await _context.SaveChangesAsync();
                // back to categories views
                return Redirect(url: "/admin/Products");
            
        }
        return Redirect(url: "/admin/createProducts");
    }

    public IActionResult UpdateProduct(int id)
    {
        
            var Product = _context.Products.Where(c => c.Id == id).FirstOrDefault();
            if (Product != null)
            {
                var viewModel = new UpdateProductviewmodel();
                viewModel.product = Product;
                viewModel.categories = _context.Categories.ToList();
                viewModel.producers = _context.Producers.ToList();
                return View(viewModel);
            }
            return Redirect(url: "/Admin/Products");
        

    }

    public RedirectResult SaveUpdateProduct(FormSaveNewProduct formdata, int id)
    {
        if (formdata.Name != null)
        {
            
                var newProduct = _context.Products.FirstOrDefault(c => c.Id == id);
                newProduct.Name = formdata.Name;
                newProduct.CategoryId = formdata.CategoryId;
                newProduct.ProducerId = formdata.ProducerId;
                newProduct.Image = formdata.Image;
                newProduct.Price = formdata.Price;
                newProduct.Number = formdata.Number;
                newProduct.Discription = formdata.Discription;

                _context.Products.Attach(newProduct);
                _context.SaveChanges();
                return new RedirectResult(url: "/admin/products");
            
        }
        return new RedirectResult(url: "/admin/updateproduct");
    }

    public RedirectResult DeleteProduct(int id)
    {
        
            var producer = _context.Producers.Where(c => c.Id == id).FirstOrDefault();
            if (producer != null)
            {

                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
            return new RedirectResult(url: "/Admin/Producers");
        
    }







    public partial class FormSaveNewProducer
    {
        public string name { get; set; }
        public string description { get; set; }
    }
    public partial class FormSaveNewProduct
    {
        public int ProducerId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
        public string Price { get; set; }
        public int Number { get; set; }
    }

    public partial class FormSaveNewCategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public partial class UpdateCategoryviewmodel
    {
        public Category category { get; set; }

    }
    public partial class UpdateProducerviewmodel
    {
        public Producer producer { get; set; }

    }
    public partial class UpdateProductviewmodel
    {
        public List<Category> categories { get; set; }
        public List<Producer> producers { get; set; }
        public Product product { get; set; }

    }
    public partial class CategoriesProducersViewModel
    {
        public List<Category> categories { get; set; }
        public List<Producer> producers { get; set; }
        public List<Product> products { get; set; }
    }
    public partial class CategoriesViewModel
    {
        public List<Category> categories { get; set; }
    }

    public partial class ProducersViewModel
    {
        public List<Producer> producers { get; set; }
    }

    public partial class ProductsViewModel
    {
        public List<Category> categories { get; set; }
        public List<Producer> producers { get; set; }
        public List<Product> products { get; set; }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
