using System;
using System.Collections.Generic;

namespace Product_Store.Models.Tables
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int ProducerId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Discription { get; set; } = null!;
        public int Number { get; set; }
        public string Price { get; set; } = null!;

        public virtual Producer Producer { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
