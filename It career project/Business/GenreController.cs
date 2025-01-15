using It_career_project.Data;
using It_career_project.Data.Models;
using System;
using System.Linq;

namespace It_career_project.Business
{
    /// <summary>
    /// Business logic for the table Genres
    /// </summary>
    public class GenreController
    {
        private VideoGamePlatformContext context;

        /// <summary>
        /// Constructer used in tests
        /// </summary>
        public GenreController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get a genre by id
        /// </summary>
        /// <param name="id">the id of the genre</param>
        /// <returns>the genre which matched the id</returns>
        public Genre GetGenreById(int id)
        {
            Genre genre =
                context
                .Genres
                .FirstOrDefault(x => x.Id == id);

            return genre;
        }

        /// <summary>
        /// Gets a genre by it's name
        /// </summary>
        /// <param name="genreName">is used to find the genre</param>
        /// <returns>The genre with the specified name</returns>
        public Genre GetGenreByName(string genreName)
        {
            if (string.IsNullOrEmpty(genreName))
            {
                throw new ArgumentException("Genre cannot be empty!");
            }

            Genre queryGenre =
                context
                .Genres
                .FirstOrDefault(x => x.Name == genreName);

            if (queryGenre == null)
            {
                throw new ArgumentException("Invalid genre!");
            }

            return queryGenre;
        }
    }
}