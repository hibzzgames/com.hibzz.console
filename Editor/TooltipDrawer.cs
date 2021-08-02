/* Script:		TooltipDrawer.cs
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		01 August, 2021
 * Description: Custom editor for the ConsoleCommands that pairs with the 
 *				Tooltip attribute  */

using UnityEditor;

namespace Hibzz.Console
{
	[CustomEditor(typeof(ConsoleCommand), editorForChildClasses: true)]
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
			EditorGUILayout.LabelField(tooltip);
			EditorGUILayout.Space();
			base.OnInspectorGUI();
		}
	}
}
