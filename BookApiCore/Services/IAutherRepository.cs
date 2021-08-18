using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public interface IAutherRepository
    {
        IEnumerable<Auther> GetAuthers();
        Auther GetAuther(int id);
        IEnumerable<Auther> GetAuthersOfBook(int bookId);
        IEnumerable<Book> GetBooksByAuther(int autherId);
        bool AuthersExisit(int autherId);
        void Insert(Auther auther);
        void Update(Auther auther);
        void Delete(Auther auther);
        void Save();
    }
}
