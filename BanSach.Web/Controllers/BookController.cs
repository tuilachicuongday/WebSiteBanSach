using BanSach.Entities;
using BanSach.Services;
using BanSach.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanSach.Web.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookTable(string Search, int? pageNo)
        {

            BookSearchViewModel model = new BookSearchViewModel();
            model.Search = Search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = BookService.Instance.GetBooksCount(Search);
            model.Books = BookService.Instance.GetBooks(Search, pageNo.Value) ;
            if (model.Books != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 5);

                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            NewBookViewModel model = new NewBookViewModel();

            model.AvailableCategories = CategoryService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewBookViewModel model)
        {
            var newBook = new Book();
            newBook.Name = model.Name;
            newBook.Author = model.Author;
            newBook.Description = model.Description;
            newBook.Price = model.Price;
            newBook.Category = CategoryService.Instance.GetCategory(model.CategoryID);
            newBook.ImageURL = model.ImageURL;

            BookService.Instance.SaveBook(newBook);
            return RedirectToAction("BookTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditBookViewModel model = new EditBookViewModel();

            var book = BookService.Instance.GetBook(ID);

            model.ID = book.ID;
            model.Author = book.Author;

            model.Name = book.Name;
            model.Description = book.Description;
            model.Price = book.Price;
            model.CategoryID = book.Category != null ? book.Category.ID : 0;
            model.ImageURL = book.ImageURL;
            model.AvailableCategories = CategoryService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditBookViewModel model)
        {
            var existProduct = BookService.Instance.GetBook(model.ID); 
            existProduct.Name = model.Name;
            existProduct.Description = model.Description;
            existProduct.Price = model.Price;
            existProduct.Author = model.Author;
            existProduct.Category = CategoryService.Instance.GetCategory(model.CategoryID);
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existProduct.ImageURL = model.ImageURL;
            }
            BookService.Instance.UpdateBook(existProduct);
            return RedirectToAction("BookTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            BookService.Instance.DeleteBook(ID);
            return RedirectToAction("BookTable");
        }
        [HttpGet]
        public ActionResult Detail(int ID)
        {
            BookViewModel model = new BookViewModel();

            model.Book = BookService.Instance.GetBook(ID);

            return View(model);
        }

    }
}