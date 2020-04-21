using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace VLM.SOFA.Security.Service.Implementation
{
    public class SOFASecurityTokenService : SecurityTokenService
    {
   

        public SOFASecurityTokenService(SecurityTokenServiceConfiguration configuration)
            : base(configuration)
        {
        }

        protected override Scope GetScope(ClaimsPrincipal principal, RequestSecurityToken request)
        {
            this.ValidateAppliesTo(request.AppliesTo);
            Scope scope = new Scope(request.AppliesTo.Uri.OriginalString, SecurityTokenServiceConfiguration.SigningCredentials);

            scope.TokenEncryptionRequired = false;
            scope.SymmetricKeyEncryptionRequired = false;

            if (string.IsNullOrEmpty(request.ReplyTo))
            {
                scope.ReplyToAddress = scope.AppliesToAddress;
            }
            else
            {
                scope.ReplyToAddress = request.ReplyTo;
            }

            return scope;
        }

        protected override ClaimsIdentity GetOutputClaimsIdentity(ClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
        {
            if (principal == null)
            {
                throw new InvalidRequestException("The caller's principal is null.");
            }

            var incomingIdentity = principal.Identity as ClaimsIdentity;
            var name=incomingIdentity.Name;

            var shortname = RemoveDomainPrefix(name);
         
            ClaimsIdentity outputIdentity = new ClaimsIdentity();
            outputIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
            outputIdentity.AddClaim(new Claim(ClaimTypes.Country, "Belgium"));
            outputIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, "23/07/1982"));



            return outputIdentity;
        }

        private string RemoveDomainPrefix(string name)
        {
            var backslash = name.LastIndexOf('\\');
            if (backslash <= 0)
                return name;

            return name.Substring(backslash+1);
        }

   
        private void ValidateAppliesTo(EndpointReference appliesTo)
        {
            if (appliesTo == null)
            {
                throw new InvalidRequestException("The AppliesTo is null.");
            }
        }
    }
}
