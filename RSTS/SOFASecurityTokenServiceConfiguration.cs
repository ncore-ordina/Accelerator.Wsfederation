using System;
using System.Collections.Generic;
using System.IdentityModel.Configuration;
using System.IdentityModel.Metadata;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace VLM.SOFA.Security.Service.Implementation
{
    public class SOFASecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {
        private SecurityServiceSettings _settings;

        public SOFASecurityTokenServiceConfiguration()
        {
            _settings = new SecurityServiceSettings();

            this.TokenIssuerName = _settings.IssuerName;
            string signingCertificatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.SigningCertificate);
            X509Certificate2 signignCert = new X509Certificate2(signingCertificatePath, _settings.SigningCertificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            this.SigningCredentials = new X509SigningCredentials(signignCert);
            this.ServiceCertificate = signignCert;

            this.SecurityTokenService = typeof(SOFASecurityTokenService);
        }

        public XElement GetFederationMetadata()
        {
            // hostname
            var passiveEndpoint = new EndpointReference(_settings.PassiveEndpoint);
            var activeEndpoint = new EndpointReference(_settings.ActiveEndpoint);

            // metadata document 
            EntityDescriptor entity = new EntityDescriptor(new EntityId(_settings.IssuerName));
            SecurityTokenServiceDescriptor sts = new SecurityTokenServiceDescriptor();
            entity.RoleDescriptors.Add(sts);

            // signing key
            KeyDescriptor signingKey = new KeyDescriptor(this.SigningCredentials.SigningKeyIdentifier);
            signingKey.Use = KeyType.Signing;
            sts.Keys.Add(signingKey);

            // claim types
            sts.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Email, "Email Address", "User email address"));
            sts.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Surname, "Surname", "User last name"));
            sts.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Name, "Name", "User name"));
            sts.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Role, "Role", "Roles user are in"));


            // passive federation endpoint
            sts.PassiveRequestorEndpoints.Add(passiveEndpoint);

            // supported protocols

            //Inaccessable due to protection level
            //sts.ProtocolsSupported.Add(new Uri(WSFederationConstants.Namespace));
            sts.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));

            // add passive STS endpoint
            sts.SecurityTokenServiceEndpoints.Add(activeEndpoint);

            // metadata signing
            entity.SigningCredentials = this.SigningCredentials;

            // serialize 
            var serializer = new MetadataSerializer();
            XElement federationMetadata = null;

            using (var stream = new MemoryStream())
            {
                serializer.WriteMetadata(stream, entity);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                var readerSettings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Prohibit, // prohibit DTD processing
                    XmlResolver = null, // disallow opening any external resources
                    // no need to do anything to limit the size of the input, given the input is crafted internally and it is of small size
                };

                var xmlReader = XmlTextReader.Create(stream, readerSettings);
                federationMetadata = XElement.Load(xmlReader);
            }

            return federationMetadata;
        }
    }
}
