using BanSach.Entities;
using BanSach.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanSach.Web.ViewModels
{
    public class ShopViewModel
    {
        public string Search { get; set; }

        public int MaximumPrice { get; set; }
        public List<Category> FeatureCategory { get; set; }
        public List<Book> Books { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }

        public  Pager Pager { get; set; }
    }
    public class FilterProductsViewModel
    {
        public string Search { get; set; }

        public List<Book> Books { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get;  set; }
        public int? CategoryID { get;  set; }
    }
    public class CheckoutViewModel
    {
        public List<Book> CartProducts { get; set; }

        public List<int> CartProductIDs { get; set; }

        public ApplicationUser User { get; set; }
    }
}