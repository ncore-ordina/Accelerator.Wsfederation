using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLM.SOFA.Security
{

    [Serializable]
    public class SecurityServiceSettings
    {
        public SecurityServiceSettings()
        {
            this.Port = 443;
            this.SigningCertificate = "LocalSTS.pfx";
            this.SigningCertificatePassword = "LocalSTS";
            this.IssuerName = "SOFASTS";
            this.PassiveEndpoint = "http://localhost:80/SOFAServicesHost/SecurityService.svc/Issue/";
            this.ActiveEndpoint = "http://localhost:80/SOFAServicesHost/SecurityService.svc/wsTrust/";
            this.FederationMetadataEndpoint = "http://localhost:80/SOFAServicesHost/FederationMetadata/2007-06/FederationMetadata.xml";
        }

        public int Port { get; set; }
        public string SigningCertificate { get; set; }
        public string SigningCertificatePassword { get; set; }
        public string IssuerName { get; set; }
        public string PassiveEndpoint { get; set; }
        public string ActiveEndpoint { get; set; }
        public string FederationMetadataEndpoint { get; set; }
        public string BazmanConnectionString { get; set; }
    }
}
