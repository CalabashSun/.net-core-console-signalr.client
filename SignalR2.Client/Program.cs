using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR2.Client
{
    class Program
    {
        static HubConnection connection;
        static void Main(string[] args)
        {
            //var url = "http://localhost:5000/";
            //var connection = new HubConnection(url);

            //var proxy = connection.CreateHubProxy("memberHubs");

            //connection.Start().Wait();

            //proxy.Invoke("InitUser", "1234561234", "1234561234").Wait();

            //proxy.On<string, string>("InitUser", (n, s) =>
            //{

            //});

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/memberHubs")
                .Build();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            //Program.ReceiveMessage();
            //Program.SendMsg();

            Program.ReceiveUserIn();
            Program.InUser();

            Program.SinglePush();
            Program.AllPush();

            //connection.InvokeAsync("InitUser", "1234561234", "1234561234");
            Console.WriteLine("success");
            Console.ReadLine();
        }

        private static async void ReceiveMessage()
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                    var newMessage = $"{user}: {message}";
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

        private static async void SendMsg()
        {
            try
            {
                await connection.InvokeAsync("SendMsg",
                    "cala", "mytest");
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
                await connection.InvokeAsync("InitUser", "1234561234", "1234561234");
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
