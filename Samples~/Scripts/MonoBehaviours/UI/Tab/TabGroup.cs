using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lnfu
{
    public class TabGroup : MonoBehaviour
    {
		public List<TabButton> tabButtons;
		public List<GameObject> targets;
		public TabButton selected;

		public void Start()
		{
			for (int i = 0; i < targets.Count; ++i)
			{
				targets[i].SetActive(false);
			}
		}

		public void Subscribe(TabButton button)
		{
			if (tabButtons == null)
			{
				tabButtons = new List<TabButton>();
			}

			tabButtons.Add(button);
		}

		public void ResetTabs()
		{
			foreach (TabButton button in tabButtons)
			{
				if (selected != null && selected == button)
				{
					continue;
				}
				Color temp = button.background.color;
				temp.a = 0.2f;
				button.background.color = temp;
			}
		}

		// 滑鼠游標移動到 button 上面的時候
		public void OnTabEnter(TabButton button)
		{
			ResetTabs();
			if (selected == null || selected != button)
			{
				Color temp = button.background.color;
				temp.a = 0.5f;
				button.background.color = temp;
			}
		}

		// 點擊選取
		public void OnTabSelected(TabButton button)
		{
			selected = button;
			ResetTabs();
			Color temp = button.background.color;
			temp.a = 1f;
			button.background.color = temp;

			int index = button.transform.GetSiblingIndex();
			for (int i = 0; i < targets.Count; ++i)
			{
				if (i == index)
				{
					targets[i].SetActive(true);
				}
				else
				{
					targets[i].SetActive(false);
				}
			}
		}

		// 滑鼠游標離開的時候
		public void OnTabExit(TabButton button)
		{
			ResetTabs();
		}

    }
}
