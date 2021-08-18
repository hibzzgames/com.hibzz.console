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
			// if there are any arguments passed, check for matching subcommands
			if(args.Length > 0)
			{
				// -cd clears the cache dictionary
				if(args[0] == "-cd")
				{
					Console.Log("Cache dictionary cleared", Color.cyan);
					Console.CacheDictionary.Clear();
					return true;
				}
				else
				{
					Console.LogError("Unkown argument \"" + args[0] + "\"");
					return false;
				}
			}

			// clear the console logs
			Console.Clear();
			return true;
		}
	}
}