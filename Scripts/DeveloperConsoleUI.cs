//-----------------------------------------------------------------------------
// Script:			DeveloperConsoleUI.cs
// Author:			Hibnu Hishath (sliptrixx)
// Date:			17 June, 2021
// Description:		The UI instance of a developer console
//-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;

namespace Hibzz.Console
{
	public class DeveloperConsoleUI : MonoBehaviour
	{
		[SerializeField] private string prefix = string.Empty;
		[SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

		[Header("UI")]
		[SerializeField] private GameObject uiCanvas = null;
		[SerializeField] private TMP_InputField inputField = null;

		[Header("Input")]
		[SerializeField] private KeyCode activationKeyCode = KeyCode.Slash;

		// Singleton UI instancce
		private static DeveloperConsoleUI instance;

		private DeveloperConsole developerConsole;
		private DeveloperConsole DeveloperConsole
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
		}

		private void Update()
		{
			if(Input.GetKeyDown(activationKeyCode))
			{
				ActivateConsole();
			}
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
				inputField.text = inputField.text + ((char)activationKeyCode);
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
			if(!Input.GetKeyDown(KeyCode.Return)) { return; }

			DeveloperConsole.ProcessCommand(input);
			inputField.text = string.Empty;
		}
	}
}