using Microsoft.Extensions.Configuration;

namespace ClientGateway.Helpers
{
    public class ApiConfig
    {
        public string IdentityApiUrl { get; private set; }
        public string SearchApiUrl { get; private set; }
        public string PostsApiUrl { get; private set; }
        public string ImagesApiUrl { get; private set; }

        public ApiConfig(IConfiguration configuration)
        {
            IdentityApiUrl = configuration["IdentityApiUrl"];
            SearchApiUrl = configuration["SearchApiUrl"];
            PostsApiUrl = configuration["PostsApiUrl"];
            ImagesApiUrl = configuration["ImagesApiUrl"];
        }
    }
}
