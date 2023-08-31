using CodeTasks.Repository;
using CodeTasks.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RandomNameGeneratorLibrary;
using System.Runtime.CompilerServices;

namespace CodeTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // setup settings
            var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");

            var config = configuration.Build();

            // Setup connection to database.
            var mongoDb = new MongoClient(config["ConnectionStrings:Mongo"]).GetDatabase(
                "Production"
            );

            // create an instance of the spotify track repository
            var spotifyTrackRepository = new SpotifyTrackRepository(mongoDb);

            // Tasks
            var spotifyTracksTask = new SpotifyTracks(spotifyTrackRepository);
            var fizzBuzz = new FizzBuzz();
            var stringParsing = new StringParsing();
            var personGenerator = new PersonNameGenerator();

            var names = personGenerator.GenerateMultipleFirstAndLastNames(10000);
            var emails = names.Select(x =>
            {
                var suffix = new List<string>
                {
                    "@footy.com",
                    "footy.com",
                    "@footy",
                    "footy",
                    "@outlook.com",
                    "outlook.com",
                    "@outlook",
                    "outlook",
                    "@gmail.com",
                    "gmail.com",
                    "@gmail",
                    "gmail",
                };
                return $"{x.Replace(" ", "")}{suffix[new Random().Next(0, suffix.Count)]}";
            });

            _ = Task.Run(async () =>
            {
                Console.WriteLine(
                    $"[stringParsing][IsEmailValid] IsValid: {stringParsing.IsEmailValid(emails.ToList())}"
                );
                Console.WriteLine(
                    $"[FizzBuzz][GetFizzBuzz0To10000] {await fizzBuzz.GetFizzBuzz1To100()}"
                );
                Console.WriteLine(
                    $"[FizzBuzz][GetFizzBuzzWithCustom0To100(2, \"The\")] {await fizzBuzz.GetFizzBuzzWithCustom1To100(2, "The")}"
                );

                Console.WriteLine(
                    $"[FizzBuzz][GetFizzBuzzWithAllCustomFrom0ToN] {await fizzBuzz.GetFizzBuzzWithAllCustomFrom1ToN(new List<(int multiple, string word)>
                    {
                        (3, "A"),
                        (4, "String"),
                        (5, "Of"),
                        (7, "Words"),
                        (9, "Can"),
                        (13, "Be"),
                        (15, "Long")
                    }, 1000)}"
                );
                Console.WriteLine(
                    $"[SpotifyTracks][GetAllTracks()] Count: {(await spotifyTracksTask.GetAllTracks()).Count}"
                );
                Console.WriteLine(
                    $"[SpotifyTracks][GetNMostPopular(100)] {string.Join(Environment.NewLine, (await spotifyTracksTask.GetNMostPopular(100)).Select(x => $"Popularity: {x.Popularity}, TrackName: {x.TrackName}"))}"
                );
                Console.WriteLine(
                    $"[SpotifyTracks][GetAllGenreSortedAZ] {string.Join(",", await spotifyTracksTask.GetAllGenreSortedAZ())}"
                );
                Console.WriteLine(
                    $"[SpotifyTracks][GetTopNAlbumsWithTrackNamesWithTheMostTracksWithinAlbum(10)] {string.Join(Environment.NewLine, (await spotifyTracksTask.GetTopNAlbumsWithTrackNamesWithTheMostTracksWithinAlbum(10)).Select(x => $"Album: {x.albumName}, Tracks: [{x.TrackNames.Count}]"))}"
                );
                Console.WriteLine(
                    $"[SpotifyTracks][GetTheMostUsedSongTitle] {await spotifyTracksTask.GetTheMostUsedSongTitle()}"
                );
            });

            Console.WriteLine("Press any key to end the proccess");
            _ = Console.ReadKey();
        }
    }
}
