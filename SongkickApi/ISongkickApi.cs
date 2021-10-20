using Newtonsoft.Json.Linq;
using RestSharp;

namespace SongkickAPI
{
    public interface ISongkickApi
    {

        JObject ParseResult(IRestResponse response);
    }
}
