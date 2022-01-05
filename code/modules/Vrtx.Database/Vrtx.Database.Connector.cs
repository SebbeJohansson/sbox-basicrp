using Sandbox;
using System;
using System.Collections.Generic;

namespace Vrtx.Database{
  public partial class Connector {

    public enum ConnectorType : ushort
    {
      FlatFiles = 1,
      VrtxQueryLanguage = 2,
      FireBase = 3
    }

    public ConnectorType SelectedConnector;

    public Connector(ConnectorType Type){
      SelectedConnector = Type;

			Log.Info("Websocket started");
			var ws = new WebSocket();
			ws.Connect("ws://localhost:8999");
			ws.Send("Hello World");
			

      switch(Type) {
        case ConnectorType.FlatFiles: {
          break;
        }
        case ConnectorType.FireBase: {
          break;
        }
        default: {
          Log.Error("No ConnectorType was specified in DatabaseConnector");
          break;
        }
      }
    }

    /// <summary>
    /// First point of contact function for selecting data from the datasource.
    /// </summary>
    /// <param name="Table"></param>
    /// <param name="Columns"></param>
    /// <param name="Filter"></param>
    /// <param name="Callback"></param>
    /// <returns>Returns a json string.</returns>

    public void Select<T>(string Table, string[] Columns, Dictionary<string, object> Filter, Action<T> Callback){
      
      switch(SelectedConnector) {
        case ConnectorType.FlatFiles: {
          Log.Info("Connector type is flatfiles so lets run that function");
          Callback(SelectFlatFiles<T>(Table, Columns, Filter));
          break;
        }
        default: {
          Log.Error("No ConnectorType was specified in DatabaseConnector");
          break;
        }
      }
    }

    /// <summary>
    /// First point of contact function for Inserting data in the datasource.
    /// </summary>
    /// <param name="Table"></param>
    /// <param name="Values"></param>
    public void Insert(string Table, Dictionary<string, object> Values, Dictionary<string, object> Filter){
      switch(SelectedConnector) {
        case ConnectorType.FlatFiles: {
          Log.Info("Connector type is flatfiles so lets run that function");
          InsertFlatFiles(Table, Values, Filter);
          break;
        }
        default: {
          Log.Error("No ConnectorType was specified in DatabaseConnector");
          break;
        }
      }
    }

    /// <summary>
    /// First point of contact function for Updating data in the datasource.
    /// </summary>
    /// <param name="Table"></param>
    /// <param name="Values">Key value pair for [column] = value.</param>
    /// <param name="Filter"></param>
    public void Update(string Table, string[][] Values, string[] Filter){
      
    }

  }
}
