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
        static Chain blockChain = Chain.Instance;

        static void Main(string[] args)
        {

            CreateChain();

            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }

        public static void CreateChain()
        {

            string MinerAddress = "Miner 1";
            string user1 = "Raps";
            string user2 = "Joe";

            Transaction transaction1 = new Transaction(10, user1, user2);
            blockChain.CreateTransaction(transaction1);

            Transaction transaction2 = new Transaction(200, user2, user1);
            blockChain.CreateTransaction(transaction2);

            Transaction transaction3 = new Transaction(10, user2, user1);
            blockChain.CreateTransaction(transaction3);

            Console.WriteLine("is the chain valid: " + blockChain.IsChainValid());

            //
            int difficulty = 1;


            Console.WriteLine();
            Console.WriteLine("----------------- Start Blockchain -----------------");

            blockChain.MineBlock(difficulty, MinerAddress);

            Console.WriteLine("Balance of the miner: " + blockChain.GetBalance(MinerAddress));

            Transaction transaction4 = new Transaction(5, user1, user2);
            blockChain.CreateTransaction(transaction4);


            Console.WriteLine();
            Console.WriteLine("--------- Start mining ---------");
            blockChain.MineBlock(difficulty, MinerAddress);

            Console.WriteLine("Balance of the miner: " + blockChain.GetBalance(MinerAddress));
            Console.WriteLine();
           
            blockChain.PrintChain();
        }

        //public static void doesBlockExist()
        //{
        //    Console.Write("Input hash to check: ");
        //    string blockhash = Console.ReadLine();

        //    Block currentBlock = blockChain.FirstOrDefault(b => b.hash == blockhash);

        //    if (currentBlock != null)
        //    {
        //        Console.WriteLine("Block Exists....");

        //    }
        //    else
        //    {
        //        Console.WriteLine("Block Does not exist");
        //    }


        //}

        //public static bool isChainValid()
        //{
        //    Block currentBlock;
        //    Block prevBlock;

        //    for (int i = 1; i < blockChain.Count; i++)
        //    {
        //        currentBlock = blockChain[i];
        //        prevBlock = blockChain[i-1];

        //        if (!currentBlock.hash.Equals(currentBlock.calculateHash()))
        //        {
        //            Console.WriteLine("Hashes are not equal");
        //            return false;
        //        }

        //        if (!prevBlock.hash.Equals(currentBlock.prevHash))
        //        {
        //            Console.WriteLine("Previous block hashes are not equal");
        //            return false;
        //        }

        //    }
        //    return true;
        //}
    }

}
