using Models.SongkickEntities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongkickAPI.Interfaces
{
    public interface ILocationServiceApi
    {
        Task<IEnumerable<LocationCity>> GetByName(string locationName);
    }
}
