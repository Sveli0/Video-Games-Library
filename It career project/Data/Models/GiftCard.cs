using System.ComponentModel.DataAnnotations;

namespace It_career_project.Data.Models
{
    public class GiftCard
    {
        /// <summary>
        /// Constructor for Business
        /// </summary>
        /// A constructor that takes paramaters and is used in Controller classes for ease of access
        public GiftCard(string code, decimal value)
        {
            Code = code;
            Value = value;
        }
        /// <summary>
        /// Constructor for Migration
        /// </summary>
        /// An empty constructor that is used when migrating the database
        public GiftCard()
        {

        }
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        public decimal Value { get; set; }
    }
}