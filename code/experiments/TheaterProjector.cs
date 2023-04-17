using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox;

public partial class TheaterProjector : Entity
{
	public ScreenWorld ScreenWorld { get; private set; }
	[Net]
	public Rotation ScreenOrientation { get; set; }
	[Net]
	public SpotLightEntity Placeholder { get; private set; }
	public SpotLightEntity ClientProjector { get; private set; }

	[ConCmd.Server( "projector_add" )]
	public static void AddProjector()
	{
		var projector = new TheaterProjector();
		projector.Name = "Projector Tester";
	}

	public override void Spawn()
	{
		base.Spawn();

		Transmit = TransmitType.Always;

		Placeholder = Entity.All.OfType<SpotLightEntity>().First();
		Placeholder.Name = "oldSpotLight";
		Placeholder.Enabled = false;
	}

	public override void ClientSpawn()
	{
		ClientProjector = CreateFromBase( Placeholder );
		ScreenWorld = new ScreenWorld();
	}

	public SpotLightEntity CreateFromBase(SpotLightEntity other )
	{
		return new SpotLightEntity()
		{
			Enabled = true,
			Position = other.Position,
			Rotation = Quaternion.Identity,
			Brightness = 256,
			BrightnessMultiplier = 2,
			Range = 4096,
			InnerConeAngle = 5,
			OuterConeAngle = 15,
			Name = "projectorSpotlight",
			LightCookie = Texture.CreateRenderTarget( "lightCookieTex", ImageFormat.RGBA8888, new Vector2( 512, 512 ) )
		};
	}

	[Event.Client.Frame]
	public void OnFrame()
	{
		ScreenWorld.WorldPanel.Rotation = ScreenOrientation;
		Graphics.RenderToTexture( ScreenWorld.Camera, ClientProjector.LightCookie );
		DebugOverlay.Texture( ClientProjector.LightCookie, new Rect( 0, 0, 100, 100 ) );
	}
}
