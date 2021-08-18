using BookApiCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext db;
        
        public BookRepository(BookDbContext _db)
        {
            db = _db;
        }
        public bool BookExisit(int bookId)
        {
            return db.Books.Any(b=>b.Id==bookId);
        }

        public bool BookExisit(string isbn)
        {
            return db.Books.Any(b => b.Isbn == isbn);

        }

        public bool BookExisitByIsbn(string isbn)
        {
            return db.Books.Any(b => b.Isbn == isbn);
        }

        public void CreateBook(List<int> autherId, List<int> categoryId, Book book)
        {
            var authers = db.Authers.Where(a => autherId.Contains(a.Id)).ToList();
            var categories = db.Categories.Where(a => categoryId.Contains(a.Id)).ToList();

            foreach (var auther in authers)
            {
                var bookAuthers = new BookAuther()
                {
                    Auther = auther,
                    Book = book

                };
                db.Add(bookAuthers);
            }
            foreach (var category in categories)
            {
                var bookCategory = new BookCategory()
                {
                    Category= category,
                    Book = book

                };
                db.Add(bookCategory);
            }
            db.Add(book);
        }

        public void Delete(int Id)
        {
            Book book = db.Books.Find(Id);
            db.Remove(book);
        }

        public Book GetBook(int id)
        {
            return db.Books.Where(a => a.Id == id).SingleOrDefault();
        }

        public Book GetBook(string isbn)
        {
            return db.Books.Where(a => a.Isbn == isbn).SingleOrDefault();

        }

        public decimal GetBookRating(int bookid)
        {
            var rev= db.Reviews.Where(a => a.Book.Id == bookid);
            if (rev.Count() <= 0)
                return 0;
            return ((decimal)rev.Sum(r => r.Rating) / rev.Count());
           
        }

        public IEnumerable<Book> GetBooks()
        {
            return db.Books.ToList();
        }

        public bool IsdublcateIsbn(int id, string isbn)
        {
            var book = db.Books.Where(a => a.Isbn.Trim().ToUpper() == isbn.Trim().ToUpper() && a.Id != id).FirstOrDefault();
            return book == null ? false : true;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(List<int> autherId, List<int> categoryId, Book book)
        {
            var authers = db.Authers.Where(a => autherId.Contains(a.Id)).ToList();
            var categories = db.Categories.Where(a => categoryId.Contains(a.Id)).ToList();

            var bookAuthersToDel = db.BookAuthers.Where(a => a.BookId == book.Id);
            var bookCategoryDel = db.BookCategories.Where(a => a.BookId == book.Id);

            db.RemoveRange(bookAuthersToDel);
            db.RemoveRange(bookCategoryDel);

            foreach (var auther in authers)
            {
                var bookAuthers = new BookAuther()
                {
                    Auther = auther,
                    Book = book

                };
                db.Add(bookAuthers);
            }
            foreach (var category in categories)
            {
                var bookCategory = new BookCategory()
                {
                    Category = category,
                    Book = book

                };
                db.Add(bookCategory);
            }
            db.Update(book); ;
        }
    }
}
