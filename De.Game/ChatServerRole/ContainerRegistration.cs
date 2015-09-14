using De.ChatService.Contracts;
using De.ChatService.Server;
using De.Server;
using SimpleInjector;

namespace ChatServerRole
{
    internal class ContainerRegistration
    {
        public void RegisterServices(Container container)
        {
            container.Register<IServer>(
                () => new Server<ChatService, IChatService>("localhost", "8888", "De.Game.ChatService"));
        }
    }
}