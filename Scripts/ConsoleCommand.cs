//-----------------------------------------------------------------------------
// Script:			ConsoleCommand.cs
// Author:			Hibnu Hishath (sliptrixx)
// Date:			17 June, 2021
// Description:		An abstract class that represents a console command
//-----------------------------------------------------------------------------

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