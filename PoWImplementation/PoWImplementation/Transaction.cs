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
        public decimal amount { get; }
        public string payer { get; }//PublicKey{from}
        public string payee { get; }//PublicKey{to}
        private string timeStamp;


        public Transaction(decimal amount, string payer, string payee)
        {
            this.amount = amount;
            this.payer = payer;
            this.payee = payee;
            timeStamp = DateTime.UtcNow.TimeOfDay.ToString();
        }
        public override string ToString()
        {
            return "Transaction Amount: " + amount + "\nPayer Public Key: " + payer + "\nPayee Public Key: " + payee + "\nTime Stamp: " + timeStamp;
        }
    }
}
