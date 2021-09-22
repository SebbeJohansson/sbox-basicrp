using Sandbox;
using System;
partial class BasicRPPlayer
{
	[Net]
	public float Armor { get; set; }

	public void ArmorInit()
	{
		Armor = 100.0f;
	}

}