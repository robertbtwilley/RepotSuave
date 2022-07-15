
using UnityEngine;
using SNCore;

/* ===== STATION UI SCENE  ===== */
/* =>  FITTING PARENT CANVAS  <= */

public class SUFittingPanel : MonoBehaviour
{

    #region INSPECTOR VARIABLES

    public SUFittingSlot[] fittingSlots;
    [Space]
    [SerializeField] SUInventoryPanel inventory;
    [SerializeField] SUInfoPopup infoPopup;
    [SerializeField] SSGameShipManager fitGameShipManager;
    [SerializeField] SSDataManager fitDataInstance;

    /* -- ACCESSORS --*/

    public SSGameShipManager FitGameShipManager
    {
        get { return fitGameShipManager; }
        set { fitGameShipManager = value; }
    }

    public SSDataManager FitDataInstance
    {
        get { return fitDataInstance; }
        set { fitDataInstance = value; }
    }

    public SUInfoPopup FitInfoPopup
    {
        get { return infoPopup; }
        set { infoPopup = value; }
    }

    public SUInventoryPanel FitInventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    #endregion INSPECTOR VARIABLES

    #region STATION SCENE INITIALIZATION

    public void AddAllListeners()
    {
        foreach (SUFittingSlot fitSlot in fittingSlots)
        {
            fitSlot.OnFitSlotClickEvent += LoadToInfoPopup;
            fitSlot.FitSlotOverlay.OnFitSlotOverlayClickEvent += FitModule;
        }
    }

    #endregion STATION SCENE INITIALIZATION

    #region SHIP FITTING METHODS

    public void LoadToInfoPopup(SOModule module, SUFittingSlot slot)
    {
        infoPopup.InfoModuleSO = module;
        infoPopup.gameObject.SetActive(true);
        infoPopup.FittingToInfoPopup(module, slot);
    }

    public void ActivateSelectedSlotOverlay(SOModule module, bool toggle)
    {
        foreach (SUFittingSlot slot in fittingSlots)
        {
            for (int i = 0; i < slot.FittingAcceptableModuleTypes.Length; i++)
            {
                if (module.ModuleSOType == slot.FittingAcceptableModuleTypes[i])
                {
                    slot.ActivateSlotOverlay(module, toggle);
                }
            }
        }
    }

    public void FitModule(SOModule module, SUFittingSlot slot)
    {
        Debug.Log("Testers");
        if (slot.FitModuleSO != null)
        {
            SOModule previousModule;
            previousModule = slot.FitModuleSO;
            inventory.AddItemToInventory(previousModule);
        }

        slot.FitModuleSO = module;
        fitGameShipManager.AddModuleToShip(module, slot);
        FitInfoPopup.ClosePopup();
        ActivateSelectedSlotOverlay(module, false);
        inventory.RemoveModuleFromInventory(module);

    }

    public void UnFitModuleToInventory(SOModule module, SUFittingSlot fitSlot)
    {
        inventory.AddItemToInventory(module);
        fitSlot.FitModuleSO = null;
        fitGameShipManager.RemoveModuleFromShip(module, fitSlot);
        FitInfoPopup.ClosePopup();
        
    }

    #endregion SHIP FITTING METHODS

}
