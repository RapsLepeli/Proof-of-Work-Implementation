using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoWImplementation
{
    internal class Program
    {
        static List<Block> blockChain;
        static void Main(string[] args)
        {
            
            CreateChain();

            Console.WriteLine(isChainValid());




            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
        public static void CreateChain()
        {
            blockChain = new List<Block>();

            blockChain.Add(new Block("Genesis Block", "0"));
            blockChain.Add(new Block("Second Block", blockChain[blockChain.Count - 1].hash));
            blockChain.Add(new Block("Third Block", blockChain[blockChain.Count - 1].hash));
            blockChain.Add(new Block("Fourth Block", blockChain[blockChain.Count - 1].hash));
            blockChain.Add(new Block("Fifth Block", blockChain[blockChain.Count - 1].hash));

        }
        public static bool isChainValid()
        {
            Block currentBlock;
            Block prevBlock;

            for (int i = 1; i < blockChain.Count; i++)
            {
                currentBlock = blockChain[i];
                prevBlock = blockChain[i-1];

                if (!currentBlock.hash.Equals(currentBlock.calculateHash()))
                {
                    Console.WriteLine("Hashes are not equal");
                    return false;
                }

                if (!prevBlock.hash.Equals(currentBlock.prevHash))
                {
                    Console.WriteLine("Previous block hashes are not equal");
                    return false;
                }

            }
            return true;
        }
    }

    public class Block
    {
        public string hash { get; set; }
        public string prevHash { get; set; }
        private string sdata;
        public string data
        {
            get
            {
                return sdata;
            }
            set
            {
                sdata = value;
            }
        }
        private string timeStamp;

        public Block(string data, string prevHash) 
        {
            this.data= data;
            this.prevHash = prevHash;
            timeStamp = DateTime.UtcNow.TimeOfDay.ToString();
            this.hash = calculateHash();
          
        }
        public string calculateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string sHash = "";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(prevHash + timeStamp));
                for (int i = 0; i < bytes.Length; i++)
                {
                    sHash += bytes[i].ToString("x2");
                }
                return sHash;
            }

        }
    }
}
