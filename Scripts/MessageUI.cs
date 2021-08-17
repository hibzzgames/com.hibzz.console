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

		/// <summary>
		/// The icon representing the type of message
		/// </summary>
		[SerializeField] private TMP_Text messageIcon;

		[Header("Message Colors")]
		[SerializeField] private Color ErrorColor;

		[SerializeField] private Color WarningColor;

		[SerializeField] private Color InfoColor;

		[SerializeField] private Color SuccessColor;

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
				messageIcon.text = char.ConvertFromUtf32(0x26d4);
				backgroundPanel.color = ErrorColor;
			}
			else if (type == Type.Warning)
			{
				messageIcon.text = char.ConvertFromUtf32(0x26a0);
				backgroundPanel.color = WarningColor;
			}
			else if(type == Type.Info)
			{
				messageIcon.text = char.ConvertFromUtf32(0x1f4ac);
				backgroundPanel.color = InfoColor;
			}
			else if(type == Type.Success)
			{
				messageIcon.text = char.ConvertFromUtf32(0x2714);
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
