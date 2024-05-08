using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MovieFunc
{
    public class Function1
    {
        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-16532.c251.east-us-mz.azure.redns.redis-cloud.com:16532,password=kuLu8SrX5mvhtwYaSxf8TZNsl4fJFW96");


        public static dynamic SingleData { get; set; }
        public static dynamic Data { get; set; }

        [FunctionName("Function1")]
        public void Run([QueueTrigger("movie", Connection = "MyAzureStorage")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var db = redis.GetDatabase();

            Task.Run(async () =>
            {
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=3b5c7291&s={myQueueItem}&plot=full");

                var str = await response.Content.ReadAsStringAsync();

                var searchData = JsonConvert.DeserializeObject<dynamic>(str);

                var posterUrl = "";

                if (searchData.Response == "True")
                {
                    var imdbID = searchData.Search[0].imdbID;

                    HttpResponseMessage posterResponse = await httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=3b5c7291&i={imdbID}&plot=full");

                    var posterStr = await posterResponse.Content.ReadAsStringAsync();

                    var posterData = JsonConvert.DeserializeObject<dynamic>(posterStr);

                    posterUrl = posterData.Poster;

                    posterUrl = posterUrl.Replace("{", "").Replace("}", "");
                }

                HashEntry[] hashEntries = new HashEntry[]
                {
                    new HashEntry($"{myQueueItem}", $"{posterUrl}")
                };

                db.HashSet("person", hashEntries);
            });
        }
    }
}
