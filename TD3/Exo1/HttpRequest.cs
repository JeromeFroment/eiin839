using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Exo1
{
    public class HttpRequest
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=b5ce880020619178373c7cfe3ad23d8977f763e7");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                // Deserialization of JSON repsonse
                List<Contrat> contrats = JsonSerializer.Deserialize<List<Contrat>>(responseBody);

                // Write results in console
                Console.WriteLine("List of contracts : \n");
                foreach(Contrat c in contrats)
                    Console.WriteLine(c.ToString());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
