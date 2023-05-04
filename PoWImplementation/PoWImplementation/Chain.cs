using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoWImplementation
{
    public sealed class Chain
    {
        //simplest/ change if needs be
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
            chain = new List<Block>();

            chain.Add(new Block("0", new Transaction(12, "Genesis", "Satoshi")));

        }

        public Block LastBlock
        {
            get
            {
                return chain[chain.Count - 1];
            }
        }

        //do it later
        //public void mineBlock(int nonce)
        //{
        //    int solution = 1;

        //    while (true)
        //    {



        //        solution = 1;
        //    }

        //}
        public void addBlock(Transaction transaction, string senderPublickey, string signiture) 
        {
            //before adding, verify id sender public key and signiture are correct

            //to do
            Block newBlock = new Block(LastBlock.hash, transaction);
            //mine th eblock
            //mineBlock(newBlock.nonce);
            //add the block
            chain.Add(newBlock);

        }


    }
}
