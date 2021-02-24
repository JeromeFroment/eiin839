using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Exo3
{
    public class HttpRequest
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task Main(string contract, string station)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations/" + station + "?contract=" + contract + "&apiKey=b5ce880020619178373c7cfe3ad23d8977f763e7");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                // Deserialization of JSON repsonse
                Station s = JsonSerializer.Deserialize<Station>(responseBody);

                // Write results in console
                Console.WriteLine(s.ToString());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
