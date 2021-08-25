/* Description: The core logic for implementing the in-game developer console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hibzz.Console
{
	public class DeveloperConsole
	{
		private readonly string prefix;							// the prefix to process the registered commands
		private readonly IEnumerable<ConsoleCommand> commands;  // registered list of commands

		private CyclicQueue<Log> logs;							// data structure used to store the logs
		private int scrollPos = 0;                              // variable that keeps track of current scroll position

		private bool AdminAccess = false;					// does the console have admin access at the moment?

		/// <summary>
		/// constructor that takes in a prefix and list of commands
		/// </summary>
		/// <param name="prefix">	The prefix for the executing the command. </param>
		/// <param name="commands"> A list of commands to process </param>
		public DeveloperConsole(string prefix, IEnumerable<ConsoleCommand> commands)
		{
			this.prefix = prefix;
			this.commands = commands;

			// this ensures that admin access is saved across a unity session
			#if UNITY_EDITOR
			AdminAccess = SessionState.GetBool("ConsoleAdminAccess", false);
			#endif

			logs = new CyclicQueue<Log>(100);
		}

		/// <summary>
		/// A function that processes the entire command line
		/// </summary>
		/// <param name="input"> An example input would be "/spawnweapon katana" </param>
		public void ProcessCommand(string input)
		{
			// if it doesn't start with a prefix, print the message to the logger
			// and break out
			if(!input.StartsWith(prefix)) 
			{
				Console.Log(input);
				return; 
			}

			// remove the prefix and split the command by spaces
			input = input.Remove(0, prefix.Length);
			string[] inputsplit = input.Split(' ');

			// the first string in the list is the command, while the rest are the args
			string commandInput = inputsplit[0];
			string[] args = inputsplit.Skip(1).ToArray();

			// pass these variable to the other process command function
			ProcessCommand(commandInput, args);
		}

		/// <summary>
		/// A function that proccess the given string command if we know how to process it
		/// </summary>
		/// <param name="commandInput"> The command keyword to process		</param>
		/// <param name="args">			The args for the command to process </param>
		public void ProcessCommand(string commandInput, string[] args)
		{
			// loop through each stored command and check if we know to process the given command input
			foreach(var command in commands)
			{
				// if it doesn't match the command string, then check for the next
				if(!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
				{ continue; }

				// if the command requires admin acceess and if the console currently has no admin access, then don't execute the command
				if(command.RequiresAdminAccess && !AdminAccess) 
				{
					Console.ReportWarning("Command requires admin access");
					return; 
				}

				if(!command.Process(args))
				{
					// represents failure to execute command... idk what to do with it
					return;
				}

				return;
			}

			// TODO: Notify "Invalid command"
			Console.ReportWarning("Invalid Command");
		}

		/// <summary>
		/// Gets a string representation of the logs
		/// </summary>
		/// <returns></returns>
		public string GetLogs()
		{
			string result = "";

			// prints based on the number of lines
			for (int i = scrollPos - 1; i >= scrollPos - DeveloperConsoleUI.instance.numberOfLines - 1; --i)
			{
				// skip if it's an invalid element
				if(i < 0) { return result; }
				result = logs.ElementAt(i).ToString() + Environment.NewLine + result;
			}

			return result;
		}

		/// <summary>
		/// Add a log with the given message and color
		/// </summary>
		/// <param name="message"> The message to print </param>
		/// <param name="color"> The color of the message </param>
		public void AddLog(string message, Color color)
		{
			// if not all the way to the bottom, 
			// then increment the scroll position so that the user sees the latest log
			if (scrollPos == logs.Count)
			{
				++scrollPos;
			}

			// detect the links
			message = DetectLinks(message);

			logs.Enqueue(new Log(message, color));
		}

		/// <summary>
		/// Clears all logs in the in-game logger
		/// </summary>
		public void Clear()
		{
			logs.Clear();
			scrollPos = 0;
		}

		/// <summary>
		/// Scroll up the console
		/// </summary>
		public void ScrollUp()
		{
			if(logs.Count > DeveloperConsoleUI.instance.numberOfLines - 1)
			{
				--scrollPos;
				scrollPos = Mathf.Clamp(scrollPos, DeveloperConsoleUI.instance.numberOfLines - 2, logs.Count);
			}
		}

		/// <summary>
		/// Scroll down the console
		/// </summary>
		public void ScrollDown()
		{
			++scrollPos;
			scrollPos = Mathf.Clamp(scrollPos, 0, logs.Count);
		}

		/// <summary>
		/// Detects strings and replace them with link tags
		/// </summary>
		/// <param name="message"> The message to look for links in </param>
		/// <returns> A string where the urls are replaced with links </returns>
		private string DetectLinks(string message)
		{
			Regex regex = new Regex("https://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
			MatchCollection matches = regex.Matches(message);

			foreach (Match match in matches)
			{
				message = message.Replace(match.Value, "<link=\"" + match.Value + "\">" + match.Value + "</link>");
			}

			return message;
		}

		/// <summary>
		/// Gives console admin access
		/// </summary>
		public void RequestAdminAccess()
		{
			AdminAccess = true;

			#if UNITY_EDITOR
			SessionState.SetBool("ConsoleAdminAccess", AdminAccess);
			#endif
		}

		/// <summary>
		/// Revokes console admin access
		/// </summary>
		public void RevokeAdminAccesss()
		{
			AdminAccess = false;

			#if UNITY_EDITOR
				SessionState.EraseBool("ConsoleAdminAccess");
			#endif
		}
	}
}