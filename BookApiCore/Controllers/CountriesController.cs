using BookApiCore.Dtos;
using BookApiCore.Models;
using BookApiCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        private readonly ICountryRepository countryRepository;
        private readonly IAutherRepository autherRepository;



        public CountriesController(ICountryRepository _countryRepository,IAutherRepository _autherRepository)
        {
            countryRepository = _countryRepository;
            autherRepository = _autherRepository;

        }
        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = countryRepository.GetCountries();
            if (!ModelState.IsValid)
                return BadRequest();

            List<CountryDto> list = new List<CountryDto>();
            foreach (var country in countries)
            {
                var coudto = new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name,


                };
                list.Add(coudto);
            }

             
            return Ok(list);

        }
        [HttpGet("{countryId}",Name = "GetCountry")]
        public IActionResult GetCountry(int countryId)
        {
            if (!countryRepository.CountryExisit(countryId))
                return NotFound();

            var country = countryRepository.GetCountry(countryId);
            var countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name
            };

            return Ok(countryDto);
        }
        [HttpGet("authers/{autherId}")]
        public IActionResult GetAutherOfCountry(int autherId)
        {
            if (autherRepository.AuthersExisit(autherId))
                return NotFound();

            var country = countryRepository.GetCountryofAuther(autherId);
            if (!ModelState.IsValid)
                return BadRequest();
            var countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name
            };

            return Ok(countryDto);
        }
        [HttpGet("{countryId}/authers")]
        public IActionResult GetAuthersFromCountry(int countryId)
        {
            if (!countryRepository.CountryExisit(countryId))
                return NotFound();
            var authers = countryRepository.GetAutherFromCountry(countryId);
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
        public IActionResult Create([FromBody]Country newCountry)
        {
            if (newCountry == null)
                return BadRequest();
            var country = countryRepository.GetCountries().Where(a => a.Name.Trim().ToUpper() == newCountry.Name.Trim().ToUpper()).FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", $"Country{newCountry.Name} Alredy Exis");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

    
                 countryRepository.Create(newCountry);
            countryRepository.Save();

            return CreatedAtRoute("GetCountry", new { countryId = newCountry.Id }, newCountry);
        }
        [HttpPut("{countryId}")]
        public IActionResult Update(int countryId,[FromBody]Country updatecountry)
        {
            if (updatecountry == null)
                return BadRequest();

            if (!countryRepository.CountryExisit(countryId))
                return NotFound();


            if (countryRepository.IsDoublcateCountryName(countryId, updatecountry.Name))
            {
                ModelState.AddModelError("", $"countryName Already {updatecountry.Name}Exist");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            countryRepository.Update(updatecountry);
                   countryRepository.Save(); 
            return NoContent();
        }
        [HttpDelete("{countryId}")]
        public IActionResult Delete(int countryId)
        {
            if (!countryRepository.CountryExisit(countryId))
                return NotFound();

            var country = countryRepository.GetCountry(countryId);
            if (countryRepository.GetAutherFromCountry(countryId).Count() > 0)
            {
                ModelState.AddModelError("", $"country{country.Name} can not be deleted becase it use by at least one auther went wrong deleting");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

            countryRepository.Delete(countryId);
            countryRepository.Save();
            return NoContent();
         
        }
    }
}
