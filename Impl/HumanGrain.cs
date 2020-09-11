using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using TestOneWay.Contracts;

namespace TestOneWay.Impl
{
	public class HumanGrain : Grain, IHuman
	{
		private readonly ILogger logger;

		public HumanGrain(ILogger<HelloGrain> logger)
		{
			this.logger = logger;
		}

		public async Task<string> Respond(string greeting)
		{
			logger.LogInformation($"\n Human sleeps '{greeting}'");
			await Task.Delay(5000);
			logger.LogInformation($"\n Human wokeup '{greeting}'");
			var helloGrain = GrainFactory.GetGrain<IHello>(0);
			await helloGrain.Test("test");
			logger.LogInformation($"\n HUman done '{greeting}'");

			return $"\n Hüman reponds '{greeting}'";
		}

		public async Task<string> Test(string greeting)
		{
			await Task.Delay(5000);
			return $"\n Hüman test '{greeting}'";
		}
	}
}
