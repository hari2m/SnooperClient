using System;
using Microsoft.AspNet.SignalR.Client;
using System.IO.Ports;

namespace SnooperClient
{
    class Program
    {
        static void Main()
        {
            string previousButton = null;
            bool previousDirection = false;
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
            Console.WriteLine("Please enter a port number");
            string portNo = Console.ReadLine();
            string port = "COM" + portNo;
            SerialPort _com = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            Controller _controller = new SnooperClient.Controller();

            myHub.On<ControlParamViewModel>("control", param =>
            {
                if (param.direction == previousDirection && param.button == previousButton)
                {
                    
                }
                else
                {
                    previousDirection = param.direction;
                    previousButton = param.button;
                    _controller.controller(_com, param);
                }
            });
        Mark:
            string command = Console.ReadLine();
            if (command != "exit") goto Mark;
            connection.Stop();
        }
    }
}
