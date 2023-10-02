using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace PoW
{
    class Program
    {
        static Wallet User1 = new Wallet();
        static Wallet User2 = new Wallet();
        static Wallet Use3 = new Wallet();
        static Wallet User4 = new Wallet();


        static IPHostEntry iPHost;
        static IPAddress ipAddr;
        static IPEndPoint localEndPoint;
        static Socket serverSocket;
        static List<Socket> clientSockets = new List<Socket>();

        //static void Main(string[] args)
        //{
        //    // Host IP Address
        //    iPHost = Dns.GetHostEntry(Dns.GetHostName());
        //    ipAddr = iPHost.AddressList[0];
        //    Console.WriteLine("Host IP Address: " + ipAddr.ToString());

        //    // Creating an endpoint (Host + Port)
        //    localEndPoint = new IPEndPoint(ipAddr, 11111);
        //    Console.WriteLine("Communication EndPoint: " + localEndPoint);

        //    Console.Write("\n1. System_Tester: \n2. Wallet:\nChoose (1/2): ");
        //    int choice = int.Parse(Console.ReadLine());

        //    if (choice == 1)
        //    {
        //        ExecuteServer();
        //    }
        //    else
        //    {
        //        ExecuteClient();
        //    }

        //    Console.WriteLine("Press any key to exit....");
        //    Console.ReadKey();
        //}

        static void ExecuteServer()
        {
            try
            {
                serverSocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(2);

                Console.WriteLine("System Tester is waiting for connections from Wallets...");

                while (true)
                {
                    Socket clientSocket = serverSocket.Accept();
                    clientSockets.Add(clientSocket);

                    Thread clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void HandleClient(Socket clientSocket)
        {
            try
            {
                Console.WriteLine("Wallet connected: " + clientSocket.RemoteEndPoint.ToString());

                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int numBytes = clientSocket.Receive(bytes);
                    string data = Encoding.ASCII.GetString(bytes, 0, numBytes);

                    Console.WriteLine("Received from client: " + data);

                    // Broadcast the message to all connected clients except the sender
                    foreach (var socket in clientSockets)
                    {
                        if (socket != clientSocket)
                        {
                            byte[] messageBytes = Encoding.ASCII.GetBytes(data);
                            string Message = Encoding.Default.GetString(messageBytes);
                            if (Message.Contains("joe"))
                            {
                                break;
                            }
                            else
                            {
                                messageBytes = Encoding.Default.GetBytes(Message);
                                socket.Send(messageBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Client disconnected: " + clientSocket.RemoteEndPoint.ToString());
                clientSockets.Remove(clientSocket);
                clientSocket.Close();
            }
        }

        static void ExecuteClient()
        {
            try
            {
                Socket clientSocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(localEndPoint);

                Console.WriteLine("Connected to System: " + clientSocket.RemoteEndPoint.ToString());

                Thread receiveThread = new Thread(() => ReceiveMessages(clientSocket));
                receiveThread.Start();

                while (true)
                {
                    Console.Write("Enter Message to Send: ");
                    string messageToSend = Console.ReadLine();

                    byte[] messageSent = Encoding.ASCII.GetBytes(messageToSend);
                    clientSocket.Send(messageSent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ReceiveMessages(Socket clientSocket)
        {
            try
            {
                while (true)
                {
                    byte[] messageReceived = new byte[1024];
                    int bytesRead = clientSocket.Receive(messageReceived);
                    string receivedMessage = Encoding.ASCII.GetString(messageReceived, 0, bytesRead);
                    Console.WriteLine("Message from System: " + receivedMessage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Disconnected from System.");
                clientSocket.Close();
                Environment.Exit(0);
            }
        }
    
    //Origional Main
         static void Main(string[] args)
         {
            Chain BlockChain = Chain.Instance;
            Console.WriteLine("-----------------Blockchain: Proof of Work Implementation -----------------");

            // Host IP Address
            iPHost = Dns.GetHostEntry(Dns.GetHostName());
            ipAddr = iPHost.AddressList[0];
            Console.WriteLine("Host IP Address: " + ipAddr.ToString());

            // Creating an endpoint (Host + Port)
            localEndPoint = new IPEndPoint(ipAddr, 11111);
            Console.WriteLine("Communication EndPoint: " + localEndPoint);

            Console.Write("\n1. System_Tester: \n2. Wallet:\nChoose (1/2): ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                ExecuteServer();
            }
            else
            {
                ExecuteClient();
            }

            Console.Write("\n\tNew Session Started");
            Block1TestData();


            Console.Write("\n\tStart New Session(Y/N):>");
            char Opt = char.Parse(Console.ReadLine().ToUpper());
            if (Opt == 'Y')
            {
                Block2TestData();

                Console.Write("\n\tDisplay BlockChain Contents(Y/N):>");
                Opt = char.Parse(Console.ReadLine().ToUpper());
                if (Opt == 'Y')
                {
                    //Display
                    DisplayBlockChainContents(BlockChain);
                }
            }
            else if (Opt == 'N')
            {
                Console.Write("\n\tDisplay BlockChain Contents(Y/N):>");
                Opt = char.Parse(Console.ReadLine().ToUpper());
                if (Opt == 'Y')
                {
                    //Display
                    DisplayBlockChainContents(BlockChain);
                }
            }
            else
            {
                Console.WriteLine("\tInvalid Input...");
                Console.Write("\n\tDisplay BlockChain Contents(Y/N):>");
                Opt = char.Parse(Console.ReadLine().ToUpper());
                if (Opt == 'Y')
                {
                    //Display
                    DisplayBlockChainContents(BlockChain);
                }
            }


            Console.WriteLine("\n\tPress any key to exit...");
          Console.ReadKey();
        }
       
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
