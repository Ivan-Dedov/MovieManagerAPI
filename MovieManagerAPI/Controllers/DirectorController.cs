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
    [Route("api/director")]
    [ApiController]

    public class DirectorController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Director>> GetDirectorsAsync()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Director>(
                @"SELECT * FROM director;");
        }

        [HttpGet("{id}")]
        public async Task<Director> GetDirectorAsync(int id)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return (await dbConnection.QueryAsync<Director>(
                @"SELECT * FROM director
                     WHERE id = @Id;",
                new
                {
                    Id = id
                }
                )).First();
        }
        
        [HttpGet("name")]
        public async Task<IEnumerable<Director>> GetDirectorByNameAsync(string firstName = null,
            string middleName = null, string lastName = null)
        {
            // Parsing request.
            int numParams = 0;
            string query = "SELECT * FROM director WHERE ";
            if (firstName != null)
            {
                query += $"first_name = '{firstName}'";
                numParams++;
            }
            if (middleName != null)
            {
                if (numParams != 0)
                {
                    query += " AND ";
                }
                query += $"middle_name = '{middleName}'";
                numParams++;
            }
            if (lastName != null)
            {
                if (numParams != 0)
                {
                    query += " AND ";
                }
                query += $"last_name = '{lastName}'";
            }

            // Executing query.
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Director>(query);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDirectorAsync([FromBody] Director director)
        {
            try
            {
                string query = @$"INSERT INTO director (first_name, middle_name, last_name, date_of_birth)
                                 VALUES('{director.FirstName}', '{director.MiddleName}', '{director.LastName}', :dateOfBirth); ";
                var dbConnection = new NpgsqlConnection(Database.ConnectionString);
                dbConnection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, dbConnection);
                NpgsqlParameter param = new NpgsqlParameter(":dateOfBirth", NpgsqlTypes.NpgsqlDbType.Date);
                param.Value = director.DateOfBirth;
                cmd.Parameters.Add(param);
                await cmd.ExecuteNonQueryAsync();
                await dbConnection.CloseAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("This director already exists.");
            }
        }
    }
}
