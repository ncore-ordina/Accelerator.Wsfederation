using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace VLM.SOFA.Security.Service.Implementation
{
    public class SOFAWSTrustServiceHostFactory: ServiceHostFactory
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomWSTrustServiceHostFactory"/> class.
        /// </summary>
        public SOFAWSTrustServiceHostFactory(): base() { }

        /// <summary>
        /// Creates and configures a <see cref="WSTrustServiceHost"/> with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host (ignored).</param>
        /// <param name="baseAddresses">The <see cref="T:Uri"/> array that contains the base addresses for the service.</param>
        /// <returns>A <see cref="WSTrustServiceHost"/> with a specific base address.</returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var config = new SOFASecurityTokenServiceConfiguration();
            var host = new WSTrustServiceHost(config, baseAddresses);
            var serviceBehavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            serviceBehavior.AddressFilterMode = AddressFilterMode.Any;
            return host;
        }

        /// <summary>
        /// Creates and configures a <see cref="WSTrustServiceHost"/> with a specific base address.
        /// </summary>
        /// <param name="constructorString">The constructor string (ignored).</param>
        /// <param name="baseAddresses">The <see cref="T:Uri"/> array that contains the base addresses for the service.</param>
        /// <returns></returns>
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var config = new SOFASecurityTokenServiceConfiguration();
            var host = new WSTrustServiceHost(config, baseAddresses);
            var serviceBehavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            serviceBehavior.AddressFilterMode = AddressFilterMode.Any;
            return host;
        }

    }
}
