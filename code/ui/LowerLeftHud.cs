using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class LowerLeftHud : Panel
{

	public string Health {get; set;}
	public string Armor {get; set;}
	public string Money {get; set;}
	public string Name {get; set;}

  
	public Panel InnerHealth { get; set; }
	public Panel InnerArmor { get; set; }

	public LowerLeftHud()
	{
    this.StyleSheet.Load( "/ui/LowerLeftHud.scss" );
	}

	public override void Tick()
	{
		var player = Local.Pawn as BasicRPPlayer;
		if ( player == null ) return;

		Health = $"{player.Health.CeilToInt()}";
		Armor = $"{player.Armor.CeilToInt()}";

    InnerHealth.Style.Set("width", $"{player.Health.CeilToInt()}%");
    InnerArmor.Style.Set("width", $"{player.Armor.CeilToInt()}%");

		Name = player.DisplayName;
		Log.Info(player.Money);
		Money = $"{player.Money}$";



	}
}
