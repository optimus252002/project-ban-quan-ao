using System;
using System.Collections.Generic;

namespace Product_Store.Models.Tables
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; } = null!;
        public int Number { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
