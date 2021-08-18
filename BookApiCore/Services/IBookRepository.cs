using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBook(int bookid);
        Book GetBook(string isbn);
        decimal GetBookRating(int bookid);
        bool BookExisit(int bookId);
        bool BookExisit(string isbn);
        bool IsdublcateIsbn(int id, string isbn);
        void CreateBook(List<int>autherId,List<int> categoryId, Book book);
        void Update(List<int>autherId,List<int> categoryId, Book book);
        void Delete(int Id);
        void Save();


    }
}
