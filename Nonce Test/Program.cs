using System.Security.Cryptography;
using System.Text;

namespace Nonce_Test
{



    class Program
    {

        static void Main()
        {
            string sDifficulty = "0";

            string sHash = "";
            string prevHash = "0";
            string timeStamp = DateTime.UtcNow.TimeOfDay.ToString();

            int nonce = 0;

            while (true)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {

                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(prevHash + nonce + timeStamp));//Remove timestamp for testing

                    foreach (byte b in bytes)
                    {
                        sHash += $"{b:X2}";
                    }
                    Console.WriteLine("Hash: " + sHash);
                    Console.WriteLine();
                }
               
                if((sHash.Substring(0, sDifficulty.Length).Equals(sDifficulty))){
                    break;
                }
                 nonce++;
            } 
            Console.WriteLine("--------------");
            Console.WriteLine("Block Mined");
            Console.WriteLine("Nonce: " + (nonce - 1));
            Console.WriteLine("Correct Hash: " + sHash);
        }
    }

    public class Example
    {
        static string ComputeSHA256(string s)
        {
            string hash = String.Empty;

            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }


    }





}