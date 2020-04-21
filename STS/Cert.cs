using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace STS
{
    public class Cert
    {
        //Certificate from https://github.com/IdentityServer/IdentityServer3.Samples/tree/master/source/Certificates
        public static X509Certificate2 Load()
        {
            return new X509Certificate2(
               Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\idsrv3test.pfx"), "idsrv3test");
        }
    }
}