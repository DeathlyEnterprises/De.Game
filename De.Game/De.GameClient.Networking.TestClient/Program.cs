using System;

namespace De.GameClient.Networking.TestClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serverClient = new ServerClient();
            serverClient.Connect();
            var chatClient = serverClient.GetChatClient();
            var message = chatClient.Echo("I am so damn good!");
            serverClient.Disconnect();

            Console.WriteLine("Connection was successful!");
            Console.WriteLine("Server returned: {0}", message);
            Console.ReadLine();
        }
    }
}