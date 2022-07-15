using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SNCore;

public class SUHudSlot : MonoBehaviour
{
    [SerializeField] SOModule hudModuleSO;
    public Image HudModuleIcon;
    public Image HudEmbptyIcon;
    public SEHardPointLocation HudModuleLocation;
    public SEModuleType HudModuleType;


    public SOModule HudModuleSO
    {
        get { return hudModuleSO; }
        set
        {
            hudModuleSO = value;

            if (value == null)
            {
                HudModuleIcon.sprite = null;
                HudModuleIcon.enabled = false;
                HudEmbptyIcon.enabled = true;
            }
            else
            {
                HudModuleIcon.sprite = hudModuleSO.itemIcon;
                HudModuleIcon.enabled = true;
                HudEmbptyIcon.enabled = false;
            }
        }
    }


}
