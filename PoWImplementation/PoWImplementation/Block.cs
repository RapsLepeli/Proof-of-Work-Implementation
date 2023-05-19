using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoWImplementation
{
    public class Block
    {
        //hash of current block
        public string hash { get; set; }
        //hash of prev block
        public string prevHash { get; set; }
        //When block was created
        private string timeStamp;
        //list of transactions
        public List<Transaction> transactions { get; set; }

        /// 
        public int nonce;
        /// 

        public Block(string prevHash, List<Transaction> transactions)
        {
            this.transactions = transactions;
            this.prevHash = prevHash;
            timeStamp = DateTime.UtcNow.TimeOfDay.ToString();
            this.hash = calculateHash();

        }

        //Mine Block(Testing the blockchain if it works forst)

        public void MineBlock(int poWDiffictlty)
        {
            //pow difficulty determines how many zero's the hash starts with
            string hashvalidation = new string('0', poWDiffictlty);

            //whenever we do not have the spercified number of zeros, keep recaluating the hash(this depends on the 4
            //props for now: prevhas,timestamp,transactions and nonoce), but the real deal is the nonce it keeps on changing(+ time I guess)
            while (hash.Substring(0,poWDiffictlty) != hashvalidation)
            {
                nonce++;
                hash = calculateHash();
                Console.WriteLine("Invalid hash: hash needed starts with " + poWDiffictlty + " zero's :" +  hash);
            }
            Console.WriteLine("Block with hash={0} successfully mined!!!",hash);
            
        }

        public string calculateHash()
        {
            //using the sha256 hash algo
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string sHash = "";
                //we hash using the 4 properties
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(prevHash + timeStamp + transactions + nonce));// transactions hash instead of the transactions themselves

                //preparing the hash and returning it
                for (int i = 0; i < bytes.Length; i++)
                {
                    sHash += bytes[i].ToString("x2");
                }
                return sHash;
            }

        }
        //public override string ToString()
        //{
        //    string BlockOutput = "";

        //    BlockOutput += "Previous Hash: " + prevHash + "\nBlock Hash: " + hash + "\n";

        //    return BlockOutput ;
        //}
    }
}
