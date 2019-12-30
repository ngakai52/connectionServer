using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleClients.SignalrClient
{
    public class SignalrClient
    {
        public HubConnection Connection { get; }

        public SignalrClient(string url)
        {
            this.Connection = new HubConnectionBuilder().WithUrl(url).Build();

            Connection.On<string, string>("ReceiveMessage", (name, message) =>
            {
                Task.Run(() =>
                {
                    Console.WriteLine($"Receive : {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Name : {name} , Message : {message}");
                });

                Console.WriteLine($"Receive task done : {Thread.CurrentThread.ManagedThreadId}");

            });
        }

        public Task SendTestAsync(string name, string message)
        {
            Console.WriteLine($"have called Chat : {Thread.CurrentThread.ManagedThreadId}");
            return Task.Run(() => this.Connection.SendAsync("SendMessage", name, message));
        }

    }
}
