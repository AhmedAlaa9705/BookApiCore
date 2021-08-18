using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int countryId);
        Country GetCountryofAuther(int autherId);
        IEnumerable< Auther> GetAutherFromCountry(int countryId);
        bool CountryExisit(int countryId);
        bool IsDoublcateCountryName(int countryId, string countryName);
        void Create(Country country);
        void Update(Country country);
        void Delete(int Id);
        void Save();
    }
}
