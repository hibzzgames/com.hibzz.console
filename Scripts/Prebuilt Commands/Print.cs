/* Script:		Print.cs
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 * Description: Prints a message to the developer console */

using UnityEngine;

namespace Hibzz.Console
{
	[CommandTooltip("Prints a message to the developer console")]
	[CreateAssetMenu(fileName = "PrintCommand", menuName = "Utilities/Console/Commands/Print")]
	public class Print : ConsoleCommand
	{
		public override bool Process(string[] args)
		{
			string text = string.Join(" ", args);
			Debug.Log("Print: " + text);
			
			return true;
		}
	}
}
