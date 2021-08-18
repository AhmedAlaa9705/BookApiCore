using BookApiCore.Services;
using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore
{
    public static class DbSeedingClass
    {
        public static void SeedDataContext(this BookDbContext context)
        {

            var booksAuthors = new List<BookAuther>()
            {
                new BookAuther()
                {
                    Book = new Book()
                    {
                        Isbn = "123",
                        Title = "The Call Of The Wild",
                        DatePublished = new DateTime(1903,1,1),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Action"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { Headine = "Awesome Book", ReviewText = "Reviewing Call of the Wild and it is awesome beyond words", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "John", LastName = "Smith" } },
                            new Review { Headine = "Terrible Book", ReviewText = "Reviewing Call of the Wild and it is terrrible book", Rating = 1,
                             Reviewer = new Reviewer(){ FirstName = "Peter", LastName = "Griffin" } },
                            new Review { Headine = "Decent Book", ReviewText = "Not a bad read, I kind of liked it", Rating = 3,
                             Reviewer = new Reviewer(){ FirstName = "Paul", LastName = "Griffin" } }
                        }
                    },
                    Auther = new Auther()
                    {
                        FirstName = "Jack",
                        LastName = "London",
                        Country = new Country()
                        {
                            Name = "USA"
                        }
                    }
                },
                new BookAuther()
                {
                    Book = new Book()
                    {
                        Isbn = "1234",
                        Title = "Winnetou",
                        DatePublished = new DateTime(1878,10,1),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Western"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { Headine = "Awesome Western Book", ReviewText = "Reviewing Winnetou and it is awesome book", Rating = 4,
                             Reviewer = new Reviewer(){ FirstName = "Frank", LastName = "Gnocci" } }
                        }
                    },
                    Auther = new Auther()
                    {
                        FirstName = "Karl",
                        LastName = "May",
                        Country = new Country()
                        {
                            Name = "Germany"
                        }
                    }
                },
                new BookAuther()
                {
                    Book = new Book()
                    {
                        Isbn = "12345",
                        Title = "Pavols Best Book",
                        DatePublished = new DateTime(2019,2,2),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Educational"}},
                            new BookCategory { Category = new Category() { Name = "Computer Programming"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { Headine = "Awesome Programming Book", ReviewText = "Reviewing Pavols Best Book and it is awesome beyond words", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "Pavol2", LastName = "Almasi2" } }
                        }
                    },
                    Auther = new Auther()
                    {
                        FirstName = "Pavol",
                        LastName = "Almasi",
                        Country = new Country()
                        {
                            Name = "Slovakia"
                        }
                    }
                },
                new BookAuther()
                {
                    Book = new Book()
                    {
                        Isbn = "123456",
                        Title = "Three Musketeers",
                        DatePublished = new DateTime(2019,2,2),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Action"}},
                            new BookCategory { Category = new Category() { Name = "History"}}
                        }
                    },
                    Auther = new Auther()
                    {
                        FirstName = "Alexander",
                        LastName = "Dumas",
                        Country = new Country()
                        {
                            Name = "France"
                        }
                    }
                },
                new BookAuther()
                {
                    Book = new Book()
                    {
                        Isbn = "1234567",
                        Title = "Big Romantic Book",
                        DatePublished = new DateTime(1879,3,2),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Romance"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { Headine = "Good Romantic Book", ReviewText = "This book made me cry a few times", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "Allison", LastName = "Kutz" } },
                            new Review { Headine = "Horrible Romantic Book", ReviewText = "My wife made me read it and I hated it", Rating = 1,
                             Reviewer = new Reviewer(){ FirstName = "Kyle", LastName = "Kutz" } }
                        }
                    },
                    Auther = new Auther()
                    {
                        FirstName = "Anita",
                        LastName = "Powers",
                        Country = new Country()
                        {
                            Name = "Canada"
                        }
                    }
                }
            };

            context.BookAuthers.AddRange(booksAuthors);
            context.SaveChanges();
        }
    }
}
