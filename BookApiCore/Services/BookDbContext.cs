using BookApiCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class BookDbContext :DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext>options)
            :base(options)
        {
            Database.Migrate();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Auther> Authers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<BookAuther> BookAuthers { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });


            modelBuilder.Entity<BookCategory>()
                .HasOne(a => a.Book)
                .WithMany(bc => bc.BookCategories)
                .HasForeignKey(f => f.BookId);

            modelBuilder.Entity<BookCategory>()
             .HasOne(a => a.Category)
             .WithMany(bc => bc.BookCategories)
             .HasForeignKey(f => f.CategoryId);

            modelBuilder.Entity<BookAuther>()
                .HasKey(ba => new { ba.BookId, ba.AutherId });

            modelBuilder.Entity<BookAuther>()
                .HasOne(a => a.Book)
                .WithMany(ba => ba.BookAuthers)
                .HasForeignKey(f => f.BookId);

            modelBuilder.Entity<BookAuther>()
                .HasOne(a => a.Auther)
                .WithMany(ba => ba.BookAuthers)
                .HasForeignKey(f => f.AutherId);






        


        }
    }
    
}
