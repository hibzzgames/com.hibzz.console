/* Description: Grants console admin access if the password entered matches
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		18 June, 2021 
 */

using UnityEngine;

namespace Hibzz.Console
{
	[CommandTooltip("Grants console admin access if the password entered matches")]
	[CreateAssetMenu(fileName = "AdminPasswordCmd", menuName = "Console/Built-in Commands/Admin Passsword")]
	public class AdminPassword : ConsoleCommand
	{
		[Tooltip("The password to set")]
		[SerializeField] private string password = "default";

		public AdminPassword()
		{
			CommandWord = "admin";
			IncludeInScan = false;	// Use something more secure if needed, this is a simple example
		}

		public override bool Process(string[] args)
		{
			// if there are no args given then we request a secured password
			if(args.Length < 1)
			{
				Console.RequestSecureInput(this);
				return true;
			}

			// If the incoming argument matches the keyword "-revoke", it revokes
			// console admin access
			if(args[0] == "-r" || args[0] == "-revoke")
			{
				Console.RevokeAdminAccess();
				Console.ReportInfo("Admin access removed");
				return true;
			}

			Console.ReportWarning("Unrecognized parameters");
			return false;
		}

		/// <summary>
		/// This function is used to handle a secure input
		/// </summary>
		/// <param name="input"> The user given secure input </param>
		public override void HandleSecureInput(string input)
		{
			// if the password matches, grant admin access and report a success message
			if(password == input)
			{
				Console.RequestAdminAccess();
				Console.ReportSuccess("Admin access granted");
			}
			else
			{
				// else post a message that the user entered an incorrect password
				Console.ReportError("Incorrect password");
			}
		}
	}
}