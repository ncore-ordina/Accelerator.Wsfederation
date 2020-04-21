using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;

namespace STS
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
            new Client {
                ClientId = "implicitclient",
                ClientName = "Example Implicit Client",
                Enabled = true,
                Flow = Flows.Implicit,
                RequireConsent = true,
                AllowRememberConsent = true,
                RedirectUris =
                  new List<string> {"https://localhost:44304/account/signInCallback"},
                PostLogoutRedirectUris =
                  new List<string> {"https://localhost:44304/"},
                AllowedScopes = new List<string> {
                    Constants.StandardScopes.OpenId,
                    Constants.StandardScopes.Profile,
                    Constants.StandardScopes.Email
                },
                AccessTokenType = AccessTokenType.Jwt
            }
        };
        }
    }
}