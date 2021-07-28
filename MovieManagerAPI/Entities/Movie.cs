using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MovieManagerAPI.Utilities;
using Npgsql;

namespace MovieManagerAPI.Entities
{
    /// <summary>
    /// Describes a single film.
    /// </summary>
    public class Movie
    {
        #region Fields

        /// <summary>
        /// The title of the film.
        /// </summary>
        private string title;
        /// <summary>
        /// The rating of the film - an integer between 0 and 100 (inclusive).
        /// </summary>
        private int rating;

        #endregion

        #region Properties

        /// <summary>
        /// The unique ID of the film.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The title of the film. Throws ArgumentException if title is null, empty or fully whitespace.
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be null, empty or consist of whitespace characters.");
                }

                title = value;
            }
        }

        /// <summary>
        /// The release year of the film.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The ID of the director of the film (from the 'director' table).
        /// Use the GetDirectorAsync() method to get the director.
        /// </summary>
        public int DirectorId { get; set; }

        /// <summary>
        /// The duration of the film.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The rating of the film - an integer between 0 and 100 (inclusive).
        /// Throws ArgumentException if this condition is not satisfied.
        /// </summary>
        public int Rating
        {
            get => rating;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Rating cannot be less than 0 or greater than 100.");
                }

                rating = value;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new movie with the specified ID, title, release year, ID of the director (from the
        /// 'director' table), duration and rating.
        /// </summary>
        private Movie(int id, string title, int year, TimeSpan duration, int directorId, int rating)
            : this(title, year, directorId, duration)
        {
            Id = id;
            Rating = rating;
        }

        /// <summary>
        /// Creates a new movie with the specified title, release year and duration.
        /// </summary>
        public Movie(string title, int year, TimeSpan duration)
        {
            Title = title;
            Year = year;
            Duration = duration;
        }

        /// <summary>
        /// Creates a new movie with the specified title, release year, duration and director ID (from
        /// the 'director' table).
        /// </summary>
        public Movie(string title, int year, int directorId, TimeSpan duration)
            : this(title, year, duration)
        {
            DirectorId = directorId;
        }

        /// <summary>
        /// Creates a new movie with the specified title, release year, duration and rating.
        /// </summary>
        public Movie(string title, int year, TimeSpan duration, int rating)
            : this(title, year, duration)
        {
            Rating = rating;
        }

        /// <summary>
        /// Creates a new movie with the specified title, release year, ID of the director (from the
        /// 'director' table), duration and rating.
        /// </summary>
        public Movie(string title, int year, TimeSpan duration, int directorId, int rating)
            : this(title, year, directorId, duration)
        {
            Rating = rating;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously retrieves the director of this film.
        /// </summary>
        /// <returns>The director of this film.</returns>
        public async Task<Director> GetDirectorAsync()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return (await dbConnection.QueryAsync<Director>(
                @"SELECT * FROM director
                  WHERE id = @DirectorId"
            )).First();
        }

        /// <summary>
        /// Asynchronously retrieves the genre of this film.
        /// </summary>
        /// <returns>The genre of this film.</returns>
        public async Task<Genre> GetGenreAsync()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return (await dbConnection.QueryAsync<Genre>(
                @"SELECT * FROM genre
                  WHERE id = @GenreId"
            )).First();
        }

        /// <summary>
        /// Asynchronously retrieves all people who have watched this film.
        /// </summary>
        /// <returns>An IEnumerable containing all people who have watched this film.</returns>
        public async Task<IEnumerable<Person>> GetViewerListAsync()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Person>(
                @"SELECT *
                  FROM view
                  JOIN person ON view.person_id = person.id AND view.movie_id = @Id");
        }

        #endregion
    }
}