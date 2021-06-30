/* Description: Script that makes links clicakble and openable in textmeshpro (with richtext enabled)
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		28th June, 2021
 */

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Hibzz.Console
{
	[RequireComponent(typeof(TMP_Text))]
	public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
	{
		private TMP_Text text = null; // A reference to the text to look through
		
		private void Start()
		{
			text = GetComponent<TMP_Text>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			// This enables ctrl + click to open the link
			#if ENABLE_INPUT_SYSTEM
			if(!Keyboard.current[Key.LeftCtrl].isPressed) { return; }
			#else
			if(!Input.GetKey(KeyCode.LeftControl)) { return; }
			#endif

			// find if the text at the given position has any links in it
			int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

			// if the link is clicked, then get the link id by the link index and open it
			if(linkIndex != -1)
			{
				TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
				Application.OpenURL(linkInfo.GetLinkID());
			}
		}
	}
}