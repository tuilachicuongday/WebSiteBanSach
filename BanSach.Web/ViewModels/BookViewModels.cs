using BanSach.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanSach.Web.ViewModels
{
    public class BookSearchViewModel
    {
        public List<Book> Books { get; set; }
        public string Search { get; set; }
        public Pager Pager { get; set; }
    }
    public class NewBookViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public string Author { get; set; }
        public List<Category> AvailableCategories { get; set; }

    }
    public class EditBookViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public  int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public string Author { get; set; }

        public List<Category> AvailableCategories { get; set; }

    }
    public class BookViewModel
    {
        public Book Book { get; set; }

    }
}