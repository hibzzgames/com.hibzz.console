/* Script:		CommandTooltip.cs
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 * Description: Custom attribute script for adding tooltips for the console commands */

using System;
using UnityEngine;

namespace Hibzz.Console
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CommandTooltip : PropertyAttribute
	{
		public readonly string description;
		public CommandTooltip(string description)
		{
			this.description = description;
		}
	}
}
