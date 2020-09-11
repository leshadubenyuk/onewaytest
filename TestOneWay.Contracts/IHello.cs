using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace TestOneWay.Contracts
{

	public interface IHello : IGrainWithIntegerKey
	{
		Task<string> SayHello(string greeting);
		
		[OneWay]
		Task Test(string greeting);
	}
}