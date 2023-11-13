using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PoW
{

   

    class P2PNetwork
    {
        //Network test
        static string name;
        static int listenPort = 49999;
        static int sendPort = 0;
        static string lastMessage = "";
        static List<string> pendingMessagesForSession = new List<string>();

        static ManualResetEvent stopListeningEvent = new ManualResetEvent(false);

        static void ListenForMessages(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            pendingMessagesForSession.Add(lastMessage);
           

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

    }
}
