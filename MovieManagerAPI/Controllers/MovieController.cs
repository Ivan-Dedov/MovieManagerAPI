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
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    { 
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Movie>(
                @"SELECT * FROM movie;"
                );
        }

        [HttpGet("{id}")]
        public async Task<Movie> GetMovies(int id)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return (await dbConnection.QueryAsync<Movie>(
                @$"SELECT * FROM movie
                    WHERE id = {id};"
                )).First();
        }

        [HttpGet("{name}")]
        public async Task<IEnumerable<Movie>> GetMovies(string name)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            return await dbConnection.QueryAsync<Movie>(
                @$"SELECT * FROM movie
                    WHERE name = '{name}';");
        }

        [HttpPost]
        public async Task AddMovie(Movie movie)
        {
            using var dbConnection = new NpgsqlConnection(Database.ConnectionString);
            await dbConnection.QueryAsync<Movie>(
                @$"INSERT INTO movie (title, year, duration, director_id, rating) VALUES
                    ('{movie.Title}', {movie.Year}, {movie.Duration.Seconds}, {movie.DirectorId}, {movie.Rating});");
        }
    }
}
