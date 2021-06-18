/* Description: A class that represents a log
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		18th June, 2021
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Console
{
	public class Log
	{
		public readonly string message;
		public readonly DateTime time;

		// constructor
		public Log(string message)
		{
			this.message = message;
			this.time = DateTime.Now;
		}

		// Format and print the log message
		public new string ToString()
		{
			return "[" + time.ToString("hh:mm") + "] " + message; 
		}
	}
}