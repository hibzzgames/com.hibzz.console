/* Description: Prints the current version of the game to the console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		18 June, 2021 
 */

using UnityEngine;

namespace Hibzz.Console
{
	[CommandTooltip("Prints the current version of the game to the console")]
	[CreateAssetMenu(fileName = "VersionCmd", menuName = "Console/Built-in Commands/Version")]
	public class Version : ConsoleCommand
	{
		// used to set default values
		public Version() 
		{
			CommandWord = "version";
		}

		public override bool Process(string[] args)
		{
			Console.Log("Version: " + Application.version);
			return true;
		}
	}
}
