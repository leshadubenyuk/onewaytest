using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace TestOneWay.Contracts
{
	public interface IHuman : IGrainWithIntegerKey
	{
		Task<string> Respond(string response);
		Task<string> Test(string response);

	}
}
