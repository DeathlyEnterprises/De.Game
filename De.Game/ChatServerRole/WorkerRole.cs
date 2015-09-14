using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using De.Server;
using Microsoft.WindowsAzure.ServiceRuntime;
using SimpleInjector;

namespace ChatServerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private IServer _chatServer;

        public override void Run()
        {
            Trace.TraceInformation("ChatServerRole is running");

            try
            {
                RunAsync(cancellationTokenSource.Token).Wait();
            }
            finally
            {
                runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            var result = base.OnStart();

            Trace.TraceInformation("ChatServerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("ChatServerRole is stopping");

            cancellationTokenSource.Cancel();
            runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("ChatServerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            var container = new Container();
            var containerRegistration = new ContainerRegistration();
            containerRegistration.RegisterServices(container);

            _chatServer = container.GetInstance<IServer>();

            _chatServer.StartServer();

            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000, cancellationToken);
            }

            _chatServer.StopServer();
        }
    }
}