using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bankown.Controllers
{
    public class AccessToken
    {
        public string Type;
        public string Value;
    }

    public class AuthenticationInterceptor : DelegatingHandler
    {
        //public AuthenticationInterceptor(HttpClientHandler handler)
        //{
        //    InnerHandler = handler;
        //}

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                HttpController.Instance.Token = null;
                Console.WriteLine("Unauthorized");
                System.Threading.Thread.Sleep(1000);
            }

            return response;
        }
    }
    /// <summary>
    /// Singleton class <c>HttpCpntroller</c> is used for sending Http Requests
    /// </summary>
    public class HttpController
    {
        private readonly string domain = "https://bankown.mutad.ml";

        private readonly HttpClient client;
        private static HttpController singleInstance = new HttpController();
        private AccessToken access_token;

        public AccessToken Token
        {
            set => access_token = value;
            private get => access_token;
        }

        private HttpController()
        {
            client = new HttpClient(new AuthenticationInterceptor() { InnerHandler = new HttpClientHandler() });
        }

        public static HttpController Instance
        {
            get
            {
                if (singleInstance == null)
                {
                    singleInstance = new HttpController();
                }
                return singleInstance;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            // Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (Token != null)
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(Token.Type, Token.Value);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, domain+url);

            var response = await client.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string json)
        {
            // Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, domain+url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);


            return response;
        }
    }
}
