using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PoW
{
    class Program
    {
        static Wallet User1 = new Wallet();
        static Wallet User2 = new Wallet();
        static Wallet Use3 = new Wallet();
        static Wallet User4 = new Wallet();


        //Network test
        static string name;
        static int listenPort = 49999;
        static int sendPort = 0;
        static string lastMessage = "";
        static string[] pendingMessagesForSession;



        static void WalletUi()
        {
            Console.WriteLine("-----------------Blockchain: Proof of Work Implementation -----------------");

            Thread listenerThread = null;

            listenPort = listenPort + 1;
            sendPort = listenPort + 1;

            Console.Write("Enter Wallet Name: ");
            name = Console.ReadLine();

           Console.WriteLine("App Uses ports and Sockets: " + name +" sends messages to " + sendPort +" and lsitens on port "+ listenPort);

           Console.WriteLine("Loading, This might take a few seconds...\n");

           string listeningOnPortsString = listenPort.ToString();

            listenerThread = new Thread(() => ListenForMessages(listenPort));
            listenerThread.Start();


            Block1TestData();

            while (true)
            {
                Console.Write(name + " sending on port: " + sendPort.ToString() + ">> Alert other clients: block mined(y/n): ");
                string message = Console.ReadLine();

                if (message == "n")
                {
                    stopListeningEvent.Set();
                    break;
                }

                // Broadcast the message to all clients
                SendMessage(name + ": " + message, IPAddress.Loopback, sendPort);
                lastMessage = message;
            }

        }

        static void Main()
        {
            WalletUi();


            Console.WriteLine("\n\tPress any key to exit...");
            Console.ReadKey();
        }


        //static void Main()
        //{
        //    Thread listenerThread = null;

        //    listenPort = listenPort+ 1;
        //    sendPort = listenPort +1;
        //    Console.Write("Enter Wallet Name: ");
        //    name = Console.ReadLine();

        //    Console.WriteLine(name+ " is listening on port: " + listenPort + "and sending messaages to port "+ sendPort);


        //    Console.WriteLine("Loading, This might take a few seconds...\n");

        //    string listeningOnPortsString = listenPort.ToString();

        //    listenerThread = new Thread(() => ListenForMessages(listenPort));
        //    listenerThread.Start();


        //    while (true)
        //    {
        //        Console.Write(name + " sending on port: " + sendPort.ToString() + ">> Enter a message or exit to stop the session: ");
        //        string message = Console.ReadLine();

        //        if (message == "exit")
        //        {
        //            stopListeningEvent.Set();
        //            break;
        //        }


        //        // Broadcast the message to all clients
        //        SendMessage(name + ": " + message, IPAddress.Loopback, sendPort);
        //        lastMessage = message;
        //    }


        //    Console.WriteLine("\n\tPress any key to exit...");
        //    Console.ReadKey();
        //}

        static ManualResetEvent stopListeningEvent = new ManualResetEvent(false);

        static void ListenForMessages(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread handlerThread = new Thread(HandleClient);
                handlerThread.Start(client);
            }
        }
        static void SendMessage(string message, IPAddress ipAddress, int port)
        {
            if (message != "")
            {
                if (!IsPortAvailable(port))
                {
                    TcpClient client = new TcpClient();
                    client.Connect(ipAddress, port);

                    NetworkStream stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    client.Close();
                }
                else
                {
                    //Console.WriteLine("Message not sent - No one listening");
                    TcpClient client = new TcpClient();
                    client.Connect(ipAddress, 50000);

                    NetworkStream stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    client.Close();
                }
            }
        }


        static void HandleClient(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (message != "")
            {
                Console.WriteLine("Message received: " + message + "\n\n");

                if (listenPort != 50000)
                    SendMessage(name + ": " + message, IPAddress.Loopback, sendPort);

            }

            client.Close();
        }

        static bool IsPortAvailable(int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect(IPAddress.Loopback, port);
                    return false;
                }
            }
            catch (SocketException)
            {
                return true;
            }
        }

        ////Origional Main
        //static void Main(string[] args)
        //{

        //    Chain BlockChain = Chain.Instance;

        //    Console.WriteLine("-----------------Blockchain: Proof of Work Implementation -----------------");

        //    Block1TestData();


        //    Console.Write("\n\tStart New Session(Y/N):>");
        //    char Opt = char.Parse(Console.ReadLine().ToUpper());
        //    if (Opt == 'Y')
        //    {
        //        Block2TestData();

        //        Console.Write("\n\tDisplay BlockChain Contents(Y/N):>");
        //        Opt = char.Parse(Console.ReadLine().ToUpper());
        //        if (Opt == 'Y')
        //        {
        //            //Display
        //            DisplayBlockChainContents(BlockChain);
        //        }
        //    }
        //    else if (Opt == 'N')
        //    {
        //        Console.Write("\n\tDisplay BlockChain Contents(Y/N):>");
        //        Opt = char.Parse(Console.ReadLine().ToUpper());
        //        if (Opt == 'Y')
        //        {
        //            //Display
        //            DisplayBlockChainContents(BlockChain);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("\tInvalid Input...");
        //        Console.Write("\n\tDisplay BlockChain Contents(Y/N):>");
        //        Opt = char.Parse(Console.ReadLine().ToUpper());
        //        if (Opt == 'Y')
        //        {
        //            //Display
        //            DisplayBlockChainContents(BlockChain);
        //        }
        //    }

        //    Console.WriteLine("\n\tPress any key to exit...");
        //    Console.ReadKey();
        //}
        static void Block1TestData()
        {
            //Create Dummy Transactions
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t----------------- Session 1 Start(p2pTest-> new p2p)-----------------\n");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tTransaction 1\n\tUser 3 Sends 32 Money to User 1");
            Transaction t1 = new Transaction(32, Use3.PublicKey
                , User1.PublicKey);
            Use3.SpendMoney(32); User1.ReceiveMoney(32);

            Console.WriteLine("\t--Sign Transaction 1--");
            //Sign T1 Transactions
            var t1Signiture = User1.SignTransaction(t1, User1.PrivateKey);
            Console.WriteLine("\tTransaction 1 Signiture: " + t1Signiture);
            //Verrify
            bool t1Ver = User1.VerifySigniture(t1, t1Signiture, Use3.PublicKey);
            Console.WriteLine("\tTransaction 1 Legit? " + t1Ver);

            Console.WriteLine("\n\tTransaction 2\n\tUser 4 Sends 22 Money to User 2");
            Transaction t2 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User4.SpendMoney(22); User2.ReceiveMoney(22);
            Console.WriteLine("\t--Sign Transaction 2--");
            //Sign T2 Transactions
            var t2Signiture = User4.SignTransaction(t2, User4.PrivateKey);
            Console.WriteLine("\tTransaction 2 Signiture: " + t2Signiture);
            //Verify
            bool t2Ver = User4.VerifySigniture(t2, t2Signiture, User4.PublicKey);
            Console.WriteLine("\tTransaction 2 Legit? " + t2Ver);


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            Console.WriteLine("\tTransactions Added to Pending List for current Session");
            User1.AddTransaction(t1);
            User4.AddTransaction(t2);

            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\tMine Created Block\n\t-----------------");
            int Difficulty = 1;
            Console.Write("\tEnter difficulty for mining above two transactions:> ");
            try
            {
                Difficulty = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed
                    ;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("\n\t"+ e.Message + " Default Difficulty of 1 has been used\t\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
           

            string message = User1.MineBlock(blockToBeAdded, Difficulty);
            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tIs Block Valid: " + ver);
            Console.WriteLine("\tMined Block Added");
            User1.AddMinedBlock(blockToBeAdded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t---------------------------------- Session 1 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void Block2TestData()
        {
            //Create Dummy Transactions
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\t---------------------------------- Session 2 Start---------------------------------------------------\n");


            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\tTransaction 1\n\tUser 1 Sends 11 Money to User 4");
            Transaction t1 = new Transaction(11, User1.PublicKey, User4.PublicKey);
            User1.SpendMoney(11); User4.ReceiveMoney(11);

            Console.WriteLine("\t--Sign Transaction 1--");
            //Sign T1 Transactions
            var t1Signiture = User1.SignTransaction(t1, User1.PrivateKey);
            Console.WriteLine("\tTransaction 1 Signiture: " + t1Signiture);
            //Verify
            bool t1Ver = User1.VerifySigniture(t1, t1Signiture, Use3.PublicKey);
            Console.WriteLine("\tTransaction 1 Legit? " + t1Ver);


            Console.WriteLine("\n\tTransaction 2\n\tUser 2 Sends 14 Money to User 3");
            Transaction t2 = new Transaction(14, User2.PublicKey, Use3.PublicKey);
            User2.SpendMoney(14); Use3.ReceiveMoney(14);
            Console.WriteLine("\t--Sign Transaction 2--");
            //Sign T2 Transactions
            var t2Signiture = User2.SignTransaction(t2, User2.PrivateKey);
            Console.WriteLine("\tTransaction 2 Signiture: " + t2Signiture);
            //Verify
            bool t2Ver = User4.VerifySigniture(t2, t2Signiture, User2.PublicKey);
            Console.WriteLine("\tTransaction 2 Legit? " + t2Ver);


            Console.WriteLine("\n\tTransaction 3\n\tUser 4 Sends 33 Money to User 3");
            Transaction t3 = new Transaction(14, User4.PublicKey, Use3.PublicKey);
            User4.SpendMoney(33); Use3.ReceiveMoney(33);
            Console.WriteLine("\t--Sign Transaction 3--");
            //Sign T3 Transactions
            var t3Signiture = User2.SignTransaction(t2, User4.PrivateKey);
            Console.WriteLine("\tTransaction 3 Signiture: " + t3Signiture);
            //Verify
            bool t3Ver = User4.VerifySigniture(t2, t3Signiture, Use3.PublicKey);
            Console.WriteLine("\tTransaction 3 Legit? " + t3Ver);



            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            Console.WriteLine("\tTransactions Added to Pending List for current Session");
            User1.AddTransaction(t1);
            User4.AddTransaction(t2);
            Use3.AddTransaction(t3);

            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tMine Created Block\n\t-----------------");
            int Difficulty = 1;
            Console.Write("\tEnter difficulty for mining above three transactions:> ");
            try
            {
                Difficulty = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed
                    ;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("\n\t" + e.Message + " Default Difficulty of 1 has been used\t\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            string message = User1.MineBlock(blockToBeAdded, Difficulty);
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tIs Block Valid: " + ver);
            Console.WriteLine("\tMined Block Added");
            User1.AddMinedBlock(blockToBeAdded);


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\t---------------------------------- Session 2 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;
        }



        //Display
        static void DisplayBlockChainContents(Chain BlockChain)
        {
            Console.Clear();
            Console.WriteLine("-----------------Blockchain: Proof of Work Implementation -----------------");

            Console.WriteLine("\n\t--------------------- Start Blockchain -----------------");
            foreach (var block in BlockChain.chain)
            {
                Console.WriteLine("\t-------------- Start Block -------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(block.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t----------- Start Block Transactions -----------");

                Console.ForegroundColor = ConsoleColor.Yellow;
                if (block.transactions.Count <= 0)
                {
                    Console.WriteLine("\n\t\tNo Transactions Available\n");
                }
                else
                {
                    foreach (var transaction in block.transactions)
                    {
                            Console.WriteLine(transaction.ToString());
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t----------- End Block Transactions -------------");
                Console.WriteLine("\n\t-------------- End Block -----------------------------");


            }
            Console.WriteLine("\n\t----------------- End Blockchain -----------------------------\n");

        }
    }
}
