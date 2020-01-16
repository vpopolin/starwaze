using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace StarWaze.Gateway
{
    public class SWApiGateway : ISWApiGateway
    {
        public readonly HttpClient client;

        public SWApiGateway(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<StarshipDTO>> GetStarships()
        {
            var requestUri = "https://swapi.co/api/starships/";
            var starships = new List<StarshipDTO>();

            do
            {
                var response = await this.client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();

                    var page = JsonConvert.DeserializeObject<PagedResultDTO>(stringResult);

                    starships.AddRange(page.results);
                    requestUri = page.Next;
                }
                else
                {
                    throw new System.Exception($"Call to SWApi failed with response code {response.StatusCode}.");
                }
            } while (!string.IsNullOrEmpty(requestUri));

            return starships;
        }
    }
}
