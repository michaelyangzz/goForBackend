﻿using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //aa().Wait();
            bb();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static void bb()
        {
            HttpClient c = new HttpClient();

            var r = c.GetAsync("https://localhost:44342/api/t/aa").Result;

           

        

        }


        async static Task aa()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5001");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}

            //Console.WriteLine(tokenResponse.Json);

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("bob", "password2", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5002/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

        }
    }
}
