using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SX_UI_Menu_Section : MonoBehaviour
{

    [SerializeField] SX_UI_Menu_Controller menuController;
    [SerializeField] SX_UI_Menu_Item[] menuItems;
    protected Color activeOutlineColor = new Color(0.227451f, 0.9137255f, 0.9568627f, 1.0f);
    protected Color activeColor = new Color(0.227451f, 0.9137255f, 0.9568627f, 0.5f);
    protected Color normalOutlineColor = Color.white;
    protected Color normalColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    public void RefreshMenuSection()
    {

        /* -- Case where Active Section is MISSIONS -- */

        if (menuController.ActiveSection == menuController.DefaultSection && menuController.ActiveSection != this)
        {
            for(int i = 0; i < menuItems.Length;  i ++)
            {
                if(i < 1)
                {
                    menuItems[i].transform.gameObject.SetActive(true);
                    menuItems[i].MenuItemOutline.color = normalOutlineColor;
                    menuItems[i].MenuItemPanel.color = normalColor;
                }
                else
                    menuItems[i].transform.gameObject.SetActive(false);
            }
        }

        else if (menuController.ActiveSection != menuController.DefaultSection && menuController.ActiveSection != this)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                //Only the Default Section uses the first array element, and only an Active assets menu utilizes elements 5 and 6.
                if (i > 0 && i < 4 && menuItems[i].SectionToEnable != menuController.ActiveSection)
                {
                    menuItems[i].transform.gameObject.SetActive(true);
                }
                else
                    menuItems[i].transform.gameObject.SetActive(false);
            }
        }

        else 
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                if(i > 0 && i < 4 && menuItems[i].SectionToEnable == menuController.ActiveSection )
                {
                    menuItems[i].transform.gameObject.SetActive(false);
                }
                else
                {
                    menuItems[i].transform.gameObject.SetActive(true);
                    if (i < 1)
                    {
                        menuItems[i].MenuItemOutline.color = activeOutlineColor;
                        menuItems[i].MenuItemPanel.color = activeColor;
                    }
                    if (i > 0 && i < 4)
                    {
                        menuItems[i].MenuItemOutline.color = normalOutlineColor;
                        menuItems[i].MenuItemPanel.color = normalColor;
                    }
                }
                    
            }
        }






        

    }
    
}
