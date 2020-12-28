using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RequestTests
{
    public static class WebRequests
    {
        public enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE,
            OPTIONS,
        }

        public class ApiRequest
        {
            public string AuthorizationBearerToken { get; set; }

            public string ResourceUrl { get; set; }

            public RequestType RequestType { get; set; }

            public string JsonBody { get; set; }

        }

        public async static Task<HttpResponseMessage> MakeRequest(ApiRequest request)
        {
            return await MakeRequest(request.RequestType, request.ResourceUrl, request.JsonBody, request.AuthorizationBearerToken);
        }
        public async static Task<HttpResponseMessage> MakeRequest(RequestType type, string url, string jsonBody, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            switch (type)
            {
                case RequestType.GET:
                    return await client.GetAsync(new Uri(url).ToString());
                case RequestType.POST:
                    return await client.PostAsync(new Uri(url).ToString(), content);
                case RequestType.PUT:
                    return await client.PutAsync(new Uri(url).ToString(), content);
                case RequestType.DELETE:
                    return await client.DeleteAsync(new Uri(url).ToString());
                case RequestType.OPTIONS:
                default:
                    break;
            }

            throw new InvalidOperationException("Invalid or unspecified request type");
        }
         

    }       
}
