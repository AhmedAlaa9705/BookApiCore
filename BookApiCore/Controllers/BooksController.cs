using BookApiCore.Dtos;
using BookApiCore.Models;
using BookApiCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAutherRepository autherRepository;
        private readonly IReviewRepository reviewRepository;
        public BooksController(IBookRepository _bookRepository, ICategoryRepository _categoryRepository, IAutherRepository _autherRepository, IReviewRepository _reviewRepository)
        {
            bookRepository = _bookRepository;
            categoryRepository = _categoryRepository;
            autherRepository = _autherRepository;
            reviewRepository = _reviewRepository;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {

            var books = bookRepository.GetBooks();
            if (!ModelState.IsValid)
                return BadRequest();
            List<BookDto> list = new List<BookDto>();
            foreach (var book in books)
            {
                var BookDto = new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Title = book.Title,
                    DatePublished = book.DatePublished
                };
                list.Add(BookDto);

            }
            return Ok(list);

        }
        [HttpGet("id/{bookId}", Name = "GetBook")]
        public IActionResult GetBook(int bookId)
        {
            if (!bookRepository.BookExisit(bookId))
                return NotFound();
            var book = bookRepository.GetBook(bookId);
            if (!ModelState.IsValid)
                return BadRequest();
            var BookDto = new BookDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                DatePublished = book.DatePublished
            };
            return Ok(BookDto);
        }
        [HttpGet("isbn/{isbn}")]
        public IActionResult GetBook(string isbn)
        {
            if (!bookRepository.BookExisit(isbn))
                return NotFound();
            var book = bookRepository.GetBook(isbn);
            if (!ModelState.IsValid)
                return BadRequest();
            var BookDto = new BookDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                DatePublished = book.DatePublished
            };
            return Ok(BookDto);
        }
        [HttpGet("{bookId}/rating")]
        public IActionResult GetBookRating(int bookId)
        {
            if (!bookRepository.BookExisit(bookId))
                return NotFound();
            var rating = bookRepository.GetBookRating(bookId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(rating);


        }


        private StatusCodeResult ValidateBook(List<int> authId, List<int> catId, Book book)
        {
            if (book == null || authId.Count() <= 0 || catId.Count() <= 0)
            {
                ModelState.AddModelError("", "Missing book ,auther or actegory");
                return BadRequest();
            }
            if (bookRepository.IsdublcateIsbn(book.Id, book.Isbn))
            {
                ModelState.AddModelError("", "Dublcate Isbn");
                return StatusCode(422);
            }
            foreach (var id in authId)
            {
                if (!autherRepository.AuthersExisit(id))
                {
                    ModelState.AddModelError("", "auther not found");
                    return StatusCode(404);
                }
            }
            foreach (var id in catId)
            {
                if (!categoryRepository.CategoryExisit(id))
                {
                    ModelState.AddModelError("", "category not found");
                    return StatusCode(404);
                }
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Critical error");
                return BadRequest();
            }
            return NoContent();

        }
        [HttpPost]
        public IActionResult Create([FromQuery] List<int> authId, [FromQuery] List<int> catId, [FromBody] Book book)
        {
            var statusCode = ValidateBook(authId, catId, book);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (ModelState.IsValid)
                bookRepository.CreateBook(authId, catId, book);
            bookRepository.Save();



            return CreatedAtRoute("GetBook", new { BookId = book.Id }, book);

        }



        [HttpPut("{bookId}")]
        public IActionResult Update(int bookId, [FromQuery] List<int> authId, [FromQuery] List<int> catId, [FromBody] Book book)
        {
            var statusCode = ValidateBook(authId, catId, book);

            if (bookId != book.Id)
                return BadRequest();

            if (!bookRepository.BookExisit(bookId))
                return NotFound();

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (ModelState.IsValid)
                bookRepository.Update(authId, catId, book);
            bookRepository.Save();



            return NoContent();

        }
        [HttpDelete("{bookId}")]
        public IActionResult Delete(int bookId)
        {
            if (!bookRepository.BookExisit(bookId))
                return NotFound();
            var reviewes = reviewRepository.GetReviewsOfAbook(bookId);
            
            if (!ModelState.IsValid)
                return BadRequest();

            reviewRepository.DeleteReviews(reviewes.ToList());
            bookRepository.Delete(bookId);
            bookRepository.Save();

            return NoContent();
        }



        //[HttpPost]
        //public IActionResult Create(Book newBook)
        //{
        //    if (newBook==null)
        //        return BadRequest();
        //    var book =bookRepository.GetBooks().Where(a => a.Isbn.Trim().ToUpper() == newBook.Isbn .Trim().ToUpper()).FirstOrDefault();

        //    if (newBook != null)
        //    {
        //        ModelState.AddModelError("", $"Book{newBook.Isbn} Alredy Exis");
        //        return StatusCode(422, ModelState);
        //    }
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //}
    } 
}
