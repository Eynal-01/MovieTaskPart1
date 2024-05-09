using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using TakeMovieApp.Entities;
using TakeMovieApp.Models;

namespace TakeMovieApp.Controllers
{
    public class HomeController : Controller
    {
        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-16532.c251.east-us-mz.azure.redns.redis-cloud.com:16532,password=kuLu8SrX5mvhtwYaSxf8TZNsl4fJFW96");

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMovie()
        {
            var db = redis.GetDatabase();

            var hashEntries = await db.HashGetAllAsync("movies");

            var movie = new Movie();

            if (hashEntries.Length > 0)
            {
                var hashEntry = hashEntries[0];

                string title = hashEntry.Name;
                string poster = hashEntry.Value;

                movie.Name = title;
                movie.Poster = poster;
            }

            return Ok(movie);
        }

        public async Task DeleteMovie()
        {
            var db = redis.GetDatabase();
            var hashEntries = await db.HashGetAllAsync("movies");

            if (hashEntries.Length > 0)
            {
                var hashEntry = hashEntries[0];
                string title = hashEntry.Name;
                string poster = hashEntry.Value;

                var movie = new Movie
                {
                    Name = title,
                    Poster = poster,
                };

                db.HashDelete("movies", movie.Name);
            }
        }
    }
}
