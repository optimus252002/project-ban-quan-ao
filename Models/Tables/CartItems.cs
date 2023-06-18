using System.ComponentModel.DataAnnotations;

namespace Product_Store.Models.Tables
{
    public class CartItems
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set;}
        public string Image { get; set; }

        public CartItems()
        {

        }
        public CartItems(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Number = 1;
            Price = Convert.ToDecimal(product.Price);
            Image = product.Image;
        }

    }
}
