using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagerAPI.Entities
{
    /// <summary>
    /// Describes a single 'Genre-Movie' pair (i.e. an element of a many-to-many relationship between
    /// genres and movies).
    /// </summary>
    public class GenredMovie
    {
        #region Properties

        /// <summary>
        /// The ID of the movie that corresponds to the chosen genre.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// The ID of this movie's genre.
        /// </summary>
        public int GenreId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new item of 'Genre-Movie' relationship.
        /// </summary>
        public GenredMovie(int movieId, int genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
        
        #endregion
    }
}
