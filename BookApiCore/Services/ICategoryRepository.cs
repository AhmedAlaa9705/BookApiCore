using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
   public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        IEnumerable<Category> GetCategoriesOfabook(int bookId);
        IEnumerable<Book> GetBooksforCategory(int categortId);
        bool CategoryExisit(int categoryId);
        bool IsDoublcateCategoryName(int categoryId, string categoryName);
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);
        void Save();

    }
}
