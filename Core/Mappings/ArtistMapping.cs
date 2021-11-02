using Models.Entities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    public class ArtistMapping
    {
        public static Artist MapToArtist(ArtistApi artistApi)
        {
            return new Artist
            {
                ArtistApiId = artistApi.id
            };
        }
    }
}
