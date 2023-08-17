using System;
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
        static Socket sender;
        static Socket clientSocket;
        static Socket listner;

        static void Main(string[] args)
        {
            //Hoost IP Adress
            iPHost = Dns.GetHostEntry(Dns.GetHostName());
            ipAddr = iPHost.AddressList[0];
             Console.WriteLine("Host IP Adress: " + ipAddr.ToString());

            //Creating an endpoint (Host + Port)
             localEndPoint = new IPEndPoint(ipAddr, 11111);
            Console.WriteLine("Communication EndPoint: "+localEndPoint);

           

            Console.WriteLine("\n1. Sever: \n2. Client: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                ExecuteServer();
            }
            else
            {
                //ConnectClient();
                    ExecuteClient();
            }


            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }

        static void ExecuteServer()
        {
            int iNum = 0;
            try
            {
                listner = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    listner.Bind(localEndPoint);

                    listner.Listen(10);

                  
                        Console.WriteLine("Waiting Connection...");

                        clientSocket = listner.Accept();
                        iNum++;

                        Console.WriteLine(iNum + " Client Has joined");

                        byte[] bytes = new byte[1024];
                        
                        
                       int numByte = clientSocket.Receive(bytes);

                        string data = Encoding.ASCII.GetString(bytes, 0, numByte);
                        Console.WriteLine("Text Received: " + data + " from Client " + iNum);
                        



                        byte[] message = Encoding.ASCII.GetBytes(data);
                        clientSocket.Send(message);
                        

                        
                        

                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        static void ExecuteClient()
        {
            try
            {
                //Sender
                 clientSocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    clientSocket.Connect(localEndPoint);
                    //Console.WriteLine(sender.Connected);
                    Console.WriteLine("Socket Connected to " + clientSocket.RemoteEndPoint.ToString());

                                        Console.Write("Enter Message to Send: ");
                    string me = Console.ReadLine();
                    
                    byte[] messageSent = Encoding.ASCII.GetBytes(me);
                    int byteSent = clientSocket.Send(messageSent);

                    //Receive from Server
                    byte[] messageRecieved = new byte[1024];
                    int bytRecv = clientSocket.Receive(messageRecieved);
                    while (messageRecieved.Length > 0)
                    {
                        Console.WriteLine("Message from Server: " + Encoding.ASCII.GetString(messageRecieved, 0, bytRecv));
                        Thread.Sleep(7000);
                    }

                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
