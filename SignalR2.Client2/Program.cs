using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR2.Client2
{
    class Program
    {
        private static HubConnection connection;
        static void Main(string[] args)
        {

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/memberHubs")
                .Build();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };


            Program.ReceiveUserIn();
            Program.InUser();
            Program.SinglePush();
            Program.AllPush();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        private static async void ReceiveUserIn()
        {
            connection.On<string>("NoticeOnline", (message) =>
            {
                var newMessage = $"inituser:{message}";
                Console.WriteLine(newMessage);
            });
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async void InUser()
        {
            try
            {
                await connection.InvokeAsync("InitUser", "1234561234", "987654321123");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async void SinglePush()
        {
            connection.On<string>("MsgPushSingle", (message) =>
            {
                var newMessage = $"这是单个推送的消息:{message}";
                Console.WriteLine(newMessage);
            });
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async void AllPush()
        {
            connection.On<string>("MsgPushAll", (message) =>
            {
                var newMessage = $"这是全局推送的消息:{message}";
                Console.WriteLine(newMessage);
            });
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
