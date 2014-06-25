using UnityEngine;
using System.Collections;
using System.Threading;

public class Manager : MonoBehaviour
{
	#region SINGLETON
	private static Manager _instance;
	public static Manager I
	{
		get
		{
			if (_instance == null) _instance = FindObjectOfType(typeof(Manager)) as Manager;
			return _instance;
		}
	}
	private void OnApplicationQuit () { _instance = null; }
	#endregion

	public GameObject PlayerPrefab;
	public GameObject PointerPrefab;
	public GameObject TreePrefab;
	public GameObject HousePrefab;

	private void Awake () 
	{
		StartCoroutine(Init());
	}

	// sequencially getting objects info from server and instantiating them
	private IEnumerator Init ()
	{
		Thread thread = new Thread(Server.GetPoint);
		thread.Start();
		while (thread.IsAlive) yield return false;
		GameObject.Instantiate(PointerPrefab, new Vector3(Server.PointerSpawnPosition.x, 15, Server.PointerSpawnPosition.y), Quaternion.identity);

		thread = new Thread(Server.GetObstacles);
		thread.Start();
		while (thread.IsAlive) yield return false;
		for (int i = 0; i < 100; i++)
			GameObject.Instantiate(Server.ObstacleTypes[i] == ObstacleType.Tree ? TreePrefab : HousePrefab, 
				new Vector3(Server.ObstacleSpawnPositions[i].x, 15, Server.ObstacleSpawnPositions[i].y), Quaternion.identity);

		thread = new Thread(Server.GetPlayer);
		thread.Start();
		while (thread.IsAlive) yield return false;
		GameObject.Instantiate(PlayerPrefab, new Vector3(Server.PlayerSpawnPosition.x, 15, Server.PlayerSpawnPosition.y), Quaternion.identity);

		yield return true;
	}

	public void Restart ()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}