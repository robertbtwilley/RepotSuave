
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SNCore;

/* ===== STATION UI SCENE  ===== */
/* => INFOPOPUP PARENT CANVAS <= */

public class SUInfoPopup : MonoBehaviour
{

    #region INSPECTOR VARIABLES

    [SerializeField] Image moduleIcon;
    [SerializeField] TextMeshProUGUI unfitButtonText;
    [SerializeField] GameObject unFitButton;
    [SerializeField] SUInventoryPanel infoModulePanel;
    [SerializeField] SUFittingPanel infoFittingPanel;
    [SerializeField] SOModule infoModuleSO;
    [SerializeField] SUFittingSlot unfitSlot;

    public SOModule InfoModuleSO
    {
        get { return infoModuleSO; }
        set
        {
            infoModuleSO = value;

            if (value == null)
            {
                moduleIcon.sprite = null;
                moduleIcon.enabled = false;
            }
            else
            {
                moduleIcon.sprite = infoModuleSO.itemIcon;
                moduleIcon.enabled = true;
            }
        }
    }

    #endregion INSPECTOR VARIABLES

    #region INFOPOPUP STATION METHODS

    public void InventoryToInfoPopup(SOModule module)
    {
        infoModuleSO = module;
        moduleIcon.sprite = infoModuleSO.itemIcon;
        unFitButton.gameObject.SetActive(false);
        unfitButtonText.enabled = false;

        infoFittingPanel.ActivateSelectedSlotOverlay(module, true);
    }
    public void FittingToInfoPopup(SOModule module, SUFittingSlot slot)
    {
        infoModuleSO = module;
        moduleIcon.sprite = infoModuleSO.itemIcon;
        unfitSlot = slot;
        unFitButton.gameObject.SetActive(true);
        unfitButtonText.enabled = true;
        infoFittingPanel.ActivateSelectedSlotOverlay(module, false);
    }

    public void UnFitButton()
    {
        infoFittingPanel.UnFitModuleToInventory(infoModuleSO, unfitSlot);
        ClosePopup();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        infoFittingPanel.ActivateSelectedSlotOverlay(infoModuleSO, false);
    }

    #endregion INFOPOPUP STATION METHODS


}
