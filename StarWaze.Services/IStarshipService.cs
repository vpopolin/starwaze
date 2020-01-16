using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWaze.Services
{
    public interface IStarshipService
    {
        Task<List<KeyValuePair<string, string>>> GetRequiredStops(decimal distance);
    }
}