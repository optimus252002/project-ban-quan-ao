using Product_Store.Models.Tables;

namespace Product_Store.ViewModel.Cart
{
    public class CartViewModel
    {
        public List<CartItems>? Items { get; set; }
        public decimal? GrandTotal { get; set; }

    }
}
