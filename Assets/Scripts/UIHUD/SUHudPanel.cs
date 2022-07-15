using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SNCore;

public class SUHudPanel : MonoBehaviour
{
    [SerializeField] SXPlayerHUD thisPlayerHUD;
    public SEHullPoint HudPointType;
    public SUModuleSlotParent[] ModuleParents;
    public Transform BoosterParentT;
    public List<SUHudSlot> HudSlots = new List<SUHudSlot>();
    public SXPlayerHUD ThisPlayerHUD
    {
        get { return thisPlayerHUD; }
        set { thisPlayerHUD = value; }
    }


    public void InitializeHudPanelHudSlots()
    {
        
    }

    public GameObject InstantiateHudSlot(Transform moduleParent)
    {
        GameObject tempSlot;
        tempSlot = Instantiate(thisPlayerHUD.hudslotPrefab, moduleParent);
        tempSlot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        tempSlot.transform.localPosition = new Vector3(0f, 0f, 0f);
        return tempSlot;
        
    }
}
