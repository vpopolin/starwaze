using System;
namespace StarWaze.Gateway
{
    public class PagedResultDTO
    {
        public int Count { get; set; }

        public string Next { get; set; }

        public StarshipDTO[] results { get; set; }
    }
}