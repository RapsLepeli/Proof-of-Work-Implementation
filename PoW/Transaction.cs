using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
        Random random;

        public Transaction(decimal amount, RSAParameters payerPubKey, RSAParameters payeePubKey)
        {
            Amount = amount;
            PayerPubKey = payerPubKey;
            PayeePubKey = payeePubKey;
            TimeStamp = DateTime.UtcNow.ToString();
            random = new Random();

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
        public string CalculatePubKey()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                int randomiser = random.Next(100);
                string sHash = "";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("transactions hash instead of the transactions themselves" + randomiser)); 
                                                                                                                                                                        //preparing the hash and returning it
                for (int i = 0; i < bytes.Length; i++)
                {
                    sHash += bytes[i].ToString("x2");
                }
                return sHash;
            }
        }

        public override string ToString()
        {
            return "\tAmount: " + Amount + "\n\tPayer Public Key: " + CalculatePubKey() + "\n\tPayee Public Key: " + CalculatePubKey() + "\n\tTimeStamp: " + TimeStamp+"\n";
        }

    }
}
