using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.IO;

namespace PoW
{
    class Program
    {
        static Wallet User1 = new Wallet();
        static Wallet User2 = new Wallet();
        static Wallet User3 = new Wallet();
        static Wallet User4 = new Wallet();
        
        static IPHostEntry iPHost;
        static IPAddress ipAddr;
        static IPEndPoint localEndPoint;
        static Socket serverSocket;
        static List<Socket> clientSockets = new List<Socket>();

        static List<Transaction> sessionTransactions = new List<Transaction>();

        static Chain BlockChain = Chain.Instance;

        //Original Main
        static void Main(string[] args)
        {
            Chain BlockChain = Chain.Instance;

            

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t--------------------------------------------------------------------------------");
            Console.WriteLine("\t\t\tBlockchain: Proof of Work Implementation");
            Console.WriteLine("\t--------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            // Host IP Address
            iPHost = Dns.GetHostEntry(Dns.GetHostName());
            ipAddr = iPHost.AddressList[0];
            Console.WriteLine("\tHost IP Address: " + ipAddr.ToString());

            // Creating an endpoint (Host + Port)
            localEndPoint = new IPEndPoint(ipAddr, 11111);
            Console.WriteLine("\tCommunication EndPoint: " + localEndPoint);

            Console.Write("\n\t1. System_Tester: \n\t2. Wallet:\n\tChoose (1/2): ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                ExecuteServer();
            }
            else
            {
                ExecuteClient();
            }

            Console.WriteLine("\n\tPress any key to exit...");
            Console.ReadKey();
        }



        /// <summary>
        /// Socket Initialization: The serverSocket is created using the Socket class.
        ///It is configured to use the TCP protocol and is set
        ///up to listen for incoming connections from clients.
        ///The Bind method associates the socket with the local
        ///IP address and port defined in the localEndPoint variable.
        ///The Listen method specifies the maximum number of queued connections.
        ///Accepting Connections: The server enters a loop using while (true) to continuously
        ///listen for incoming client connections.When a client connects,
        ///the serverSocket.Accept() method is called, which blocks until
        ///a client connects.Once a client connection is accepted,
        ///a new socket(clientSocket) is created to handle communication
        ///with that specific client.
        ///Client Handling Thread: A new thread(clientThread) is started to handle communication
        ///with the connected client.The HandleClient method is executed
        ///on this thread, which typically involves receiving and processing
        ///messages from the client and possibly sending responses.
        ///Server Input Thread: Another thread (serverInputThread) is started to handle server-side
        ///input. In this thread, the ServerInput method is executed.
        ///This method allows the server to input messages
        ///(e.g., commands or broadcast messages) and send them to all
        ///connected clients.
        ///Overall, the ExecuteServer method sets up the server to accept incoming client connections,
        ///spawn separate threads to handle communication with each client, and enable the server to
        ///send messages to all connected clients using the ServerInput method.This architecture allows for
        ///concurrent communication with multiple clients while keeping the server responsive to new connections and server-initiated messages.
        /// </summary>
        static void ExecuteServer()
        {
            try
            {

                Transaction t1 = new Transaction(32, User3.PublicKey
                , User1.PublicKey);

                sessionTransactions.Add(t1);


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

                    Thread serverInputThread = new Thread(() => ServerInput());
                    serverInputThread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ServerInput()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\tCommands to send to clients:" +
                        "\n\tcreate : to create transactions for session" +
                        "\n\tsign: sign created transactions" +
                        "\n\tmine <difficulty>: for mining block for current session " +
                        "\n\tsession 2: to start session  2" + 
                        "\n\tsession 3: to start session 3" +
                        "\n\tsession 4/5: to start session 4/5" +
                        "\n\tdisplay: to display blockchain contents" +
                        "\n\tstats: to display mining stats" +
                        "\n\tsave: to save blockchain data to file" +
                        "\n\tquit: to quit the app");

                    Console.Write("\n\tEnter Command: ");
                    string messageToSend = Console.ReadLine();

                    if (messageToSend == "save")
                    {
                        WriteDataToFile(BlockChain);
                    }

                    foreach (var socket in clientSockets)
                    {
                        byte[] messageBytes = Encoding.ASCII.GetBytes(messageToSend);
                        socket.Send(messageBytes);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /// <summary>
        /// Keeps track of the messages sent from clients and checks if message contains certain keyword
        /// </summary>
        /// <param name="clientSocket"></param>
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

                    if (receivedMessage == "create")
                    {
                        //create
                        CreateTransactionsS1();
                        
                        
                    }
                    else if (receivedMessage == "sign")
                    {
                        //sign
                        SignS1Transactions();
                        

                    }
                    else if (receivedMessage.Contains("mine"))
                    {
                        string[] commands = receivedMessage.Split(' ');
                        //mine difficulty
                        CreateAndMineBlockForS1(int.Parse(commands[1]));

                        Console.WriteLine("\tSession 2 to start automatically(difficulty is set to 5): Input: <session 2> to start");

                    }
                    else if (receivedMessage == "display")
                    {
                        DisplayBlockChainContents(BlockChain);
                    }else if(receivedMessage == "stats")
                    {
                        DisplayStats(BlockChain);
                    }
                    else if (receivedMessage == "session 2")
                    {
                        //s2
                        CreateTransactionsS2();
                        //s2
                        SignS2Transactions();
                        //s2
                        CreateAndMineBlockForS2(int.Parse("3"));

                        Console.WriteLine("\tSession 3 to start automatically(difficulty is set to 4): Input: <session 3> to start");
                    }
                    else if (receivedMessage == "session 3")
                    {
                        //s2
                        CreateTransactionsS3();
                        //s2
                        SignS3Transactions();
                        //s2
                        CreateAndMineBlockForS3(int.Parse("4"));
                    }
                    else if (receivedMessage == "session 4")
                    {
                        //s2
                        CreateTransactionsS4();
                        //s2
                        SignS4Transactions();
                        //s2
                        CreateAndMineBlockForS4(int.Parse("2"));
                        Console.WriteLine("\tSession 4 to start automatically(difficulty is set to 2): Input: <session 3> to start");
                    }
                    else if (receivedMessage == "session 5")
                    {
                        //s2
                        CreateTransactionsS5();
                        //s2
                        SignS5Transactions();
                        //s2
                        CreateAndMineBlockForS5(int.Parse("6"));

                        Console.WriteLine("\tSession 5 to start automatically(difficulty is set to 6): Input: <session 3> to start");
                    }else if(receivedMessage == "save")
                    {
                        Console.Write("\n\tData saved to file successfuly ");
                    }
                    else
                    {
                        Console.Write("\n\tInput a correct command ");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Disconnected from System.");
                clientSocket.Close();
                Environment.Exit(0);
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

                            messageBytes = Encoding.Default.GetBytes(Message);
                            socket.Send(messageBytes);
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

                    if (messageToSend == "display")
                    {
                        DisplayBlockChainContents(BlockChain);
                    }
                   
                    byte[] messageSent = Encoding.ASCII.GetBytes(messageToSend);
                    clientSocket.Send(messageSent);
           
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Function for Creating Transaction
        // Calculating the hash for block(mining)
        // Verifying Block
        //adding block to blockchain

        static void CreateTransactionsS1()
        {
            //Create Dummy Transactions

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Transactions for Session 1-----------------\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tTransaction 1\n\tUser 3 Sends 32 Crypto Coins to User 1");
            Transaction t1 = new Transaction(32, User3.PublicKey
                , User1.PublicKey);
            User3.SpendMoney(32); User1.ReceiveMoney(32);
            Thread.Sleep(781);

            Console.WriteLine("\n\tTransaction 2\n\tUser 4 Sends 22 Crypto Coins to User 2");
            Transaction t2 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User4.SpendMoney(22); User2.ReceiveMoney(22);
            Thread.Sleep(370);

            Console.WriteLine("\n\tTransaction 3\n\tUser 1 Sends 32 Crypto Coins to User 2");
            Transaction t3 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(32); User2.ReceiveMoney(32);
            Thread.Sleep(580);

            Console.WriteLine("\n\tTransaction 4\n\tUser 4 Sends 4 Crypto Coins to User 3");
            Transaction t4 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(4); User2.ReceiveMoney(4);
            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            sessionTransactions.Add(t1);
            sessionTransactions.Add(t2);
            sessionTransactions.Add(t3);
            sessionTransactions.Add(t4);

            User1.AddTransaction(t1);
            User1.AddTransaction(t2);
            User1.AddTransaction(t3);
            User1.AddTransaction(t4);   

            foreach (var item in sessionTransactions)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void SignS1Transactions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            //Sign Transactions
            Console.ForegroundColor = ConsoleColor.Blue;
            int counter = 0;
            foreach (var transaction in sessionTransactions)
            {
                var sig = User1.SignTransaction(transaction, User1.PrivateKey);
                counter++;
                Console.WriteLine("\tSigniture for transaction " + counter + ": " + sig);
                Thread.Sleep(780);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CreateAndMineBlockForS1(int difficulty)
        {
            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();

            User1.MineBlock(blockToBeAdded, difficulty);
           

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tBlock has not been verified. Only verified and added to the blockchain when quit option is selected");
 
            User1.AddMinedBlock(blockToBeAdded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t---------------------------------- Session 1 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            sessionTransactions.Clear();

           
        }

        static void CreateTransactionsS2()
        {
            //Create Dummy Transactions

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Transactions for Session 2-----------------\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tTransaction 1\n\tUser 3 Sends 32 Crypto Coins to User 1");
            Transaction t1 = new Transaction(32, User3.PublicKey
                , User1.PublicKey);
            User3.SpendMoney(32); User1.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 2\n\tUser 4 Sends 22 Crypto Coins to User 2");
            Transaction t2 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User4.SpendMoney(22); User2.ReceiveMoney(22);
            Thread.Sleep(559);

            Console.WriteLine("\n\tTransaction 3\n\tUser 1 Sends 32 Crypto Coins to User 2");
            Transaction t3 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(32); User2.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 4\n\tUser 4 Sends 4 Crypto Coins to User 3");
            Transaction t4 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(4); User2.ReceiveMoney(4);
            Thread.Sleep(234);

            Console.WriteLine("\tTransaction 5\n\tUser 3 Sends 32 Crypto Coins to User 1");
            Transaction t5 = new Transaction(32, User3.PublicKey
                , User1.PublicKey);
            User3.SpendMoney(32); User1.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 6\n\tUser 4 Sends 22 Crypto Coins to User 2");
            Transaction t6 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User4.SpendMoney(22); User2.ReceiveMoney(22);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 7\n\tUser 1 Sends 32 Crypto Coins to User 2");
            Transaction t7 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(32); User2.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 8\n\tUser 4 Sends 4 Crypto Coins to User 3");
            Transaction t8 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(4); User2.ReceiveMoney(4);
            Thread.Sleep(780);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            sessionTransactions.Add(t1);
            sessionTransactions.Add(t2);
            sessionTransactions.Add(t3);
            sessionTransactions.Add(t4);
            sessionTransactions.Add(t5);
            sessionTransactions.Add(t6);
            sessionTransactions.Add(t7);
            sessionTransactions.Add(t8);

            User1.AddTransaction(t1);
            User1.AddTransaction(t2);
            User1.AddTransaction(t3);
            User1.AddTransaction(t4);
            User1.AddTransaction(t5);
            User1.AddTransaction(t6);
            User1.AddTransaction(t7);
            User1.AddTransaction(t8);

            foreach (var item in sessionTransactions)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void SignS2Transactions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            //Sign Transactions
            Console.ForegroundColor = ConsoleColor.Blue;
            int counter = 0;
            foreach (var transaction in sessionTransactions)
            {
                var sig = User1.SignTransaction(transaction, User1.PrivateKey);
                counter++;
                Console.WriteLine("\tSigniture for transaction "+ counter + ": "+ sig);
                Thread.Sleep(780);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CreateAndMineBlockForS2(int difficulty)
        {
            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();

           User1.MineBlock(blockToBeAdded, difficulty);
           
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tBlock has not been verified. Only verified and added to the blockchain when quit option is selected");
 
            User1.AddMinedBlock(blockToBeAdded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t---------------------------------- Session 2 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            sessionTransactions.Clear();

            
        }

        static void CreateTransactionsS3()
        {
            //Create Dummy Transactions

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Transactions for Session 3-----------------\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tTransaction 1\n\tUser 1 Sends 43 Crypto Coins to User 2");
            Transaction t1 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User3.SpendMoney(43); User1.ReceiveMoney(43);
            Thread.Sleep(781);

            Console.WriteLine("\n\tTransaction 2\n\tUser 2 Sends 2 Crypto Coins to User 1");
            Transaction t2 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User2.SpendMoney(2); User1.ReceiveMoney(2);
            Thread.Sleep(370);

            Console.WriteLine("\n\tTransaction 3\n\tUser 4 Sends 245 Crypto Coins to User 2");
            Transaction t3 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(245); User2.ReceiveMoney(245);
            Thread.Sleep(580);

            Console.WriteLine("\n\tTransaction 4\n\tUser 3 Sends 14 Crypto Coins to User 4");
            Transaction t4 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(14); User2.ReceiveMoney(14);
            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            sessionTransactions.Add(t1);
            sessionTransactions.Add(t2);
            sessionTransactions.Add(t3);
            sessionTransactions.Add(t4);

            User1.AddTransaction(t1);
            User1.AddTransaction(t2);
            User1.AddTransaction(t3);
            User1.AddTransaction(t4);

            foreach (var item in sessionTransactions)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void SignS3Transactions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            //Sign Transactions
            Console.ForegroundColor = ConsoleColor.Blue;
            int counter = 0;
            foreach (var transaction in sessionTransactions)
            {
                var sig = User1.SignTransaction(transaction, User1.PrivateKey);
                counter++;
                Console.WriteLine("\tSigniture for transaction " + counter + ": " + sig);
                Thread.Sleep(780);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CreateAndMineBlockForS3(int difficulty)
        {
            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();

            User1.MineBlock(blockToBeAdded, difficulty);


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tBlock has not been verified. Only verified and added to the blockchain when quit option is selected");
 
            User1.AddMinedBlock(blockToBeAdded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t---------------------------------- Session 1 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            sessionTransactions.Clear();


        }
        static void CreateTransactionsS4()
        {
            //Create Dummy Transactions

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Transactions for Session 4-----------------\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tTransaction 1\n\tUser 1 Sends 43 Crypto Coins to User 2");
            Transaction t1 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User3.SpendMoney(43); User1.ReceiveMoney(43);
            Thread.Sleep(781);

            Console.WriteLine("\n\tTransaction 2\n\tUser 2 Sends 2 Crypto Coins to User 1");
            Transaction t2 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User2.SpendMoney(2); User1.ReceiveMoney(2);
            Thread.Sleep(370);

            Console.WriteLine("\n\tTransaction 3\n\tUser 4 Sends 245 Crypto Coins to User 2");
            Transaction t3 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(245); User2.ReceiveMoney(245);
            Thread.Sleep(580);

            Console.WriteLine("\n\tTransaction 4\n\tUser 3 Sends 14 Crypto Coins to User 4");
            Transaction t4 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(14); User2.ReceiveMoney(14);
            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            sessionTransactions.Add(t1);
            sessionTransactions.Add(t2);
            sessionTransactions.Add(t3);
            sessionTransactions.Add(t4);

            User1.AddTransaction(t1);
            User1.AddTransaction(t2);
            User1.AddTransaction(t3);
            User1.AddTransaction(t4);

            foreach (var item in sessionTransactions)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void SignS4Transactions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            //Sign Transactions
            Console.ForegroundColor = ConsoleColor.Blue;
            int counter = 0;
            foreach (var transaction in sessionTransactions)
            {
                var sig = User1.SignTransaction(transaction, User1.PrivateKey);
                counter++;
                Console.WriteLine("\tSigniture for transaction " + counter + ": " + sig);
                Thread.Sleep(780);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CreateAndMineBlockForS4(int difficulty)
        {
            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();

            User1.MineBlock(blockToBeAdded, difficulty);


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tBlock has not been verified. Only verified and added to the blockchain when quit option is selected");
 
            User1.AddMinedBlock(blockToBeAdded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t---------------------------------- Session 4 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            sessionTransactions.Clear();


        }
        static void CreateTransactionsS5()
        {
            //Create Dummy Transactions

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Transactions for Session 5-----------------\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tTransaction 1\n\tUser 3 Sends 32 Crypto Coins to User 1");
            Transaction t1 = new Transaction(32, User3.PublicKey
                , User1.PublicKey);
            User3.SpendMoney(32); User1.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 2\n\tUser 4 Sends 22 Crypto Coins to User 2");
            Transaction t2 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User4.SpendMoney(22); User2.ReceiveMoney(22);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 3\n\tUser 1 Sends 32 Crypto Coins to User 2");
            Transaction t3 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(32); User2.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 4\n\tUser 4 Sends 4 Crypto Coins to User 3");
            Transaction t4 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(4); User2.ReceiveMoney(4);
            Thread.Sleep(780);

            Console.WriteLine("\tTransaction 5\n\tUser 3 Sends 32 Crypto Coins to User 1");
            Transaction t5 = new Transaction(32, User3.PublicKey
                , User1.PublicKey);
            User3.SpendMoney(32); User1.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 6\n\tUser 4 Sends 22 Crypto Coins to User 2");
            Transaction t6 = new Transaction(22, User4.PublicKey, User2.PublicKey);
            User4.SpendMoney(22); User2.ReceiveMoney(22);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 7\n\tUser 1 Sends 32 Crypto Coins to User 2");
            Transaction t7 = new Transaction(32, User1.PublicKey
                , User2.PublicKey);
            User1.SpendMoney(32); User2.ReceiveMoney(32);
            Thread.Sleep(780);

            Console.WriteLine("\n\tTransaction 8\n\tUser 4 Sends 4 Crypto Coins to User 3");
            Transaction t8 = new Transaction(4, User4.PublicKey, User3.PublicKey);
            User4.SpendMoney(4); User2.ReceiveMoney(4);
            Thread.Sleep(780);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            sessionTransactions.Add(t1);
            sessionTransactions.Add(t2);
            sessionTransactions.Add(t3);
            sessionTransactions.Add(t4);
            sessionTransactions.Add(t5);
            sessionTransactions.Add(t6);
            sessionTransactions.Add(t7);
            sessionTransactions.Add(t8);

            User1.AddTransaction(t1);
            User1.AddTransaction(t2);
            User1.AddTransaction(t3);
            User1.AddTransaction(t4);
            User1.AddTransaction(t5);
            User1.AddTransaction(t6);
            User1.AddTransaction(t7);
            User1.AddTransaction(t8);

            foreach (var item in sessionTransactions)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void SignS5Transactions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t----------------- Adding Transactions to session list -----------------\n");
            //Sign Transactions
            Console.ForegroundColor = ConsoleColor.Blue;
            int counter = 0;
            foreach (var transaction in sessionTransactions)
            {
                var sig = User1.SignTransaction(transaction, User1.PrivateKey);
                counter++;
                Console.WriteLine("\tSigniture for transaction " + counter + ": " + sig);
                Thread.Sleep(780);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CreateAndMineBlockForS5(int difficulty)
        {
            Console.WriteLine("\tBlock Created for current/Pending session Transactions");
            Block blockToBeAdded = User1.CreateBlock();

            User1.MineBlock(blockToBeAdded, difficulty);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tInfo!\n\t-----");
            bool ver = User1.VerifyMinedBlock(blockToBeAdded);
            Console.WriteLine("\tBlock has not been verified. Only verified and added to the blockchain when quit option is selected");
 
            User1.AddMinedBlock(blockToBeAdded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t---------------------------------- Session 5 End---------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            sessionTransactions.Clear();


        }
        static void WriteDataToFile(Chain BlockChain)
        {
            Random rnd = new Random();
            int fileNum = rnd.Next(100);

            StreamWriter wr = new StreamWriter("Data_" + fileNum + ".txt");
            foreach (var block in BlockChain.chain)
            {
      
                wr.WriteLine(block.ToString());
              
                    foreach (var transaction in block.transactions)
                    {
                        wr.WriteLine(transaction.ToString());
                    }
            }
            wr.Close();
        }

        static void DisplayStats(Chain BlockChain)
        {

            Console.Clear();
            Console.WriteLine("\n\t-----------------Blockchain: Proof of Work Implementation -----------------");

            Console.WriteLine("\n\t--------------------- Statistics -----------------");
            int BlockNum = 1;

            foreach (var item in BlockChain.chain)
            {
                Console.WriteLine("\t--------- Block Number: "+ BlockNum + " -------------");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t" + item.Index);
                Console.ForegroundColor = ConsoleColor.White;
                BlockNum++;
            }

        }
        //Display
        static void DisplayBlockChainContents(Chain BlockChain)
        {

            Console.Clear();
            Console.WriteLine("\n\t----------------- Blockchain: Proof of Work Implementation -----------------");

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
