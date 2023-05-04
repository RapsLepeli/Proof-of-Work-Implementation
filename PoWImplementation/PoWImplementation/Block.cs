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
        public string hash { get; set; }
        public string prevHash { get; set; }
        
        private string timeStamp;
        //Single Transaction For Now....
        Transaction transaction;

        /// 
        public int nonce = 1;
        /// 

        public Block(string _prevHash, Transaction _transaction)
        {
            transaction = _transaction;
            this.prevHash = _prevHash;
            timeStamp = DateTime.UtcNow.TimeOfDay.ToString();
            this.hash = calculateHash();

        }
        public string calculateHash()
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                string sHash = "";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(prevHash + timeStamp));// add nonce after test + transactions hash

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
