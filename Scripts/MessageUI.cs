using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Hibzz.Console
{
	public class MessageUI : MonoBehaviour
	{
		[Header("UI Elements")]
		/// <summary>
		/// The text representing the message
		/// </summary>
		[SerializeField] private TMP_Text message;

		/// <summary>
		/// The background panel of the message UI
		/// </summary>
		[SerializeField] private Image backgroundPanel;

		[Header("Message Colors")]
		[SerializeField] private Color ErrorColor;

		[SerializeField] private Color WarningColor;

		[SerializeField] private Color InfoColor;

		[SerializeField] private Color SuccessColor;

		private void Start()
		{
			gameObject.SetActive(false);
		}

		/// <summary>
		/// Closes the messages
		/// </summary>
		public void CloseMessageUI()
		{
			gameObject.SetActive(false);
		}

		/// <summary>
		/// Opens the message UI
		/// </summary>
		private void OpenMessageUI()
		{
			gameObject.SetActive(true);
		}

		/// <summary>
		/// Send a message to the Message UI with the specified type
		/// </summary>
		internal void SendMessage(string message, Type type)
		{
			this.message.text = message;

			if (type == Type.Error)
			{
				backgroundPanel.color = ErrorColor;
			}
			else if (type == Type.Warning)
			{
				backgroundPanel.color = WarningColor;
			}
			else if(type == Type.Info)
			{
				backgroundPanel.color = InfoColor;
			}
			else if(type == Type.Success)
			{
				backgroundPanel.color = SuccessColor;
			}

			OpenMessageUI();
		}

		internal enum Type
		{
			Error,
			Warning,
			Info,
			Success
		}
	}
}
