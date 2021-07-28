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
    /// Describes a single user.
    /// </summary>
    public class Person
    {
        #region Fields

        /// <summary>
        /// The minimum date of birth allowed for users.
        /// </summary>
        private static readonly DateTime MinimumDateOfBirth = new DateTime(1900, 1, 1);

        /// <summary>
        /// The unique username of the user.
        /// </summary>
        private string username;
        /// <summary>
        /// The first name of the user.
        /// </summary>
        private string firstName;
        /// <summary>
        /// The last name of the user.
        /// </summary>
        private string lastName;
        /// <summary>
        /// The date of birth of the user.
        /// </summary>
        private DateTime dateOfBirth;

        #endregion

        #region Properties

        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The unique e-mail of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The unique username name of the user. Throws ArgumentException if name is null, empty or fully whitespace.
        /// </summary>
        public string Username
        {
            get => username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be null, empty or consist of whitespace characters.");
                }

                username = value;
            }
        }

        /// <summary>
        /// The first name of the user. Throws ArgumentException if name is null, empty or fully whitespace.
        /// </summary>
        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "First name cannot be null, empty or consist of whitespace characters.");
                }

                firstName = value;
            }
        }

        /// <summary>
        /// The last name of the user. Throws ArgumentException if name is null, empty or fully whitespace.
        /// </summary>
        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name cannot be null, empty or consist of whitespace characters.");
                }

                lastName = value;
            }
        }

        /// <summary>
        /// The date of birth of the user. Optional.
        /// </summary>
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (value < MinimumDateOfBirth || value > DateTime.Today)
                {
                    throw new ArgumentException("Invalid date of birth.");
                }

                dateOfBirth = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new user with the specified ID, email, username, first and last names, and date of birth.
        /// </summary>
        private Person(int id, string email, string username, string firstName, string lastName, DateTime dateOfBirth)
            : this(email, username, firstName, lastName, dateOfBirth)
        {
            Id = id;
        }

        /// <summary>
        /// Creates a new user with the specified email, username, first and last names.
        /// </summary>
        public Person(string email, string username, string firstName, string lastName)
        {
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Creates a new user with the specified email, username, first and last names and date of birth.
        /// </summary>
        public Person(string email, string username, string firstName, string lastName, DateTime dateOfBirth)
            : this(email, username, firstName, lastName)
        {
            DateOfBirth = dateOfBirth;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously retrieves all movies watched by this user.
        /// </summary>
        /// <returns>An IEnumerable containing all movies watched by this user.</returns>
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Movie>(
                @"SELECT *
                  FROM view
                  JOIN movie ON view.movie_id = movie.id AND view.person_id = @Id");
        }

        #endregion
    }
}