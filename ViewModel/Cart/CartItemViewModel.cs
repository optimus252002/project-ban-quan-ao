namespace Product_Store.ViewModel.Cart
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? Number {  get; set; }
        public decimal? Price { get; set; }
        public string? Image { get;set; }
        public decimal? Total { get { return Price* Number; } }

    }
}
