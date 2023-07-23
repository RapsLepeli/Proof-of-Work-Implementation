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
           // Wallet wallet = new Wallet();
           // Console.Write("Pub: " + wallet.PublicKey + "\nPriv: " + wallet.PrivateKey);


            CreateChain();

            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }

        public static void CreateChain()
        {

            string MinerAddress = "Miner 1";

            string user1 = "Raps";
            string user2 = "Joe";
            int amount = 10;

            Transaction transaction1 = new Transaction(amount, user1, user2);
            blockChain.CreateTransaction(transaction1);
            Console.WriteLine("Transaction 1: " + user1 + " sends " + amount + " amount to "+ user2);

            amount = 200;
            Transaction transaction2 = new Transaction(amount, user2, user1);
            blockChain.CreateTransaction(transaction2);

            Console.WriteLine("Transaction 2: " + user2 + " sends " + amount + " amount to " + user1);

            amount = 10;
            Transaction transaction3 = new Transaction(10, user2, user1);
            blockChain.CreateTransaction(transaction3);
            Console.WriteLine("Transaction 3: " + user2 + " sends " + amount + " amount to " + user1);


            //Console.WriteLine("is the chain valid: " + blockChain.IsChainValid());

            //
            int difficulty = 1;

            Console.Write("\nEnter the difficulty for mining a block containig the above 3 transactions: ");
            difficulty = int.Parse(Console.ReadLine());


            Console.WriteLine();
            Console.WriteLine("----------------- Start mining first[not genesis] block(with 3 transactions)-----------------");

            blockChain.MineBlock(difficulty, MinerAddress);

            Console.WriteLine("Balance of the miner: " + blockChain.GetBalance(MinerAddress));

            amount = 5;
            Transaction transaction4 = new Transaction(amount, user1, user2);
            blockChain.CreateTransaction(transaction4);

            Console.Write("\nEnter the difficulty for mining a block containig the above 1 transaction: ");
            difficulty = int.Parse(Console.ReadLine());


            Console.WriteLine();
            Console.WriteLine("--------- Start mining block(with 1 transaction) ---------");
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
