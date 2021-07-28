using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagerAPI.Entities
{
    /// <summary>
    /// Describes a single film director.
    /// </summary>
    public class Director
    {
        #region Fields

        /// <summary>
        /// The first name of the director.
        /// </summary>
        private string firstName;
        /// <summary>
        /// The last name of the director.
        /// </summary>
        private string lastName;
        /// <summary>
        /// The date of birth of the director.
        /// </summary>
        private DateTime dateOfBirth;
        
        #endregion

        #region Properties
        /// <summary>
        /// The unique ID of the director.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The first name of the director. Throws ArgumentException if name is null, empty or fully whitespace.
        /// </summary>
        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name cannot be null, empty or consist of whitespace characters.");
                }

                firstName = value;
            }
        }

        /// <summary>
        /// The middle name of the director. Optional.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The last name of the director. Throws ArgumentException if name is null, empty or fully whitespace.
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
        /// The date of birth of the director.
        /// </summary>
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (value > DateTime.Today)
                {
                    throw new ArgumentException("Invalid date of birth.");
                }

                dateOfBirth = value;
            }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Creates a new director with specified ID, first, middle and last names, and date of birth.
        /// </summary>
        private Director(int id, string firstName, string middleName, string lastName, DateTime dateOfBirth)
            : this(firstName, middleName, lastName, dateOfBirth)
        {
            Id = id;
        }

        /// <summary>
        /// Creates a new director with specified first and last names, and date of birth.
        /// </summary>
        public Director(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// Creates a new director with specified first, middle and last names, and date of birth.
        /// </summary>
        public Director(string firstName, string middleName, string lastName, DateTime dateOfBirth)
            : this(firstName, lastName, dateOfBirth)
        {
            MiddleName = middleName;
        }

        #endregion
    }
}