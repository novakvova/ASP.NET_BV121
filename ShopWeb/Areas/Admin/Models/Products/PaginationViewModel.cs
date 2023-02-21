using System;

namespace ShopWeb.Areas.Admin.Models.Products
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
