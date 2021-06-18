/* Description: The core logic for implementing the in-game developer console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hibzz.Console
{
	public class DeveloperConsole
	{
		private readonly string prefix;							// the prefix to process the registered commands
		private readonly IEnumerable<ConsoleCommand> commands;  // registered list of commands

		private CyclicQueue<Log> logs;

		/// <summary>
		/// constructor that takes in a prefix and list of commands
		/// </summary>
		/// <param name="prefix">	The prefix for the executing the command. </param>
		/// <param name="commands"> A list of commands to process </param>
		public DeveloperConsole(string prefix, IEnumerable<ConsoleCommand> commands)
		{
			this.prefix = prefix;
			this.commands = commands;

			this.logs = new CyclicQueue<Log>(4);
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
				DeveloperConsoleUI.Log(input);
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

				if(!command.Process(args))
				{
					// TODO: Print "invalid args"
					return;
				}
			}

			// TODO: Notify "Invalid command"
		}

		/// <summary>
		/// Gets a string representation of the logs
		/// </summary>
		/// <returns></returns>
		public string GetLogs()
		{
			string result = "";

			// 6 (7 - 1 = 6) is the number of messages I've configured.
			// At a later point I must change it to something that users can configure from the
			// editor like the logger size or something
			for (int i = logs.Count - 1; i >= logs.Count - 7; --i)
			{
				// skip if it's an invalid element
				if(i < 0) { return result; }
				result = logs.ElementAt(i).ToString() + Environment.NewLine + result;
			}

			return result;
		}

		/// <summary>
		/// Add a log with the given message
		/// </summary>
		/// <param name="message"> The message to print </param>
		public void AddLog(string message)
		{
			logs.Enqueue(new Log(message));
		}
	}
}