using BanSach.Entities;
using BanSach.Services;
using BanSach.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace BanSach.Web.Controllers
{
    public class CategoryController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryTable(string Search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.Search = Search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoryService.Instance.GetCategoriesCount(Search);
            model.Categories = CategoryService.Instance.GetCategories(Search, pageNo.Value);
            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo,5);

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
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newcategory = new Category();
                newcategory.Name = model.Name;
                newcategory.Description = model.Description;
                newcategory.isFeatured = model.isFeatured;
                CategoryService.Instance.SaveCategory(newcategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoryService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            
            model.isFeatured = category.isFeatured;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existCategory = CategoryService.Instance.GetCategory(model.ID);
            existCategory.Name = model.Name;
            existCategory.Description = model.Description;
            existCategory.isFeatured = model.isFeatured;
            CategoryService.Instance.UpdateCategory(existCategory);
            return RedirectToAction("CategoryTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoryService.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
        public ActionResult GetMainCategories()
        {
            var categories = CategoryService.Instance.GetAllCategories();
            return PartialView(categories);
        }
    }
}