using UnityEngine;
using System.Collections;

public class GUIInput : MonoBehaviour
{
	private void OnGUI ()
	{
		if (GUILayout.Button("Restart")) Application.LoadLevel(Application.loadedLevel);
	}
}