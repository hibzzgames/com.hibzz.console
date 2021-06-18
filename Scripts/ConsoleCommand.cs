/* Description: An abstract class that represents a console command
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 */

using UnityEngine;

namespace Hibzz.Console
{
	public abstract class ConsoleCommand : ScriptableObject
	{
		[SerializeField] public string CommandWord = string.Empty;

		// abstract class that children must implement
		public abstract bool Process(string[] args);
	}
}