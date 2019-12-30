using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleClients.SignalrClient;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region SignalR Sample

            var client = new SignalrClient("https://localhost:5051/chatHub");

            //連線
            client.Connection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine($"Occur error in : {task.Exception.GetBaseException()}");
                }
            }).Wait();

            var taskA = Task.Run(() => client.SendTestAsync("AA", "Hello AA"));
            var taskB = Task.Run(() => client.SendTestAsync("BB", "Hello BB"));
            Console.WriteLine($"Tasks are running : {Thread.CurrentThread.ManagedThreadId}");
            await Task.WhenAll(taskA, taskB);

            #endregion

            Console.ReadKey();
        }
    }
}
