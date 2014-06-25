using UnityEngine;
using System.Collections;

public class PlaceOnTerrain : MonoBehaviour
{
	private void Start () 
	{
		// places the object on the terrain surface and randomizes its y-rotation
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			transform.position = hit.point;
			transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), 0);
		}
	}

}