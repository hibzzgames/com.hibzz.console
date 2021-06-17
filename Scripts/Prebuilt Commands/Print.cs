using UnityEngine;

namespace Hibzz.Console
{
	[CreateAssetMenu(fileName = "PrintCommand", menuName = "Utilities/Console/Commands/Print")]
	public class Print : ConsoleCommand
	{
		public override bool Process(string[] args)
		{
			string text = string.Join(" ", args);
			Debug.Log(text); 
			
			return true;
		}
	}
}
