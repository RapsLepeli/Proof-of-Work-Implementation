using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoW
{
    public class Block
    {
        public string Index { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
        public string TimeStamp { get; private set; }

        public List<Transaction> transactions { get; set; }

        public Block(string prevBlockHash, List<Transaction> transactions)
        {
            this.transactions = transactions;
            this.PrevHash = prevBlockHash;
            TimeStamp = DateTime.UtcNow.ToString();
            Hash = CalculateBlockHash();
            Index = "";

        }
        public string CalculateBlockHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string sHash = "";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(PrevHash + TimeStamp + transactions));// transactions hash instead of the transactions themselves
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
            return "\tHash: " + Hash + "\n\tPrev Hash: " + PrevHash + "\n\tTimeStamp: "+TimeStamp+"\n\tMining Time: " + Index;
        }
    }
}
