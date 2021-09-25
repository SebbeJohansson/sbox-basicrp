using Sandbox;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;

namespace Vrtx.Database{
  public partial class Connector {

    private static void InsertFlatFiles(string Table, Dictionary<string, object> Values, Dictionary<string, object> Filter){
      Log.Info("Saving data to flat file.");
      string steamid = Filter.GetValueOrDefault("steamid").ToString();
      FileSystem.Data.WriteJson($"{steamid}-{Table}.json", Values);
    }
    
    private static T SelectFlatFiles<T>(string Table, string[] Columns, Dictionary<string, object> Filter){
      string steamid = Filter.GetValueOrDefault("steamid").ToString();
      try {
        T data = FileSystem.Data.ReadJson<T>($"{steamid}-{Table}.json");
        return data;
      } catch {
        Log.Warning($"File '{steamid}-{Table}.json' doesnt exist.");
        return default;
      }
    }

  }
}