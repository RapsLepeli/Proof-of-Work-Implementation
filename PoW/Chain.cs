using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoW
{
    public sealed class Chain
    {
        private static Chain cInstance = null;

        public static Chain Instance
        {
            get
            {
                if (cInstance == null)
                {
                    cInstance = new Chain();
                }
                return cInstance;
            }
        }
        public List<Block> chain;

        private Chain()
        {
            chain = new List<Block>();

            //Genesis Block
            
            List<Transaction> GenesisTransactions = new List<Transaction>();
            Block Genesis = new Block("0",GenesisTransactions);
            Genesis.Index = "0 Seconds, 432 Milliseconds!!!";
            chain.Add(Genesis);
        }

        public Block LastBlock
        {
            get
            {
                return chain[chain.Count - 1];
            }
        }
    }
}
