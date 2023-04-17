using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox;

public class ScreenWorld
{
	public SceneWorld SceneWorld { get; private set; }
	public SceneCamera Camera { get; private set; }
	public WorldPanel WorldPanel { get; private set; }
	public WebPanel WebPanel { get; private set; }

	public ScreenWorld()
	{
		SceneWorld = new SceneWorld();
		WorldPanel = new WorldPanel( SceneWorld );
		WebPanel = new WebPanel();
		WebPanel.Surface.Url = "www.google.com";
		WorldPanel.AddChild( WebPanel );
		Camera = new SceneCamera()
		{
			Position = WorldPanel.Position.WithX( 30 ),
			Rotation = Rotation.LookAt( Vector3.Backward ),
			World = SceneWorld,
			BackgroundColor = Color.White
		};
	}

	[Event.Tick.Client]
	public void OnTick()
	{

	}
}
