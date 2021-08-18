using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class AutherRepository : IAutherRepository
    {
        private readonly BookDbContext db;
        public AutherRepository(BookDbContext _db)
        {
            db = _db;
        }
        public bool AuthersExisit(int autherId)
        {
            return db.Authers.Any(a => a.Id == autherId);
        }

        public void Delete(Auther auther)
        {
            db.Remove(auther);
        }

        public Auther GetAuther(int id)
        {
            return db.Authers.Where(a => a.Id == id).SingleOrDefault();
        }

        public IEnumerable<Auther> GetAuthers()
        {
            return db.Authers.OrderBy(a=>a.LastName).ToList();
        } 

        public IEnumerable<Auther> GetAuthersOfBook(int bookId)
        {
            return db.BookAuthers.Where(a => a.Book.Id == bookId).Select(a => a.Auther).ToList();
        }

        public IEnumerable<Book> GetBooksByAuther(int autherId)
        {
            return db.BookAuthers.Where(a => a.AutherId == autherId).Select(a => a.Book).ToList();
        }

        public void Insert(Auther auther)
        {
            db.Add(auther);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Auther auther)
        {
            db.Update(auther);
        }
    }
}
