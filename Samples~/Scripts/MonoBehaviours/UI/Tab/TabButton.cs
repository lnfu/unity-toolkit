using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // event system (enter, click, exit handler)
using UnityEngine.UI;

namespace lnfu
{
	[RequireComponent(typeof(Image))] // 確保有 Image component
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
		public TabGroup tabGroup;

		public Image background;

        void Start()
        {
			background = GetComponent<Image>();
			tabGroup.Subscribe(this);
        }

		public void OnPointerEnter(PointerEventData eventData)
		{
			tabGroup.OnTabEnter(this);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			tabGroup.OnTabSelected(this);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			tabGroup.OnTabExit(this);
		}

    }
}
