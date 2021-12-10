
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

[Library]
public partial class RoleplayMenu : Panel
{
	public static RoleplayMenu Instance;
	Panel toollist;

	public Panel Left { get; set; }
	public ButtonGroup LeftTabsHolder { get; set; }
	public Panel LeftBody { get; set; }

	public Panel Right { get; set; }
	public Panel RightTabsHolder { get; set; }
	public Panel RightBody { get; set; }

	public RoleplayMenu()
	{
		Instance = this;

	}

  protected override void PostTemplateApplied() {
    base.PostTemplateApplied();

		StyleSheet.Load( "/ui/RoleplayMenu.scss" );
    
    LeftTabsHolder.AddClass( "tabs" );

    var props = LeftBody.AddChild<SpawnList>();
    LeftTabsHolder.SelectedButton = LeftTabsHolder.AddButtonActive( "Props", ( b ) => props.SetClass( "active", b ) );

    var jobs = LeftBody.AddChild<JobList>();
    LeftTabsHolder.AddButtonActive( "Jobs", ( b ) => jobs.SetClass( "active", b ) );

		var rightTabs = RightTabsHolder.Add.Panel( "tabs" );
    rightTabs.Add.Button( "Tools" ).AddClass( "active" );
    
    toollist = RightBody.Add.Panel( "toollist" );
    RebuildToolList();

    RightBody.Add.Panel( "inspector" );
  }

	void RebuildToolList()
	{
		toollist.DeleteChildren( true );

		foreach ( var entry in Library.GetAllAttributes<Sandbox.Tools.BaseTool>() )
		{
			if ( entry.Title == "BaseTool" )
				continue;

			var button = toollist.Add.Button( entry.Title );
			button.SetClass( "active", entry.Name == ConsoleSystem.GetValue( "tool_current" ) );

			button.AddEventListener( "onclick", () =>
			{
				ConsoleSystem.Run( "tool_current", entry.Name );
				ConsoleSystem.Run( "inventory_current", "weapon_tool" );

				foreach ( var child in toollist.Children )
					child.SetClass( "active", child == button );
			} );
		}
	}

	public override void Tick()
	{
		base.Tick();

		Parent.SetClass( "roleplaymenuopen", Input.Down( InputButton.Menu ) );
	}

	public override void OnHotloaded()
	{
		base.OnHotloaded();

		RebuildToolList();
	}
}
