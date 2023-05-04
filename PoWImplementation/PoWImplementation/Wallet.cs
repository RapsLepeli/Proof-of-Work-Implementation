using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoWImplementation
{
    public class Wallet//does all the hasshing, and provides keys
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        public string PrivateKey { get { return _privateKey.ToString(); } }
        private RSAParameters _publicKey;
        public string PublicKey { get { return GetPublicKey(); } }
    

        public Wallet()
        {
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }

        private string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }
        public void sendMoney(int amount, string payeePublickKey)
        {
            //
        }
        
    }
}
