/* Script:		CommandTooltip.cs
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 * Description: Editor and custom attribute script for adding tooltips for the console commands */

using System;
using UnityEngine;
using UnityEditor;

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

	[CustomEditor(typeof(ScriptableObject), editorForChildClasses: true)]
	public class TooltipDrawer : Editor
	{
		string tooltip;

		private void OnEnable()
		{
			var attributes = target.GetType().GetCustomAttributes(inherit: false);
			foreach (var attr in attributes)
			{
				if (attr is CommandTooltip tooltip)
				{
					this.tooltip = tooltip.description;
				}
			}
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField(tooltip) ;
			EditorGUILayout.Space();
			base.OnInspectorGUI();
		}
	}
}
