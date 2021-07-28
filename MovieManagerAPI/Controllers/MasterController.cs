using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MovieManagerAPI.Utilities;
using Npgsql;

namespace MovieManagerAPI.Controllers
{
    /// <summary>
    /// The master controller to run any PostgreSQL query to the database.
    /// </summary>
    [Route("api/master")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        /// <summary>
        /// Asynchronously runs the provided query to the database.
        /// </summary>
        /// <param name="query">The query to run.</param>
        [HttpGet]
        public async Task<IEnumerable<string>> RunQuery(string query)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<string>(query);
        }
    }
}