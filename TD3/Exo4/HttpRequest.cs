using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace Exo4
{
    public class HttpRequest
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations/" + args[1] + "?contract=" + args[0] + "&apiKey=b5ce880020619178373c7cfe3ad23d8977f763e7");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                // Deserialization of JSON repsonse
                Station s = JsonSerializer.Deserialize<Station>(responseBody);

                response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations?contract=" + args[0] + "&apiKey=b5ce880020619178373c7cfe3ad23d8977f763e7");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                List<Station> stations = JsonSerializer.Deserialize<List<Station>>(responseBody);

                 s.GetClosestStation(stations);

                // Write results in console
                Console.WriteLine("Station choisie : \n" + s.ToString());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
