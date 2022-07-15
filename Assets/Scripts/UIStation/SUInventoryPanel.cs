
using System.Collections.Generic;
using UnityEngine;
using SNCore;


/* ===== STATION UI SCENE  ===== */
/* => INVENTORY PARENT CANVAS <= */

public class SUInventoryPanel : MonoBehaviour
{
    #region INSPECTOR VARIABLES

    [SerializeField] List<SUModuleSlot> moduleSlots;
    [SerializeField] RectTransform moduleInventoryParent;
    [SerializeField] GameObject moduleSlotPrefab;
    [Space]
    [SerializeField] SUFittingPanel invFitPanel;
    [SerializeField] SUInfoPopup invInfoPopup;
    [Space]
    [SerializeField] SSGameShipManager gameShipManager;
    [SerializeField] SSDataManager dataInstance;

    /* ---- ACCESSORS ---- */

    public SUFittingPanel InvFitPanel
    {
        get { return invFitPanel; }
        set { invFitPanel = value; }
    }

    public SUInfoPopup InvInfoPopup
    {
        get { return invInfoPopup; }
        set { invInfoPopup = value; }
    }

    public SSDataManager DataInstance
    {
        get { return dataInstance; }
        set { dataInstance = value; }
    }

    public SSGameShipManager GameShipManager
    {
        get { return gameShipManager; }
        set { gameShipManager = value; }
    }

    #endregion INSPECTOR VARIALBES

    #region STATION SCENE INITIALIZATION

    public void SetPlayerModuleSlots()
    {
        for (int i = 0; i < dataInstance.PlayerModuleSOs.Count; i++)
        {
            if (dataInstance.PlayerModuleSOs[i].ItemCurrentSlot == SECurrentSlot.Hangar)
            {
                GameObject tempSlot;
                tempSlot = Instantiate(moduleSlotPrefab, moduleInventoryParent.transform);
                SUModuleSlot tempModSlot;
                tempModSlot = tempSlot.GetComponent<SUModuleSlot>();
                tempModSlot.InventoryPanel = this;
                moduleSlots.Add(tempModSlot);
                tempModSlot.ModuleSO = dataInstance.PlayerModuleSOs[i];
            }
        }
    }

    public void AddEventListeners()
    {
        for (int i = 0; i < moduleSlots.Count; i++)
        {
            moduleSlots[i].OnModuleSlotClickEvent += FitToPopup;
        }
    }

    #endregion STATION SCENE INITIALIZATION

    #region INVENTORY STATION METHODS

    public void AddItemToInventory(SOModule module)
    {
        GameObject tempSlot;
        tempSlot = Instantiate(moduleSlotPrefab, moduleInventoryParent);
        SUModuleSlot tempModSlot;
        tempModSlot = tempSlot.GetComponent<SUModuleSlot>();
        moduleSlots.Add(tempModSlot);

        for (int i = 0; i < moduleSlots.Count; i++)
        {
            if (moduleSlots[i].ModuleSO == null)
            {
                moduleSlots[i].ModuleSO = module;
                moduleSlots[i].OnModuleSlotClickEvent += FitToPopup;
            }
        }
    }

    public void RemoveModuleFromInventory(SOModule module)
    {
        for (int i = 0; i < moduleSlots.Count; i++)
        {
            if (moduleSlots[i].ModuleSO == module)
            {
                moduleSlots[i].ModuleSO = null;

                Destroy(moduleSlots[i].gameObject);
                moduleSlots.Remove(moduleSlots[i]);
            }
        }
    }

    public void FitToPopup(SOModule module)
    {
        invInfoPopup.gameObject.SetActive(true);
        invInfoPopup.InventoryToInfoPopup(module);
        ActivateFittingSlotOverlays(module);
    }

    public void ActivateFittingSlotOverlays(SOModule module)
    {
        invFitPanel.ActivateSelectedSlotOverlay(module, true);
    }

    #endregion INVENTORY STATION METHODS

}
