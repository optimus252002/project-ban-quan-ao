using Product_Store.Models.Tables;

namespace Product_Store.ViewModel
{
    public class CategoriesProducersProductsViewModel
    {
        public List<Category> categories { get; set; }
        public List<Producer> producers { get; set; }
        public List<Product> products { get; set; }
        public Product product { get; set; }
    }
}
