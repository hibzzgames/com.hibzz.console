/* Description: Prints a message to the developer console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021 
 */

using UnityEngine;

namespace Hibzz.Console
{
	[CommandTooltip("Prints a message to the developer console")]
	[CreateAssetMenu(fileName = "PrintCmd", menuName = "Console/Built-in Commands/Print")]
	public class Print : ConsoleCommand
	{
		public override bool Process(string[] args)
		{
			string text = string.Join(" ", args);
			DeveloperConsoleUI.Log(text);
			
			return true;
		}
	}
}
