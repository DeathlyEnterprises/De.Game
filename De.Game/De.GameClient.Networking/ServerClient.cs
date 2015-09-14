using De.ChatService.Contracts;
using De.Client;

namespace De.GameClient.Networking
{
    public class ServerClient
    {
        private readonly IClient<IChatService> _chatClient;

        public ServerClient()
        {
            _chatClient = new Client<IChatService>("localhost", "8888", "De.Game.ChatService");
        }

        public void Connect()
        {
            _chatClient.Connect();
        }

        public void Disconnect()
        {
            _chatClient.Disconnect();
        }

        public IChatService GetChatClient()
        {
            return _chatClient.GetService();
        }
    }
}