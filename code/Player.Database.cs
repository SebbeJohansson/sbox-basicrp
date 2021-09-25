using Sandbox;
using Sandbox.Internal;
using System;
using System.Collections.Generic;

using Vrtx.Database;

partial class BasicRPPlayer
{
  public class PlayerData{
    public string steamid {get;set;}
    public int money {get;set;}
    public string name {get;set;}
  }
  
	public Connector dbCon;

  RealTimeUntil TimeUntilSave;

  public void InitiatePlayer(){
    if (IsClient) return;

    string[] columns = {"steamid", "money"};

    Dictionary<string, object> filter = new();
    filter.Add("steamid", SteamId);

    // Grab info from database with a callback function that will spawn the player.
    dbCon.Select<PlayerData>($"Players", columns, filter, (result) => {
      if (result == null) {
        Log.Info("Callback returned null.");
        SetDefaults();
      } else {
        Log.Info("Callback from select");

        Log.Info(result);

        Money = result.money;
      }

      Respawn();
      SalaryInit();

      TimeUntilSave = 5.0f;
      CheckSavingPlayer();


      Log.Info("Starts timer loop for salary");

    });
  }

  private void SetDefaults(){
    if (IsClient) return;
    Money = 10001;
  }

  public void SavePlayer(){
    if (IsClient) return;

    Log.Info("Saving player");

		Dictionary<string, object> values = new();
		values["name"] = DisplayName;
		values["money"] = Money;

    Dictionary<string, object> filter = new();
    filter.Add("steamid", SteamId);

		dbCon.Insert($"Players", values, filter);
  }

  [Event( "server.tick" )]
  private void CheckSavingPlayer(){
    // TimeUntil has passed so lets save the palyer
    if(TimeUntilSave < 0){
      SavePlayer();
      TimeUntilSave = 240.0f;
    }
  }

}