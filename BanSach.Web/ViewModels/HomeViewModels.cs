using BanSach.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanSach.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Book> FeaturedBooks { get; set; }
    }
}