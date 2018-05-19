﻿using System;
using System.Threading.Tasks;
using MassTransit;

namespace HildenCo.MassTransit.InMemory
{
    class Program
    {
        private static MTService svc;

        static void Main(string[] args)
        {
            OnInit();  
            
            svc = new MTService();
            svc.Init();
            var run = true;

            while(run){
                Console.Write("> ");
                var cmd = Console.ReadLine();
                var slices = (cmd ?? "").Split(' ');

                switch(slices[0]){
                    case "q":
                        run = false;
                        break;

                    case "s":
                        SendMsg(slices);
                        break;

                    default:
                        Log("\r\nOptions: [h]elp | [s]end mail name | [q]uit ");
                        break;
                }
            }

            OnShutDown();
        }

        private static void SendMsg(string[] slices)
        {
            if (slices == null || slices.Length != 3){
                return;
            }

            Log(" Submitting msg...\r\n");
            svc.Send(new CreateAccount{
                Email = slices[1],
                Name = slices[2]
            }).Wait();
        }

        private static void OnInit()
        {
            Log("-----------------------------");
            Log("Initting MT.InMemoryService...");
            Log("-----------------------------\r\n");
        }

        private static void OnShutDown()
        {
            Log("-----------------------------");
            Log("Shutting down MT.InMemoryService...");
            Log("-----------------------------");
        }

        private static void Log(string msg){
            Console.WriteLine(msg);
        }
    }
}