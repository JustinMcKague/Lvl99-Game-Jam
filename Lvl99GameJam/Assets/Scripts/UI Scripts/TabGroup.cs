using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    [HideInInspector]
    public TabButton selectedTab;

    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(tabButton);
    }

    //public void OnTabEnter(TabButton tabButton)
    //{
    //    ResetTabs();
    //    if (selectedTab == null || tabButton != selectedTab)
    //    {
    //        tabButton.background.color = tabRef.;
    //    }
    //}

    //public void OnTabExit(TabButton tabButton)
    //{
    //    ResetTabs();
    //    if (selectedTab == null || tabButton != selectedTab)
    //    {
    //        tabButton.background.color = tabIdle;
    //    }
    //}

    public void OnTabSelected(Tab tab)
    {
        selectedTab = tab.gameObject.GetComponent<TabButton>();
        ResetTabs();
        for (int i = 0; i < tab.graphics.Count; i++)
        {
            tab.graphics[i].color = tab.activeColors[i];
        }
        int index = tab.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            for (int i = 0; i < button.gameObject.GetComponent<Tab>().graphics.Count; i++)
            {
                button.gameObject.GetComponent<Tab>().graphics[i].color = button.gameObject.GetComponent<Tab>().inactiveColors[i];
            }
        }
    }

    public void ReOpenTabGroup()
    {
        selectedTab = tabButtons[2];
        foreach (TabButton button in tabButtons)
        {
            if (button == selectedTab)
            {
                for (int i = 0; i < button.gameObject.GetComponent<Tab>().graphics.Count; i++)
                {
                    button.gameObject.GetComponent<Tab>().graphics[i].color = button.gameObject.GetComponent<Tab>().activeColors[i];
                }
                objectsToSwap[0].SetActive(true);
            }
            else
            {
                for (int i = 0; i < button.gameObject.GetComponent<Tab>().graphics.Count; i++)
                {
                    button.gameObject.GetComponent<Tab>().graphics[i].color = button.gameObject.GetComponent<Tab>().inactiveColors[i];
                }
                for (int i = 0; i < objectsToSwap.Count; i++)
                {
                    objectsToSwap[i].SetActive(false);
                }
            }
        }
    }
}