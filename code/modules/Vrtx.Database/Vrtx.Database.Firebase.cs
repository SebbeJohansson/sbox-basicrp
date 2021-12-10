using Sandbox;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;

namespace Vrtx.Database{
  public partial class Connector {

		private string _firebase_url = "https://vrtx-database.firebaseio.com/";
		private string _firebase_key = "";
		private string _firebase_secret = "";
		private string _firebase_token = "";
		private string _firebase_token_expire = "";

		public void Firebase_Init(string url, string key, string secret){
			_firebase_url = url;
			_firebase_key = key;
			_firebase_secret = secret;
		}

		public void Connect(){
			if(_firebase_key == "" || _firebase_secret == ""){
				throw new Exception("Firebase key or secret not set");
			}
			if(_firebase_token == "" || _firebase_token_expire == ""){
				Firebase_GetToken();
			}
			if(DateTime.Now > DateTime.Parse(_firebase_token_expire)){
				Firebase_GetToken();
			}
		}

		// Get token from firebase using key and secret
		public void Firebase_GetToken(){
			string url = _firebase_url + "?auth=" + _firebase_key + ":" + _firebase_secret;
			string json = Utils.GetWebPage(url);
			JsonDocument doc = JsonDocument.Parse(json);
			JsonElement root = doc.RootElement;
			_firebase_token = root.GetProperty("token").GetString();
			_firebase_token_expire = root.GetProperty("expire").GetString();
		}

		// Authenticate with Firebase
		// Use 

    private static void InsertFirebase(string Table, Dictionary<string, object> Values, Dictionary<string, object> Filter){
      Log.Info("Saving data to flat file.");
      string steamid = Filter.GetValueOrDefault("steamid").ToString();
      FileSystem.Data.WriteJson($"{steamid}-{Table}.json", Values);
    }
    
    private static T SelectFirebase<T>(string Table, string[] Columns, Dictionary<string, object> Filter){
      string steamid = Filter.GetValueOrDefault("steamid").ToString();
			
    }

		// Pull data from firebase based on steamid using websocket
		// Output data is a dictionary of key value pairs
		private static T SelectDataFromFirebase<T>(string Table, string[] Columns, Dictionary<string, object> Filter){
			string steamid = Filter.GetValueOrDefault("steamid").ToString();
			string url = $"https://vrtx-database.firebaseio.com/{steamid}/{Table}.json";
			string json = FileSystem.Data.ReadJson(url);
			T data = JsonSerializer.Deserialize<T>(json);
			return data;
		}
  }
}
