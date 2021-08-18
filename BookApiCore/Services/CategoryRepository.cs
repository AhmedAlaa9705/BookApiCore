using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private BookDbContext db;
        public CategoryRepository(BookDbContext _db)
        {
            db = _db;
        }
        public bool CategoryExisit(int categoryId)
        {
            return db.Categories.Any(c => c.Id == categoryId);
        }

        public void Delete(Category category)
        {
           
            db.Remove(category);

        }

        public IEnumerable<Book> GetBooksforCategory(int categortId)
        {
            return db.BookCategories.Where(c => c.CategoryId == categortId).Select(a => a.Book).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.OrderBy(a=>a.Name).ToList();
        }

        public IEnumerable<Category> GetCategoriesOfabook(int bookId)
        {
            return db.BookCategories.Where(a => a.BookId == bookId).Select(c => c.Category).ToList();
        }

        public Category GetCategory(int id)
        {
            return db.Categories.Where(c => c.Id == id).SingleOrDefault();
        }

        public void Insert(Category category)
        {
            db.Categories.Add(category);
        }

        public bool IsDoublcateCategoryName(int categoryId, string categoryName)
        {
            var category = db.Categories.Where(c => c.Name.Trim().ToUpper() == categoryName.Trim().ToUpper() && c.Id != categoryId).FirstOrDefault();
            return category == null ? false : true;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Category category)
        {
            db.Update(category);
        }
    }
}
