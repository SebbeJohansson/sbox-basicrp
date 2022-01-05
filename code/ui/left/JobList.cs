using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Tests;

using Jobs;

[Library]
public partial class JobList : Panel
{
	VirtualScrollPanel Canvas;
  public JobList(){
    AddClass( "spawnpage" );
    AddChild( out Canvas, "canvas" );

    Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemHeight = 0;
		Canvas.Layout.ItemWidth = 100;
		Canvas.Style.Set("flex-direction: column; overflow: hidden; flex-grow: 1; overflow: scroll;");
    Canvas.OnCreateCell = ( cell, data ) =>
    {
      var file = (string)data;
      var panel = cell.Add.Panel( "icon" );
			panel.Style.Set("width: 100%;");
      panel.AddEventListener( "onclick", () => ConsoleSystem.Run( "spawn", "models/" + file ) );
			panel.Style.BackgroundImage = Texture.Load( $"/models/{file}_c.png", false );
    };

    JobData[] jobs = GetAvailableJobs();

    foreach ( var file in FileSystem.Mounted.FindFile( "models", "*.vmdl_c.png", true ) )
    {
      if ( string.IsNullOrWhiteSpace( file ) ) continue;
      if ( file.Contains( "_lod0" ) ) continue;
      if ( file.Contains( "clothes" ) ) continue;

      Canvas.AddItem( file.Remove( file.Length - 6 ) );
    }
  }

  public static JobData[] GetAvailableJobs() {
    if ( Game.Current is not BasicRPGame game ) return System.Array.Empty<JobData>();
    if (game.jobHandler is not JobHandler) return System.Array.Empty<JobData>();

    foreach (var job in game.jobHandler.AvailableJobs) {
      Log.Info(job.Name);
    }
    return game.jobHandler.AvailableJobs;
  }
}
