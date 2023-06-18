using System;
using System.Collections.Generic;

namespace Product_Store.Models.Tables
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Comment1 { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
