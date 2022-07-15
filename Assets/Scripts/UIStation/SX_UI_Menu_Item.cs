using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SX_UI_Menu_Item : MonoBehaviour
{
    public Image MenuItemOutline;
    public Image MenuItemPanel;
    [SerializeField] SX_UI_Menu_Controller menuController;
    public SX_UI_Menu_Section SectionToEnable;

    public void MenuItemButtonClick()
    {
        if(menuController.ActiveSection != SectionToEnable)
        {
            menuController.ToggleActiveSection(SectionToEnable);
        }
    }
}


