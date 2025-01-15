using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace It_career_project.Data.Models
{
    public class Country
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that is often used in the controllers for easier creation of objects of this class
        public Country(string countryCode, string name, string currency, decimal currencyExchangeRateToEuro)
        {
            CountryCode = countryCode;
            Name = name;
            Currency = currency;
            CurrencyExchangeRateToEuro = currencyExchangeRateToEuro;
            Users = new HashSet<User>();
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// Empty constructor that is necessary when migrating, instances collection of Users
        public Country()
        {
            Users = new HashSet<User>();
        }
        /// <summary>
        /// Primary Key
        /// </summary>
        /// The primary key for this table is a varchar as it makes access easier and fits the table better
        [Key]
        [MaxLength(3)]
        [Required]
        public string CountryCode { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(5)]
        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal CurrencyExchangeRateToEuro { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
