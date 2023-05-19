using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoWImplementation
{
    public sealed class Chain
    {
        //simplest / change if needs be
        private static Chain cInstanance = null;
        public static Chain Instance
        {
            get
            {
                if (cInstanance == null)
                {
                    cInstanance = new Chain();
                }
                return cInstanance;
            }
        }
        List<Block> chain;

        private Chain()
        {
            //add first block
            chain = new List<Block>();
            chain.Add(CreateGenesisBlock());
        }
        //making the chain a singleton above

        //below(Properties and methods)
        private readonly decimal _miningReward;

        private List<Transaction> _pendingTransactions = new List<Transaction>();

        //create first block
        private Block CreateGenesisBlock()
        {
            
            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(new Transaction(0, "", ""));
            return new Block("0", transactions); 
        }
        

        //get last block
        public Block LastBlock
        {
            get
            {
                return chain[chain.Count - 1];
            }
        }

        public void MineBlock(int poWDifficulty, string minerAdress)
        {
            Transaction minerRewardTransaction = new Transaction(_miningReward, null, minerAdress);
            _pendingTransactions.Add(minerRewardTransaction);

            Block blockToBeAdded = new Block(LastBlock.hash, _pendingTransactions);
            blockToBeAdded.MineBlock(poWDifficulty);

           // blockToBeAdded.prevHash = LastBlock.hash;

            //add block after mined
            chain.Add(blockToBeAdded);
           
            }
        public void CreateTransaction(Transaction transaction)
        {
            _pendingTransactions.Add(transaction);
        }

        public decimal GetBalance(string userAdress)
        {
            decimal balance = 0;

            foreach (Block block in chain)
            {
                foreach (Transaction transaction in block.transactions)
                {
                    if (transaction.payee == userAdress)
                    {
                        balance -= transaction.amount;
                    }
                    if (transaction.payer == userAdress)
                    {
                        balance += transaction.amount;
                    }
                }
            }
            return balance;
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < chain.Count; i++)
            {
                Block prevBlock = chain[i - 1];
                Block curBlock = chain[i];
                if (curBlock.hash != curBlock.calculateHash())
                {
                    return false;
                }
                if (curBlock.prevHash != prevBlock.hash)
                {
                    return false;
                }
            }
            return true;
        }

        public void PrintChain()
        {
            Console.WriteLine("----------------- Start Blockchain -----------------");
            foreach (Block block in chain)
            {
                Console.WriteLine();
                Console.WriteLine("------ Start Block ------");
                Console.WriteLine("Hash: {0}", block.hash);
                Console.WriteLine("Previous Hash: {0}", block.prevHash);
                Console.WriteLine("--- Start Transactions ---");
                foreach (Transaction transaction in block.transactions)
                {
                    Console.WriteLine("From: {0} To {1} Amount {2}", transaction.payer, transaction.payee, transaction.amount);
                }
                Console.WriteLine("--- End Transactions ---");
                Console.WriteLine("------ End Block ------");
            }
            Console.WriteLine("----------------- End Blockchain -----------------");
        }
    }
    
}
