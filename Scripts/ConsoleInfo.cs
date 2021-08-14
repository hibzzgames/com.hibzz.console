using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Console
{
	/// <summary>
	/// A class with static variables and functions that gives useful information 
	/// about the current status of the developer console
	/// </summary>
	/// <remarks>
	/// Remarks: The info in this class isn't guaranteed to be right if there are no DeveloperConsole on the scene
	/// </remarks>
	public class ConsoleInfo
	{
		/// <summary>
		/// Is the mouse hovering the console UI
		/// </summary>
		public static bool IsHovered = false;

		/// <summary>
		/// Is the console textbox focuesed at the moment
		/// </summary>
		public static bool IsTextboxFocused = false;
	}
}
