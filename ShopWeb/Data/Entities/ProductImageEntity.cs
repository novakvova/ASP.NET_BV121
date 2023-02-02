﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWeb.Data.Entities
{
    [Table("tblProductImages")]
    public class ProductImageEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public int Priority { get; set; }
        [ForeignKey("Product")]
        public int ? ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
