/* Script:		CustomMenu.cs
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		01 August, 2021
 * Description: This script is used to customize the Unity Menu to add 
 *				easy-to-use functionality for the hibzz.console package */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hibzz.Console
{
	public class CustomMenu
	{
		[MenuItem("GameObject/Custom/Hibzz.Console")]
		static void AddConsolePrefab()
		{
			Object prefab = AssetDatabase.LoadAssetAtPath(
				"Packages/com.hibzz.console/Prefabs/DeveloperConsole.prefab", 
				typeof(GameObject));
			
			PrefabUtility.InstantiatePrefab(prefab);
		}
	}
}
