using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWaze.Gateway
{
    public interface ISWApiGateway
    {
        Task<List<StarshipDTO>> GetStarships();
    }
}
