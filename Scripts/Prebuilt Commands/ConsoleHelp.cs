/* Description: Prints a link to the console github repository
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		27 June, 2021 
 */

using UnityEngine;

namespace Hibzz.Console
{
	[CommandTooltip("Prints a link to the console github repository")]
	[CreateAssetMenu(fileName = "ConsoleHelpCmd", menuName = "Console/Built-in Commands/Console Help")]
	public class ConsoleHelp : ConsoleCommand
	{
		// used to set default values
		public ConsoleHelp() 
		{
			CommandWord = "console.help";
		}

		public override bool Process(string[] args)
		{
			DeveloperConsoleUI.Log("https://github.com/Hibzz-Games/unity.console");
			return true;
		}
	}
}