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
    public class AuthersController : Controller
    {
        private readonly IAutherRepository autherRepository;
        private readonly IBookRepository bookRepository;
        private readonly ICountryRepository countryRepository;
        public AuthersController(IAutherRepository _autherRepository,IBookRepository _bookRepository,ICountryRepository _countryRepository)
        {
            autherRepository = _autherRepository;
            bookRepository = _bookRepository;
            countryRepository = _countryRepository;
        }
        [HttpGet]
        public IActionResult GetAuthers()
        {
            var authers = autherRepository.GetAuthers();
            if (!ModelState.IsValid)
                return BadRequest();
            List<AutherDto> list = new List<AutherDto>();
            foreach (var auther in authers)
            {
                var autherDto = new AutherDto
                {
                    Id = auther.Id,
                    FirstName = auther.FirstName,
                    LastName = auther.LastName
                };
                list.Add(autherDto);

            }
            return Ok(list);
        }
        [HttpGet("{autherId}",Name = "GetAuther")]
        public IActionResult GetAuther(int autherId)
        {
            if (!autherRepository.AuthersExisit(autherId))
                return NotFound();
            var auther = autherRepository.GetAuther(autherId);
            if (!ModelState.IsValid)
                return BadRequest();
            var autherDto = new AutherDto
            {
                Id = auther.Id,
                FirstName = auther.FirstName,
                LastName = auther.LastName
            };
            return Ok(autherDto);
        }
        [HttpGet("{autherId}/books")]
        public IActionResult GetBooksOfAuther(int autherId)
        {
            if (!autherRepository.AuthersExisit(autherId))
                return NotFound();
            var books = autherRepository.GetBooksByAuther(autherId);
            if (!ModelState.IsValid)
                return BadRequest();
            List<BookDto> list = new List<BookDto>();
            foreach (var book in books)
            {
                var bookDto = new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Title = book.Title,
                    DatePublished = book.DatePublished
                };
                list.Add(bookDto);

            }
            return Ok(list);
        }
        [HttpGet("books/{bookId}")]
        public IActionResult GetAuthersOfBook(int bookId)
        {
            if (!bookRepository.BookExisit(bookId))
                return NotFound();
            var authers = autherRepository.GetAuthersOfBook(bookId);
            if (!ModelState.IsValid)
                return BadRequest();
            List<AutherDto> list = new List<AutherDto>();
            foreach (var auther in authers)
            {
                var autherDto = new AutherDto
                {
                    Id = auther.Id,
                    FirstName = auther.FirstName,
                    LastName = auther.LastName
                };
                list.Add(autherDto);
            }
            return Ok(list);
        }
        [HttpPost]
        public IActionResult Ceaete(Auther auther)
        {
            if (auther == null)
                return BadRequest();

            if (!countryRepository.CountryExisit(auther.Country.Id))
            {
                ModelState.AddModelError("", "country does't Exisit You shoud put country Id");
                return StatusCode(404, ModelState);
            }
            auther.Country = countryRepository.GetCountry(auther.Country.Id);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something Went Wrong saving the auther" + $"{auther.FirstName}{auther.LastName}");
            }
            autherRepository.Insert(auther);
            autherRepository.Save();

            return CreatedAtRoute("GetAuther", new { autherId = auther.Id }, auther);
                

        }
        [HttpPut("{autherId}")]
        public IActionResult Update(int autherId, Auther auther)
        {
            if (auther == null)
                return BadRequest();
            if (autherId != auther.Id)
                return BadRequest();

            if (!autherRepository.AuthersExisit(autherId))
                ModelState.AddModelError("", "auther does not exist");

            if (!countryRepository.CountryExisit(auther.Country.Id))
                ModelState.AddModelError("", "country does't Exisit You shoud put country Id");

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            auther.Country = countryRepository.GetCountry(auther.Country.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
             
            
            autherRepository.Update(auther);
            autherRepository.Save();

            return NoContent();


        }
        [HttpDelete("{autherId}")]
        public IActionResult Delete(int autherId)
        {
      
            if (!autherRepository.AuthersExisit(autherId))
                return NotFound();

            var auther = autherRepository.GetAuther(autherId);

            if (autherRepository.GetBooksByAuther(autherId).Count() > 0)
            {
                ModelState.AddModelError("", $"Aurher can not delete this {auther.FirstName}");
                return StatusCode(409, ModelState);
            }
            if (ModelState.IsValid)
                autherRepository.Delete(auther);
            autherRepository.Save();

            return NoContent();
        }
    }
}
