/* Description: Clears all logs in the console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		25 June, 2021 
 */

using UnityEngine;

namespace Hibzz.Console
{
	[CommandTooltip("Clears all logs in the console")]
	[CreateAssetMenu(fileName = "ClearCmd", menuName = "Console/Built-in Commands/Clear")]
	public class Clear : ConsoleCommand
	{
		// used to set default values
		public Clear() 
		{
			CommandWord = "clear";
		}

		public override bool Process(string[] args)
		{
			Console.Clear();
			return true;
		}
	}
}