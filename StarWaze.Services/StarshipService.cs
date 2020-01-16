using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWaze.Gateway;

namespace StarWaze.Services
{
    public class StarshipService : IStarshipService
    {
        private readonly ISWApiGateway apiGateway;

        public StarshipService(ISWApiGateway apiGateway)
        {
            this.apiGateway = apiGateway;
        }

        public async Task<List<KeyValuePair<string, string>>> GetRequiredStops(decimal distance)
        {
            var starships = await this.GetStarships();

            return starships.Select(
                s => new KeyValuePair<string, string>(s.Name, this.CalculateRequiredStops(s, distance)))
                .ToList();
        }

        private string CalculateRequiredStops(StarshipDTO starship, decimal distance)
        {
            if (!decimal.TryParse(starship.MGLT, out var mglt)||starship.Consumables.ToUpperInvariant() == "UNKNOWN")
            {
                return "Unknown";
            }

            var authonomyInDays = GetAuthonomyInDays(starship.Consumables);

            var travelHours = distance / mglt;

            var travelDays = travelHours / 24;

            return (Math.Ceiling(travelDays / authonomyInDays) - 1).ToString();
        }

        private int GetAuthonomyInDays(string consumables)
        {
            var parts = consumables.Split(' ');

            var amount = Convert.ToInt32(parts[0]);

            switch (parts[1].ToLowerInvariant())
            {
                case ("year"):
                case ("years"): return amount * 365;

                case ("month"):
                case ("months"): return amount * 30;

                case ("week"):
                case ("weeks"): return amount * 7;

                case ("day"):
                case ("days"): return amount;

                default: throw new ArgumentException($"Unexpected time unit encountered in consumables: {parts[1]}.");
            }
        }

        private Task<List<StarshipDTO>> GetStarships()
        {
            return this.apiGateway.GetStarships();
        }
    }
}
