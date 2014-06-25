using UnityEngine;
using System.Net;
using System.IO;
using System.Collections;
using SimpleJSON;

public static class Server
{
	private const string SERVER_URL = @"http://cwalker-server.appspot.com";

	private const string PLAYER_ACTION = @"/getPlayer";
	private const string POINTER_ACTION = @"/getPointer";
	private const string OBSTACLES_ACTION = @"/getObstacles";

	public static Vector2 PlayerSpawnPosition;
	public static Vector2 PointerSpawnPosition;
	public static Vector2[] ObstacleSpawnPositions = new Vector2[100];
	public static ObstacleType[] ObstacleTypes = new ObstacleType[100];

	public static void GetPlayer ()
	{
		JSONNode json = JSON.Parse(RetreiveData(PLAYER_ACTION));
		PlayerSpawnPosition = new Vector2(json["PlayerPosition"][0].AsInt, json["PlayerPosition"][1].AsInt);
	}

	public static void GetPoint ()
	{
		JSONNode json = JSON.Parse(RetreiveData(POINTER_ACTION));
		PointerSpawnPosition = new Vector2(json["PointerPosition"][0].AsInt, json["PointerPosition"][1].AsInt);
	}

	public static void GetObstacles ()
	{
		JSONNode json = JSON.Parse(RetreiveData(OBSTACLES_ACTION));

		for (int i = 0; i < 100; i++)
		{
			ObstacleTypes[i] = json["Obstacles"][i][0].Value == "tree" ? ObstacleType.Tree : ObstacleType.House;
			ObstacleSpawnPositions[i] = new Vector2(json["Obstacles"][i][1].AsInt, json["Obstacles"][i][2].AsInt);
		}
	}

	private static string RetreiveData (string action)
	{
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SERVER_URL + action);
		request.Method = WebRequestMethods.Http.Get;
		request.Accept = "application/json";
		request.ContentType = "application/json; charset=utf-8";

		HttpWebResponse response = (HttpWebResponse)request.GetResponse();

		StreamReader reader = new StreamReader(response.GetResponseStream());
		return reader.ReadToEnd();
	}
}