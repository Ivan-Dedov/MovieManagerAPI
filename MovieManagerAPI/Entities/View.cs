using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagerAPI.Entities
{
    /// <summary>
    /// Describes a single view, which is a 'Movie-Person' pair (i.e. an element of a many-to-many relationship).
    /// </summary>
    public class View
    {
        #region Properties

        /// <summary>
        /// The unique ID of the person who watched this movie.
        /// </summary>

        public int PersonId { get; set; }
        /// <summary>
        /// The unique ID of the movie which this person has watched.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// User's rating of this movie.
        /// </summary>
        public int Rating { get; set; }

        #endregion
    }
}