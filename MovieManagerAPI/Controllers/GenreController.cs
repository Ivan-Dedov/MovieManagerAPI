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
    /// Controller for CRUD actions involving genres.
    /// </summary>
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        /// <summary>
        /// Asynchronously retrieve the info about the provided genre if exists.
        /// </summary>
        /// <param name="genre">The name of the genre.</param>
        /// <returns>The info about the genre, if it exists.</returns>
        [HttpGet]
        public async Task<IEnumerable<Genre>> GetGenreAsync(string genre = null)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            
            if (genre is null)
            {
                return await dbConnection.QueryAsync<Genre>(
                    @"SELECT * FROM genre;");
            }
            return await dbConnection.QueryAsync<Genre>(
            @"SELECT * FROM genre
                  WHERE name = @Name", 
            new
                {
                    Name = genre
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreByIdAsync(int id)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            try
            {
                return (await dbConnection.QueryAsync<Genre>(
                    @"SELECT * FROM genre
                  WHERE id = @Id",
                    new
                    {
                        Id = id

                    }
                )).First();
            }
            catch
            {
                return NotFound("Genre with this ID does not exist.");
            }
        }

        /// <summary>
        /// Asynchronously inserts a new genre to the database.
        /// </summary>
        /// <param name="genre">The name of the genre to insert (must be unique - if not, returns error code 400).</param>
        [HttpPost]
        public async Task<IActionResult> InsertGenreAsync([FromQuery] string genre)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            try
            {
                await dbConnection.QueryAsync<Genre>(
                    @"INSERT INTO genre (name) VALUES (@Name);",
                    new
                    {
                        Name = genre
                    });
                return Ok();
            }
            catch
            {
                return BadRequest("This genre already exists.");
            }
        }

        /// <summary>
        /// Asynchronously deletes a genre from the database.
        /// </summary>
        /// <param name="genre">The name of the genre to delete.</param>
        [HttpDelete]
        public async Task DeleteGenreAsync([FromBody] string genre)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            await dbConnection.QueryAsync(
                @"DELETE FROM genre WHERE name = @Name;",
                new
                {
                    Name = genre
                });
        }
    }
}