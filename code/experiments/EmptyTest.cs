using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox;

public class EmptyTest : ModelEntity
{
	public override void Spawn()
	{
		base.Spawn();

		Transmit = TransmitType.Always;
	}

	[ConCmd.Server( "empty_add" )]
	public static void AddProjector()
	{
		var empty = new EmptyTest();
	}
}
