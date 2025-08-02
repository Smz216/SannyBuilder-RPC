using System;
using System.Diagnostics;
using System.Threading;
using DiscordRPC;

namespace SannyBuilder
{
    class Program
    {
        static DiscordRpcClient rpcClient;
        static bool rpcActive = false;
        static string processName = "sanny"; 
        static string clientId = "1400993954710945884";

        static void Main(string[] args)
        {
            Console.Title = "Sanny Builder RPC Watcher";
            Console.WriteLine("Waiting for Sanny Builder");

            while (true)
            {
                bool isRunning = Process.GetProcessesByName(processName).Length > 0;

                if (isRunning && !rpcActive)
                {
                    StartRichPresence();
                }
                else if (!isRunning && rpcActive)
                {
                    StopRichPresence();
                }

                Thread.Sleep(2000); 
            }
        }

        static void StartRichPresence()
        {
            rpcClient = new DiscordRpcClient(clientId);

            rpcClient.Initialize();

            rpcClient.SetPresence(new RichPresence
            {
                Details = "Editing GTA Scripts",
                State = "Working in Sanny Builder",
                Assets = new Assets
                {
                    LargeImageKey = "sanny_logo", 
                    LargeImageText = "Sanny Builder"
                },
                Timestamps = Timestamps.Now
            });

            rpcActive = true;
            Console.WriteLine("RPC Activated.");
        }

        static void StopRichPresence()
        {
            rpcClient.Dispose();
            rpcClient = null;
            rpcActive = false;
            Console.WriteLine("RPC Stopped");
        }
    }
}
