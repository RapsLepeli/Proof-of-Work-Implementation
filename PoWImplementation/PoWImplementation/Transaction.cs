using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoWImplementation
{
    public class Transaction
    {
        //should have its own has
        //dummy data

        //To be added
        public int amount { get; set; }
        public string payer { get; set; }//PublicKey
        public string payee { get; set; }//PublicKey


        public Transaction(int _amount, string _payer, string _payee)
        {
            amount = _amount;
            payer = _payer;
            payee = _payee;
        }
        public override string ToString()
        {
            return "Transaction Amount: " + amount + "\nPayer Public Key: " + payer + "\nPayee Public Key: " + payee;
        }
    }
}
