using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace P2P_Test
{
    class Program
    {
        static IPHostEntry iPHost;
        static IPAddress ipAddr;
        static IPEndPoint localEndPoint;
        static Socket serverSocket;
        static List<Socket> clientSockets = new List<Socket>();

        static void Main(string[] args)
        {
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

            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }

        static void ExecuteServer()
        {
            try
            {
                serverSocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);

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
    }
}
