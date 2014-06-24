using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AutoSave
{
	// auto save scene and assets on scene run
	static AutoSave ()
	{
		EditorApplication.playmodeStateChanged = () =>
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				EditorApplication.SaveScene();
				EditorApplication.SaveAssets();
			}
		};
	}
}