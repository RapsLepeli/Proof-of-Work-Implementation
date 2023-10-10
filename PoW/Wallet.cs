using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoW
{
    public class Wallet
    {
        public static RSACryptoServiceProvider KeyProvider = new RSACryptoServiceProvider();
        public static List<Transaction> PendingTransactions = new List<Transaction>();
        //Priv Key
        public RSAParameters PrivateKey;
        //Pub Key
        public RSAParameters PublicKey;

        private int nonce;


        public decimal Balance { get; private set; }
        public Wallet()
        {
            PrivateKey = KeyProvider.ExportParameters(true);
            PublicKey = KeyProvider.ExportParameters(false);
            Balance = 43;
            
        }

        public void ReceiveMoney(decimal amount)
        {
            Balance += amount;
        }
        public void SpendMoney(decimal amount)
        {
            if (amount < Balance)
            {
                Balance -= amount;
            }
        }
        public string SignTransaction(Transaction transaction, RSAParameters PrivateKey)
        {
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(PrivateKey);

                var dataToSign = Encoding.UTF8.GetBytes(transaction.ToString());
                var signiture = rsa.SignData(dataToSign, HashAlgorithmName.MD5,RSASignaturePadding.Pkcs1);

                return Convert.ToBase64String(signiture);
            }
        }
        public bool VerifySigniture(Transaction transaction,string signiture, RSAParameters PayerPubKey)
        {
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(PayerPubKey);

                var dataToVerify = Encoding.UTF8.GetBytes(transaction.ToString());
                var signitureData = Convert.FromBase64String(signiture);

                return rsa.VerifyData(dataToVerify, signitureData, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }
        public Block CreateBlock()
        {
            Block blockToBeAdded = new Block(Chain.Instance.LastBlock.Hash, PendingTransactions);
            blockToBeAdded.Hash = CalculateBlockHash(blockToBeAdded);

            return blockToBeAdded;
        }

        public string CalculateBlockHash(Block blockToBeMined)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string sHash = "";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(blockToBeMined.PrevHash + blockToBeMined.TimeStamp + blockToBeMined.transactions + nonce));// transactions hash instead of the transactions themselves
                                                                                                                                                                        //preparing the hash and returning it
                for (int i = 0; i < bytes.Length; i++)
                {
                    sHash += bytes[i].ToString("x2");
                }
                return sHash;
            }
        }
        public void MineBlock(Block blockToBeMined,int poWDifficulty)
        {
            string hashValidation = new string('0', poWDifficulty);
            string Message = "";
            Console.ForegroundColor = ConsoleColor.Red;
            Stopwatch stopWatch = new Stopwatch();
            
            stopWatch.Start();
            while (blockToBeMined.Hash.Substring(0,poWDifficulty) != hashValidation)
            {
                nonce++;
                blockToBeMined.Hash = CalculateBlockHash(blockToBeMined);
               Console.WriteLine("\tInvalid hash: hash needed starts with " + poWDifficulty + " zero's :" + blockToBeMined.Hash+"");
            }
            stopWatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tBlock with hash= "+ blockToBeMined.Hash + " successfully mined!!!");
            Console.WriteLine("\n\tTime Elapsed to mine block: " + (stopWatch.ElapsedMilliseconds)/ 1000 + " seconds!!!");
            blockToBeMined.Index = (int)(stopWatch.ElapsedMilliseconds) / 1000;
            Console.ForegroundColor = ConsoleColor.White;
     
        }
        public bool VerifyMinedBlock(Block blockToBeVerified)
        {
            if (blockToBeVerified.Hash != CalculateBlockHash(blockToBeVerified))
            {
                return false;
            }
            return true;
        }
        public void AddMinedBlock(Block blockToBeAdded)
        {
            Chain.Instance.chain.Add(blockToBeAdded);
            PendingTransactions = new List<Transaction>();
        }

    }
}
