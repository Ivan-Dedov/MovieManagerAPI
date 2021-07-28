using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MovieManagerAPI.Entities;
using MovieManagerAPI.Utilities;
using Npgsql;

namespace MovieManagerAPI.Controllers
{
    /// <summary>
    /// Controller for CRUD actions involving users.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Asynchronously retrieve all users from the database.
        /// </summary>
        /// <returns>An IEnumerable containing all registered users.</returns>
        [HttpGet]
        public async Task<IEnumerable<Person>> GetUsersAsync()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Person>(
                @"SELECT * FROM person;");
        }

        /// <summary>
        /// Asynchronously inserts a new user to the database.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertUserAsync([FromBody] Person user)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            try
            {
                await dbConnection.QueryAsync<Person>(
                    @$"INSERT INTO person (email, username, first_name, last_name) VALUES
                        ('{user.Email}', '{user.Username}', '{user.FirstName}', '{user.LastName}');");
                return Ok();
            }
            catch
            {
                return BadRequest("This person already exists.");
            }
        }

        /// <summary>
        /// Asynchronously deletes a user from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete]
        public async Task DeleteUserAsync(int id)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            await dbConnection.QueryAsync(
                @$"DELETE FROM person
                     WHERE id = '{id}';");
        }
    }
}