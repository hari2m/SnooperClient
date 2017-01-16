using System;
using Microsoft.AspNet.SignalR.Client;

namespace SnooperClient
{
    class Program
    {
        static void Main()
        {
            //Set connection
            var connection = new HubConnection("http://www.squty.com/signalr/hubs");
            //Make proxy to hub based on hub name on server
            IHubProxy myHub = connection.CreateHubProxy("chatHub");
            //Start connection
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        Console.WriteLine("There was an error opening the connection:{0}",
                            task.Exception.GetBaseException());
                    else Console.WriteLine("Task has returned null");
                }
                else
                {
                    Console.WriteLine("Connected");
                }
            }).Wait();
            //connection.StateChanged += connection_StateChanged;

            myHub.On("control", param => {
                Console.WriteLine(param.message);
            });

            Console.Read();
            connection.Stop();
        }
    }
}
