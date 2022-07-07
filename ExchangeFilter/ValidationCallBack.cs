using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ExchangeFilter;

public class ValidationCallBack
{
    private static bool RedirectionUrlValidationCallback(string redirectionUrl)
    {
        return true;
    }

    private static bool CertificateValidationCallBack(
        object sender,
        X509Certificate? certificate,
        X509Chain? chain,
        SslPolicyErrors sslPolicyErrors
    )
    {
        return true;
    }
}