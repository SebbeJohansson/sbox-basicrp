using Sandbox;
using System;
partial class BasicRPPlayer
{
	[Net]
	public int Money { get; set; }

	public void MoneyInit()
	{
		Money = 10000;
	}

}