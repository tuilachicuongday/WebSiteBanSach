using BanSach.Entities;
using BanSach.Services;
using BanSach.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanSach.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index(string search, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();

            ShopViewModel model = new ShopViewModel();

            model.Search = search;
            model.FeatureCategory = CategoryService.Instance.GetFeaturedCategories();
            model.MaximumPrice = BookService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = BookService.Instance.SearchBookCount(search, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Books = BookService.Instance.SearchBook(search, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount,pageNo, pageSize);
            return View(model);
        }
        public ActionResult FilterBooks(string search, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();

            FilterProductsViewModel model = new FilterProductsViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            model.Search = search;
            int totalCount = BookService.Instance.SearchBookCount(search, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Books = BookService.Instance.SearchBook(search, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var CartProductsCookie = Request.Cookies["CartProducts"];
            if(CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x=> int.Parse(x)).ToList();
                model.CartProducts = BookService.Instance.GetBooks(model.CartProductIDs);
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }
        public JsonResult Order(string bookIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(bookIDs))
            {
                var bookQuantities = bookIDs.Split('-').Select(x => int.Parse(x)).ToList();
                var boughtBooks = BookService.Instance.GetBooks(bookQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = boughtBooks.Sum(x => x.Price * bookQuantities.Where(productID => productID == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtBooks.Select(x => new OrderItem() { BookID = x.ID, Quantity = bookQuantities.Where(productID => productID == x.ID).Count() }));

                var rowOrder = ShopService.Instance.SaveOrder(newOrder);

                result.Data = new { Success = true,Rows = rowOrder };
            }
            else
            {
                result.Data = new { Success = false };

            }

            return result;
        }
    }
}