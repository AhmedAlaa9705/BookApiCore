using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class CountryRepository : ICountryRepository
    {
        private readonly BookDbContext db;
        public CountryRepository(BookDbContext _db)
        {
            db = _db;
        }

        public bool CountryExisit(int countryId)
        {
            return db.Countries.Any(c => c.Id == countryId);
        }

        public void Create(Country country)
        {
            db.Countries.Add(country);
        }

        public void Delete(int Id)
        {
          var country=  db.Countries.Where(c => c.Id == Id).FirstOrDefault();
            db.Remove(country);
        }

        public IEnumerable<Auther> GetAutherFromCountry(int countryId)
        {
            return db.Authers.Where(c => c.Country.Id == countryId).ToList();
        }

        public IEnumerable<Country> GetCountries()
        {
            return db.Countries.OrderBy(c => c.Name).ToList();
        }

        public Country GetCountry(int countryId)
        {
            return db.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public Country GetCountryofAuther(int autherId)
        {
            return db.Authers.Where(a => a.Id == autherId).Select(c => c.Country).FirstOrDefault();
        }

        public bool IsDoublcateCountryName(int countryId, string countryName)
        {
            var Country = db.Countries.Where(c => c.Name.Trim().ToUpper() == countryName.Trim().ToUpper() && c.Id != countryId).FirstOrDefault();
            return Country == null ? false : true;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Country country)
        {
            db.Countries.Update(country);
        }
    }
}
