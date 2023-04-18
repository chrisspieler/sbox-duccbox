using Sandbox;
using Sandbox.UI;
using System.Runtime.CompilerServices;

[Library]
public partial class SandboxHud : HudEntity<RootPanel>
{
	private static SandboxHud Instance;

	public SandboxHud()
	{
		if ( !Game.IsClient )
			return;

		RootPanel.StyleSheet.Load( "/Styles/sandbox.scss" );

		RootPanel.AddChild<Chat>();
		RootPanel.AddChild<VoiceList>();
		RootPanel.AddChild<VoiceSpeaker>();
		RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		RootPanel.AddChild<Health>();
		RootPanel.AddChild<InventoryBar>();
		RootPanel.AddChild<CurrentTool>();
		RootPanel.AddChild<SpawnMenu>();
		RootPanel.AddChild<Crosshair>();

		Instance = this;
	}

	[ConCmd.Client( "hud_enable" )]
	public static void EnableHud(bool shouldEnable)
	{
		if ( Instance == null )
		{
			Log.Info( "No HUD exists" );
			return;
		}
		if ( shouldEnable )
		{
			Instance.RootPanel.SetClass( "disabled", false );
		}
		else
		{
			Instance.RootPanel.SetClass( "disabled", true );
		}
	}
}
