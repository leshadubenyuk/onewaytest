using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using TestOneWay.Contracts;

namespace TestOneWay.Impl
{
	public class HelloGrain : Grain, IHello
	{
		private readonly ILogger logger;

		public HelloGrain(ILogger<HelloGrain> logger)
		{
			this.logger = logger;
		}

		public async Task<string> SayHello(string greeting)
		{
			logger.LogInformation($"\n Hello sleeps '{greeting}'");
			await Task.Delay(3000);
			logger.LogInformation($"\n Hello wokeup '{greeting}'");
			var helloGrain = GrainFactory.GetGrain<IHuman>(0);
			await helloGrain.Test("Hello test");
			logger.LogInformation($"\n Hello Done '{greeting}'");

			return $"\n Hello reponds '{greeting}'";
		}

		public async Task Test(string greeting)
		{
			await Task.Delay(5000);
			logger.LogInformation($"\n Hello Test '{greeting}'");

			return;
		}
	}
}
