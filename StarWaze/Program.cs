using System;
using System.Threading.Tasks;
using StarWaze.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using StarWaze.Gateway;

namespace StarWaze
{
    class Program
    {
        static ServiceProvider serviceProvider;

        static async Task Main(string[] args)
        {
            Setup();

            await Prompt();
        }

        private static async Task Prompt()
        {
            Console.WriteLine("Welcome to StarWaze space trip calculator!\n");

            var service = serviceProvider.GetService<IStarshipService>();

            var distance = ReadDistance();

            Console.WriteLine("\nCalculating...\n");

            foreach (var result in await service.GetRequiredStops(distance))
            {
                Console.WriteLine($"{result.Key}: {result.Value}");
            }
        }

        private static void Setup()
        {
            serviceProvider = new ServiceCollection()
                .AddSingleton<HttpClient>()
                .AddScoped<ISWApiGateway, SWApiGateway>()
                .AddScoped<IStarshipService, StarshipService>()
                .BuildServiceProvider();
        }

        private static decimal ReadDistance()
        {
            bool validInput = false;
            decimal result = 0;

            do
            {
                Console.Write("Please enter the distance in MGLT: ");
                var input = Console.ReadLine();

                if ((validInput = decimal.TryParse(input, out result)) == false)
                {
                    Console.WriteLine("\nPlease enter a valid decimal number.\n");
                }
            } while(!validInput);

            return result;
        }
    }
}
