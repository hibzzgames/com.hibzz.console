/* Description: The UI instance of a developer console
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		17 June, 2021
 */

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Linq;
using System.Collections;

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
		[SerializeField] private TMP_InputField logUI = null;
		[SerializeField] private GameObject UIPanel = null;
		[SerializeField] internal MessageUI messageUI = null; 

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

		[SerializeField] private float width = 210.0f;
		public float Width
		{
			get { return width; }
			set { width = value; UpdateSize(); }
		}

		[SerializeField] private float height = 85.0f;
		public float Height
		{
			get { return height; }
			set { height = value; UpdateSize(); }
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

		/// <summary>
		/// A cyclic queue used to store previous commands
		/// </summary>
		private CyclicQueue<string> PreviousCommands = new CyclicQueue<string>(20);

		/// <summary>
		/// A marker variable used to store the current command that the user is cycling through
		/// </summary>
		private int PreviousCommandMarker = 0;

		/// <summary>
		/// Text that was written, but user decided to cycle through previous commands
		/// </summary>
		private string currentTextBeingEditted = string.Empty;

		/// <summary>
		/// number of lines in the console to be displayed based on height
		/// </summary>
		internal int numberOfLines = 6;

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

			// calculate the number of lines based on height of console and font height of the font
			numberOfLines = Mathf.CeilToInt((height - 27.5f) / 10.551f);

			// new pointer event data on the current event system
			pointerEventData = new PointerEventData(EventSystem.current);
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

			// code cycle through previous commands (if they exist)
			if (Console.IsTextboxFocused)
			{
				int arrowkey = 0; // variable to store whether up or down arrow key is pressed

				#if ENABLE_INPUT_SYSTEM
				if(Keyboard.current[Key.UpArrow].wasPressedThisFrame)
				{
					arrowkey = 1;
				}
				else if(Keyboard.current[Key.DownArrow].wasPressedThisFrame)
				{
					arrowkey = -1;
				}
#else
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					arrowkey = 1;
				}
				else if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					arrowkey = -1;
				}
#endif

				// if up or down arrow was pressed, process accordingly
				if (arrowkey == 1 && PreviousCommandMarker < PreviousCommands.Count)
				{
					// if the user is typing something new, we store it in a cache
					if (PreviousCommandMarker == 0)
					{
						currentTextBeingEditted = inputField.text;
					}

					// retrieve text from previous command queue
					PreviousCommandMarker += 1;
					inputField.text = PreviousCommands.ElementAt(PreviousCommands.Count - PreviousCommandMarker);
					inputField.caretPosition = inputField.text.Length;
				}
				else if (arrowkey == -1 && PreviousCommandMarker > 0)
				{
					// move command marker down
					PreviousCommandMarker -= 1;

					// if in pos 0, it means we have reached user command
					if (PreviousCommandMarker == 0)
					{
						// read from cache and update text
						inputField.text = currentTextBeingEditted;
						inputField.caretPosition = inputField.text.Length;
					}
					else
					{
						// if not in 0 pos, then we read from the previous command queue
						inputField.text = PreviousCommands.ElementAt(PreviousCommands.Count - PreviousCommandMarker);
						inputField.caretPosition = inputField.text.Length;
					}
				}
			}
		}

		private void OnValidate()
		{
			UpdateDefaultColor();
			UpdateSize();
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
			// add the input to the previous command queue
			PreviousCommands.Enqueue(input);
			PreviousCommandMarker = 0;

			// process the command and clear the text field
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
		/// Stores pointer event data of the current mouse
		/// </summary>
		private PointerEventData pointerEventData = null;

		/// <summary>
		/// A list to store raycast result when checking for mouse hovering over console
		/// </summary>
		private List<RaycastResult> raycastResults = new List<RaycastResult>();

		/// <summary>
		/// Is the user currently hovering over the console panel
		/// </summary>
		/// <returns> True if the user is hovering over the console panel </returns>
		private bool IsHoveredOverConsole()
		{
			#if ENABLE_INPUT_SYSTEM
			pointerEventData.position = Mouse.current.position.ReadValue();
			#else
			pointerEventData.position = Input.mousePosition;
			#endif
			
			raycastResults.Clear();
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
			logUI.text = developerConsole.GetLogs().Trim();
		}

		/// <summary>
		/// Updates the default color of other elements assosciated with it
		/// </summary>
		private void UpdateDefaultColor()
		{
			TMP_Text logtext = logUI.GetComponentInChildren<TMP_Text>();
			logtext.color = defaultColor;
		}

		/// <summary>
		/// Updates the size of the console
		/// </summary>
		/// <returns> Nothing. It returns nothing. Nada. </returns>
		private void UpdateSize()
		{
			UIPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			
			RectTransform MessageUIRect = messageUI.GetComponent<RectTransform>();
			MessageUIRect.sizeDelta = new Vector2(width, MessageUIRect.sizeDelta.y);
			MessageUIRect.anchoredPosition = new Vector2(MessageUIRect.anchoredPosition.x, height + 22.5f);

			numberOfLines = Mathf.CeilToInt((height - 27.5f) / 10.551f);

			string text = "";
			for(int i = numberOfLines; i > 0; --i)
			{
				text += "log " + i + "\n";
			}
			logUI.text = text.TrimEnd();
		}

#if UNITY_EDITOR

		/// <summary>
		/// [Editor only function] Scans for new commands
		/// </summary>
		public List<ConsoleCommand> Scan()
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

			return commands;
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
				List<ConsoleCommand> commands = console.Scan();

				// after scanning, reserialize the list
				SerializedProperty commandsProperty = serializedObject.FindProperty("commands");

				commandsProperty.ClearArray();
				int i = 0;

				foreach(var command in commands)
				{
					commandsProperty.InsertArrayElementAtIndex(i);
					commandsProperty.GetArrayElementAtIndex(i).objectReferenceValue = command;
					i++;
				}

				serializedObject.ApplyModifiedProperties();
			}
		}
	}

#endif
}