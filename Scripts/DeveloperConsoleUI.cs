/* Description: The UI instance of a developer console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 */

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

#if ENABLE_INPUT_SYSTEM
	using UnityEngine.InputSystem;
#endif

namespace Hibzz.Console
{
	public class DeveloperConsoleUI : MonoBehaviour
	{
		[SerializeField] private string prefix = string.Empty;
		[SerializeField] private List<ConsoleCommand> commands = new List<ConsoleCommand>();

		[Header("UI")]
		[SerializeField] private GameObject uiCanvas = null;
		[SerializeField] private TMP_InputField inputField = null;
		[SerializeField] private TMP_Text logUI = null;
		[SerializeField] private GameObject UIPanel = null;

		[Header("Input")]
		#if ENABLE_INPUT_SYSTEM
		[SerializeField] private Key activationKey = Key.Slash;
		#else
		[SerializeField] private KeyCode activationKeyCode = KeyCode.Slash;
		#endif

		[Header("Visuals")]
		[SerializeField] private Color defaultColor = Color.white;
		public Color DefaultColor
		{
			get { return defaultColor; }
			set { defaultColor = value; UpdateDefaultColor(); }
		}

		// Singleton UI instancce
		internal static DeveloperConsoleUI instance;

		private DeveloperConsole developerConsole;
		public DeveloperConsole DeveloperConsole
		{
			get
			{
				if(developerConsole == null) 
				{ 
					developerConsole = new DeveloperConsole(prefix, commands); 
				}

				return developerConsole;
			}
		}

		private void Awake()
		{
			// singleton pattern that destroys any new Developer Console UI
			if(instance != null && instance != this)
			{
				Destroy(gameObject);
				return;
			}

			// set the singleton instance to this class and configure the gameobject
			// to be not destroyed on load
			instance = this;
			DontDestroyOnLoad(gameObject);
			logUI.text = string.Empty;
		}

		private void Update()
		{
			#if ENABLE_INPUT_SYSTEM
			if(Keyboard.current[activationKey].wasPressedThisFrame)			
			#else
			if (Input.GetKeyDown(activationKeyCode))
			#endif
			{
				ActivateConsole();
			}

			// Update console info
			Console.IsHovered = IsHoveredOverConsole();
			Console.IsTextboxFocused = inputField.isFocused;

			// If hovered and scrolling
			if (Console.IsHovered) 
			{
				// get the mouse scroll delta from the mouse
				#if ENABLE_INPUT_SYSTEM
				Vector2 scrollvec = Mouse.current.scroll.ReadValue();
				#else
				Vector2 scrollvec = Input.mouseScrollDelta;
				#endif
				
				if(scrollvec.y > 0) 
				{
					DeveloperConsole.ScrollUp();
					UpdateLogText();

				}
				else if(scrollvec.y < 0)
				{
					DeveloperConsole.ScrollDown();
					UpdateLogText();
				}
			}
		}

		private void OnValidate()
		{
			UpdateDefaultColor();
		}

		/// <summary>
		/// Toggle the UI
		/// </summary>
		public void ToggleUI()
		{
			uiCanvas.SetActive(uiCanvas.activeSelf);
		}

		/// <summary>
		/// Activate the console
		/// </summary>
		public void ActivateConsole()
		{
			// if it's not already focused
			if(!inputField.isFocused)
			{
				uiCanvas.SetActive(true);
				inputField.text += prefix;
				inputField.ActivateInputField();
				inputField.caretPosition = inputField.text.Length;
			}
		}

		/// <summary>
		/// Process the given input string as a command
		/// </summary>
		/// <param name="input"> The input string to process </param>
		public void ProcessCommand(string input)
		{
			// if return wasn't pressed that frame, then don't process the command
#if ENABLE_INPUT_SYSTEM
			if(!Keyboard.current[Key.Enter].wasPressedThisFrame) { return; }
#else
			if(!Input.GetKeyDown(KeyCode.Return)) { return; }
#endif

			DeveloperConsole.ProcessCommand(input);
			inputField.text = string.Empty;
		}

		/// <summary>
		/// Add message as a log to the logger
		/// </summary>
		/// <param name="message"> The message to add </param>
		/// <param name="color"> The color of the message </param>
		internal void AddLog(string message, Color color)
		{
			DeveloperConsole.AddLog(message, color);
			UpdateLogText();
		}

		/// <summary>
		/// clears the logs in the developer console
		/// </summary>
		internal void ClearLogs()
		{
			DeveloperConsole.Clear();
			UpdateLogText();
		}

		/// <summary>
		/// Is the user currently hovering over the console panel
		/// </summary>
		/// <returns> True if the user is hovering over the console panel </returns>
		private bool IsHoveredOverConsole()
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current); // can be cached

#if ENABLE_INPUT_SYSTEM
			pointerEventData.position = Mouse.current.position.ReadValue();
#else
			pointerEventData.position = Input.mousePosition;
#endif
			
			List<RaycastResult> raycastResults = new List<RaycastResult>(); // can be cached as well
			EventSystem.current.RaycastAll(pointerEventData, raycastResults);

			// if it's hovered over the UI panel, then we return true
			// else we continue and eventually return false
			foreach (RaycastResult result in raycastResults)
			{
				if(result.gameObject == UIPanel) { return true; }
			}

			return false;
		}

		/// <summary>
		/// Updates the log text
		/// </summary>
		private void UpdateLogText()
		{
			logUI.text = developerConsole.GetLogs();
		}

		/// <summary>
		/// Updates the default color of other elements assosciated with it
		/// </summary>
		private void UpdateDefaultColor()
		{
			logUI.color = defaultColor;
		}

#if UNITY_EDITOR

		/// <summary>
		/// [Editor only function] Scans for new commands
		/// </summary>
		public void Scan()
		{
			// clear the list of existing commands
			commands.Clear();

			// use tags to scan the asset database
			string[] guids = AssetDatabase.FindAssets("t:" + typeof(ConsoleCommand).Name);
			foreach(string guid in guids)
			{
				// get the command from the guid
				string path = AssetDatabase.GUIDToAssetPath(guid);
				ConsoleCommand command = AssetDatabase.LoadAssetAtPath<ConsoleCommand>(path);

				// if the command is marked to include in the scan,
				// then we add it to the list of commands
				if (command.IncludeInScan)
				{
					commands.Add(command);
				}
			}
		}

#endif
	}

#if UNITY_EDITOR

	[CustomEditor(typeof(DeveloperConsoleUI))]
	public class DeveloperConsoleInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			// Draw the default inspector and some spacing below it
			DrawDefaultInspector();
			GUILayout.Space(10);

			// Draw a button that scans for new commands
			DeveloperConsoleUI console = target as DeveloperConsoleUI;
			if(GUILayout.Button("Scan for Commands", GUILayout.Height(25)))
			{
				console.Scan();
			}
		}
	}

#endif
}