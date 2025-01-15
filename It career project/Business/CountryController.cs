using System;
using System.Collections.Generic;
using System.Linq;
using It_career_project.Data;
using It_career_project.Data.Models;

namespace It_career_project.Business
{
    public class CountryController
    {
        private VideoGamePlatformContext context;

        public CountryController()
        {
            context = new VideoGamePlatformContext();
        }

        public CountryController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        public Country GetCountryByName(string countryName)
        {
            Country queryCountry =
                context
                .Countries
                .FirstOrDefault(x => x.Name == countryName);

            if (queryCountry == null)
            {
                throw new ArgumentException("Invalid country!");
            }

            return queryCountry;
        }

        public Country GetCountryByCountryCode(string countryCode)
        {
            Country country =
                context
                .Countries
                .FirstOrDefault(x => x.CountryCode == countryCode);

            return country;
        }
    }
}