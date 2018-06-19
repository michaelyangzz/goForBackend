using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;


            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }


            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            var tokenResponse =  tokenClient.RequestClientCredentialsAsync("api1").Result;

    


            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine(tokenResponse.IdentityToken);


            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response =  client.GetAsync("http://localhost:5001/api/identity").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }


            Console.ReadKey();
        }
    }
}
