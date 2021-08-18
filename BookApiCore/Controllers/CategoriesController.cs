using BookApiCore.Dtos;
using BookApiCore.Models;
using BookApiCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IBookRepository bookRepository;
        public CategoriesController(ICategoryRepository _categoryRepository, IBookRepository _bookRepository)
        {
            categoryRepository = _categoryRepository;
            bookRepository = _bookRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = categoryRepository.GetCategories();
            if (!ModelState.IsValid)
                return BadRequest();

            List<CategoryDto> list = new List<CategoryDto>();

            foreach (var cat in categories)
            {
                var catDto = new CategoryDto
                {
                    Id = cat.Id,
                    Name = cat.Name
                };
                list.Add(catDto);
            }
            return Ok(list);
        }
        [HttpGet("{categoryId}", Name = "GetCategory")]
        public IActionResult GetCategory(int categoryId)
        {
            var category = categoryRepository.GetCategory(categoryId);
            if (!categoryRepository.CategoryExisit(categoryId))
                return NotFound();

            var catDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
            return Ok(catDto);
        }
        [HttpGet("books/{bookId}")]
        public IActionResult GetCategoriesforBook(int bookId)
        {
            if (!bookRepository.BookExisit(bookId))
                return NotFound();
            var categories = categoryRepository.GetCategoriesOfabook(bookId);

            List<CategoryDto> list = new List<CategoryDto>();
            foreach (var Cat in categories)
            {
                var catdto = new CategoryDto
                {
                    Id = Cat.Id,
                    Name = Cat.Name

                };
                list.Add(catdto);
            }

            return Ok(list);
        }
        [HttpGet("{categortId}/books")]
        public IActionResult GetAllBooksForCategory(int categortId)
        {
            if (!categoryRepository.CategoryExisit(categortId))
                return NotFound();
            var books = categoryRepository.GetBooksforCategory(categortId);
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
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (category == null)
                return BadRequest();

            var cat = categoryRepository.GetCategories()
                .Where(a => a.Name.Trim().ToUpper() ==
                category.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (cat != null)
            {
                ModelState.AddModelError("", $"categyName{cat.Name}is already exsist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

                categoryRepository.Insert(category);
            categoryRepository.Save();

            return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, category);


        }
        [HttpPut("{catId}")]
        public IActionResult Update(int catId, [FromBody] Category category)
        {
            if (category == null)
                return BadRequest();

            if (!categoryRepository.CategoryExisit(catId))
                return NotFound();

            if (categoryRepository.IsDoublcateCategoryName(catId, category.Name))
            {
                ModelState.AddModelError("", $"CategoryName{category.Name}is already Exist");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

            categoryRepository.Update(category);
            categoryRepository.Save();

            return NoContent();
        }
        [HttpDelete("{CateID}")]
        public IActionResult Delete(int CateID,Category category)
        {
            if (category == null)
                return BadRequest();

            if (!categoryRepository.CategoryExisit(CateID))
                return NotFound();
            if (categoryRepository.GetBooksforCategory(CateID).Count() > 0)
            {
                ModelState.AddModelError("", $"can not delete this because hava alot of categot{category.Name}is already Exist");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

            categoryRepository.Delete(category);
            categoryRepository.Save();

            return NoContent();
        }
    }
}
