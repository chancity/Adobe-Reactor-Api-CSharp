﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace AdobeLoginBase
{
    public class LoginClient
    {
        private readonly ILoginClient _client;
        public LoginClient(HttpClient httpClient)
        {
            _client = RestService.For<ILoginClient>(httpClient);
        }

        public LoginClient(string url, HttpMessageHandler innerHandler)
            : this(new HttpClient(innerHandler) { BaseAddress = new Uri(url)})
        {}

        public Task<AccessToken> Login(string clientId, string clientSecret, string jwtToken)
        {
            return _client.Login(clientId, clientSecret, jwtToken);
        }
    }

    internal interface ILoginClient
    {
        [Multipart]
        [Post("/ims/exchange/jwt/")]
        Task<AccessToken> Login([AliasAs("client_id")] string clientId, [AliasAs("client_secret")] string clientSecret,
            [AliasAs("jwt_token")] string jwtToken);
    }
}