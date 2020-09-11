﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using TestOneWay.Contracts;

namespace TestOneWay.Client
{
	class Program
	{
		static int Main(string[] args)
		{
			return RunMainAsync().Result;
		}

		private static async Task<int> RunMainAsync()
		{
			try
			{
				using (var client = await ConnectClient())
				{
					await DoClientWork(client);
					Console.ReadKey();
				}

				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine($"\nException while trying to run client: {e.Message}");
				Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
				Console.WriteLine("\nPress any key to exit.");
				Console.ReadKey();
				return 1;
			}
		}

		private static async Task<IClusterClient> ConnectClient()
		{
			IClusterClient client;
			client = new ClientBuilder()
				.UseLocalhostClustering()
				.Configure<ClusterOptions>(options =>
				{
					options.ClusterId = "dev";
					options.ServiceId = "OrleansBasics";
				})
				.ConfigureLogging(logging => logging.AddConsole())
				.Build();

			await client.Connect();
			Console.WriteLine("Client successfully connected to silo host \n");
			return client;
		}

		private static async Task DoClientWork(IClusterClient client)
		{
			// example of calling grains from the initialized client
			var friend1 = client.GetGrain<IHello>(0);
			var friend2 = client.GetGrain<IHuman>(0);
			var response1 =  friend1.SayHello("1");
			var response2 =  friend2.Respond("2");

			Console.WriteLine($"\n\nAll done\n\n");
		}
	}
}
