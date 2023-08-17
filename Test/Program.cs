using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


class Program
{
    static string name;
    static int listenPort = 0;
    static int sendPort = 0;
    static string lastMessage = "";
    static void Main(string[] args)
    {
        Thread listenerThread = null;

        Console.Write("Enter client name: ");
        name = Console.ReadLine();

        Console.Write("Enter a listening port number (even numbers only and unique - try 50000,50002 etc): ");
        listenPort = int.Parse(Console.ReadLine());
        sendPort = listenPort + 1;

        Console.Write("Configuring ports. May take a few seconds..");

        string listeningOnPortsString = listenPort.ToString();

        listenerThread = new Thread(() => ListenForMessages(listenPort));
        listenerThread.Start();
   

        Console.WriteLine("\n\nListening for incoming messages on ports: " + listeningOnPortsString);

        while (true)
        {
            Console.Write(name + " sending on port: " + sendPort.ToString() + ">> Enter a message:\n");
            string message = Console.ReadLine();

            if (message == "exit")
            {
                stopListeningEvent.Set();
                break;
            }


            // Broadcast the message to all clients
            SendMessage(name + ": " + message, IPAddress.Loopback, sendPort);
            lastMessage = message;
        }

    }

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