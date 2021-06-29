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
		public readonly Color color;

		// constructor with custom color
		public Log(string message, Color color)
		{
			this.message = message;
			this.time = DateTime.Now;
			this.color = color;
		}

		// Format and print the log message
		public new string ToString()
		{
			string result = "[" + time.ToString("hh:mm") + "] " + message;

			// if the color isn't the default color, then add the custom color tags
			if(color != DeveloperConsoleUI.instance.DefaultColor)
			{
				result = "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" 
					+ result + "</color>";
			}

			return result;
		}
	}
}