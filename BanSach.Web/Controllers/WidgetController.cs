using BanSach.Services;
using BanSach.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanSach.Web.Controllers
{
    public class WidgetController : Controller
    {       
        public ActionResult Books()
        {
            BooksWidgetViewModel model = new BooksWidgetViewModel();

                model.Books = BookService.Instance.GetBooks(10);
            return PartialView(model);
        }
        public ActionResult LatestBooks()
        {
            BooksWidgetViewModel model = new BooksWidgetViewModel();

            model.Books = BookService.Instance.GetLatestBooks(4);
            
            return PartialView(model);
        }
        public ActionResult RelateBooks(int? CategoryID, int? ID)
        {
            BooksWidgetViewModel model = new BooksWidgetViewModel();
            
            if(CategoryID.HasValue && CategoryID.Value > 0)
            {
                model.Books = BookService.Instance.GetBooksByCategory(CategoryID.Value, 4,ID.Value);

            }
            return PartialView(model);
        }
    }
}