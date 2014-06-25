using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject pointerPrefab;
	public GameObject treePrefab;
	public GameObject housePrefab;

	private void Awake () 
	{
		ObjectType[] objects = new ObjectType[10] 
		{
			ObjectType.House,
			ObjectType.House,
			ObjectType.Tree,
			ObjectType.House,
			ObjectType.House,
			ObjectType.Tree,
			ObjectType.Tree,
			ObjectType.Tree,
			ObjectType.House,
			ObjectType.Tree
		};

		Vector2[] positions = new Vector2[10] 
		{
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
			new Vector2(Random.Range(-24, 25), Random.Range(-24, 25)),
		};

		for (int i = 0; i < 10; i++)
		{
			GameObject.Instantiate(objects[i] == ObjectType.Tree ? treePrefab : housePrefab, new Vector3(positions[i].x, 15, positions[i].y), Quaternion.identity);
		}

		GameObject.Instantiate(pointerPrefab, new Vector3(Random.Range(-24, 25), 15, Random.Range(-24, 25)), Quaternion.identity);
		GameObject.Instantiate(playerPrefab, new Vector3(Random.Range(-24, 25), 15, Random.Range(-24, 25)), Quaternion.identity);
	}

	private void Update () 
	{
    	
	}
}