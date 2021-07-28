using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace MovieManagerAPI.Entities
{
    /// <summary>
    /// Describes a single film genre.
    /// </summary>
    public class Genre
    {
        #region Fields

        /// <summary>
        /// The unique name of the genre.
        /// </summary>
        private string name;

        #endregion

        #region Properties

        /// <summary>
        /// The unique ID of the genre.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The unique name of the genre. Throws ArgumentException if name is null, empty or fully whitespace.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Genre name cannot be null, empty or consist of whitespace characters.");
                }

                name = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new genre with specified ID and name.
        /// </summary>
        private Genre(int id, string name)
            : this(name)
        {
            Id = id;
        }

        /// <summary>
        /// Creates a new genre with a specified name.
        /// </summary>
        public Genre(string name)
        {
            Name = name;
        }

        #endregion
    }
}