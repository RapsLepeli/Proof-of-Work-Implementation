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

            doesBlockExist();

           // Console.WriteLine(isChainValid());



            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }

        public static void CreateChain()
        {
            //create a class fro blockchain
            blockChain = new List<Block>();

            blockChain.Add(new Block(new List<Transaction>() { new Transaction(1,"First Transaction in genesis block"), new Transaction(3, "third Transaction in genesis") }, "0"));
            Console.WriteLine(blockChain[0]);

            blockChain.Add(new Block(new List<Transaction>() { new Transaction(1, "First Transaction in block2 block"), new Transaction(2, "Second Transaction in block2") }, blockChain[blockChain.Count -1].hash));
            Console.WriteLine(blockChain[1]);

            blockChain.Add(new Block(new List<Transaction>() { new Transaction(5, "Firfth Transaction in block3 block"), new Transaction(6, "Sixth Transaction in block3") }, blockChain[blockChain.Count - 1].hash));
            Console.WriteLine(blockChain[2]);

        }
        public static void doesBlockExist()
        {
            Console.Write("Input hash to check: ");
            string blockhash = Console.ReadLine();

            Block currentBlock = blockChain.FirstOrDefault(b => b.hash == blockhash);

            if (currentBlock != null)
            {
                Console.WriteLine("Block Exists....");

            }
            else
            {
                Console.WriteLine("Block Does not exist");
            }
           
            
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

}
