using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;
using IdentityServer3.Core;

namespace STS
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser> {
            new InMemoryUser {
                Subject = "1",
                Username = "BaWu",
                Password = "Password123!",
                Claims = new List<Claim> {
                    new Claim(Constants.ClaimTypes.GivenName, "Bart"),
                    new Claim(Constants.ClaimTypes.FamilyName, "Wullems"),
                    new Claim(Constants.ClaimTypes.Email, "info@bawu.be"),
                    new Claim(Constants.ClaimTypes.Role, "Admin")
                }
            }
        };
        }
    }
}