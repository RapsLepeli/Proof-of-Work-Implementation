using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoW
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public RSAParameters PayerPubKey { get; set; }
        public RSAParameters PayeePubKey { get; set; }
        private string TimeStamp;

        public Transaction(decimal amount, RSAParameters payerPubKey, RSAParameters payeePubKey)
        {
            Amount = amount;
            PayerPubKey = payerPubKey;
            PayeePubKey = payeePubKey;
            TimeStamp = DateTime.UtcNow.ToString();


        }

        private string DisplayPubKey(byte[] bytes)
        {

                string sHash = "";
            
                for (int i = 0; i < bytes.Length; i++)
                {
                    sHash += bytes[i].ToString("x2");
                }

                return sHash.Substring(0,80);
            
        }

        public override string ToString()
        {
            return "\tAmount: " + Amount + "\n\tPayer Public Key: " + DisplayPubKey(PayerPubKey.Modulus) + "\n\tPayee Public Key: " + DisplayPubKey(PayeePubKey.Modulus) + "\n\tTimeStamp: " + TimeStamp+"\n";
        }

    }
}
