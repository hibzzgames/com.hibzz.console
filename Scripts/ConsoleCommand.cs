/* Description: An abstract class that represents a console command
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 */

using UnityEngine;

namespace Hibzz.Console
{
	public abstract class ConsoleCommand : ScriptableObject
	{
		[Tooltip("The keyword to execute this command")]
		[SerializeField] public string CommandWord = string.Empty;

		[Tooltip("Does the command requires admin access")]
		[SerializeField] public bool RequiresAdminAccess = false;
		
		[Tooltip("This property provides the option to exclude a command from scan")]
		[SerializeField] public bool IncludeInScan = true;

		// abstract class that children must implement
		public abstract bool Process(string[] args);
	}
}