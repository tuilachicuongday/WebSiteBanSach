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
    public class BookService
    {
        public static BookService Instance
        {
            get
            {
                if (instance == null) instance = new BookService();
                return instance;
            }
        }
        private static BookService instance { get; set; }
        private BookService()
        {
        }

        public int GetMaximumPrice()
        {
            using (var context = new BanSachContext())
            {
                return (int)context.Books.Max(x => x.Price);
            }
        }

        public List<Book> SearchBook(string search, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int pageNo, int pageSize)
        {
            using (var context = new BanSachContext())
            {
                var books = context.Books.ToList();
                if (categoryID.HasValue)
                {
                    books = books.Where(x=>x.Category.ID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    books = books.Where(x=>x.Name.ToLower().Contains(search.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    books = books.Where(x => x.Price >=minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    books = books.Where(x => x.Price <= maximumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            books = books.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            books = books.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            books = books.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return books.Skip((pageNo -1)*pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchBookCount(string search, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new BanSachContext())
            {
                var books = context.Books.ToList();
                if (categoryID.HasValue)
                {
                    books = books.Where(x => x.Category.ID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    books = books.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    books = books.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    books = books.Where(x => x.Price <= maximumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            books = books.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            books = books.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            books = books.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return books.Count;
            }
        }

        public Book GetBook(int ID)
        {
            using (var context = new BanSachContext())
            {
                return context.Books.Where(x=>x.ID ==ID).Include(x=>x.Category).FirstOrDefault();
            }
        }
        public List<Book> GetAllBooks()
        {
            using (var context = new BanSachContext())
            {
                return context.Books.ToList();
            }
        }

        public List<Book> GetBooksByCategory(int categoryID, int pageSize, int ID)
        {
            using(var context = new BanSachContext())
            {
                return context.Books.Where(x=>x.Category.ID == categoryID && x.ID != ID ).OrderByDescending(x=>x.ID).Take(pageSize).Include(x=>x.Category).ToList();
            }
        }
        public List<Book> GetBooks(string Search, int pageNo)
        {
            int pageSize = 5;
            using (var context = new BanSachContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return context.Books.Where(book => book.Name != null && book.Name.ToLower().Contains(Search.ToLower())).OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                }
                else
                {
                    return context.Books.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                }
            }
        }
        public List<Book> GetBooks(List<int> IDs)
        {
            using (var context = new BanSachContext())
            {
                return context.Books.Where(book => IDs.Contains(book.ID)).ToList();
            }
        }
        public List<Book> GetBooks(int pageSize)
        {
            using (var context = new BanSachContext())
            {
                return context.Books.OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        public List<Book> GetLatestBooks(int numberOfBooks)
        {
            using (var context = new BanSachContext())
            {
                return context.Books.OrderByDescending(x=>x.ID).Take(numberOfBooks).Include(x => x.Category).ToList();
            }
        }

        public int GetBooksCount(string Search)
        {

            using (var context = new BanSachContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return context.Books.Where(book => book.Name != null && book.Name.ToLower().Contains(Search.ToLower())).Count();
                }
                else
                {
                    return context.Books.Count();
                }
            }
        }
        public void SaveBook(Book book)
        {
            using (var context = new BanSachContext())
            {
                context.Entry(book.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Books.Add(book);
                context.SaveChanges();
            }
        }
        public void UpdateBook(Book book)
        {
            using (var context = new BanSachContext())
            {
                context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteBook(int ID)
        {
            using (var context = new BanSachContext())
            {

                var book = context.Books.Find(ID);

                context.Books.Remove(book);
                context.SaveChanges();
            }
        }
    }
}
