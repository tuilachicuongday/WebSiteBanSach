using BanSach.Database;
using BanSach.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace BanSach.Services
{
    public class CategoryService
    {
        public static CategoryService Instance
        {
            get
            {
                if (instance == null) instance = new CategoryService();
                return instance;
            }
        }
        private static CategoryService instance { get; set; }
        private CategoryService()
        {
        }
        public Category GetCategory(int ID)
        {
            using (var context = new BanSachContext())
            {
            return context.Categories.Find(ID);
            }
        }
        public List<Category> GetAllCategories()
        {
            using (var context = new BanSachContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetCategories(string Search, int pageNo)
        {
            int pageSize = 5;
            using (var context = new BanSachContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return context.Categories.Where(category => category.Name != null && category.Name.ToLower().Contains(Search.ToLower())).OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Books).ToList();
                }
                else
                {
                    return context.Categories.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Books).ToList();
                }
            }
        }
        public int GetCategoriesCount(string Search)
        {

            using (var context = new BanSachContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return context.Categories.Where(category => category.Name != null && category.Name.ToLower().Contains(Search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new BanSachContext())
            {
                return context.Categories.Where(x=>x.isFeatured).ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            using ( var context = new BanSachContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new BanSachContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (var context = new BanSachContext())
            {
                var category = context.Categories.Where(x=>x.ID ==ID).Include(x=> x.Books).FirstOrDefault();
                context.Books.RemoveRange(category.Books);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
