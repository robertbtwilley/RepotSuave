using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SNCore;


public class SUStationManager : MonoBehaviour
{
    [SerializeField] SSDataManager dataManagerInstance;
    [SerializeField] SSGameShipManager gameShipManagerInstance;
    [SerializeField] SUFittingPanel fittingPanel;
    [SerializeField] SUInventoryPanel inventoryPanel;
    [SerializeField] SUInfoPopup infoPopup;
    [SerializeField] SXActiveShipContainer activeShipContainer;

    private void Start()
    {
        if (!dataManagerInstance)
            dataManagerInstance = SSDataManager.DataManagerInstance;
        if (!gameShipManagerInstance)
            gameShipManagerInstance = SSGameShipManager.GameShipManagerInstance;

        if (!dataManagerInstance.GameShipManagerInstance)
            dataManagerInstance.GameShipManagerInstance = gameShipManagerInstance;
        if (!gameShipManagerInstance.DataManagerInstance)
            gameShipManagerInstance.DataManagerInstance = dataManagerInstance;

        gameShipManagerInstance.GSMStationManager = this;
        gameShipManagerInstance.GSMInventoryPanel = inventoryPanel;
        gameShipManagerInstance.GSMFittingPanel = fittingPanel;
        inventoryPanel.DataInstance = dataManagerInstance;
        inventoryPanel.GameShipManager = gameShipManagerInstance;

        Debug.Log("TestStart");
        LoadStationUIComponents();
    }

    public void LoadStationUIComponents()
    {
        gameShipManagerInstance.GSMActiveShipParent = FindObjectOfType<SXActiveShipContainer>();
        inventoryPanel.GameShipManager = gameShipManagerInstance;
        inventoryPanel.DataInstance = dataManagerInstance;

        fittingPanel.FitGameShipManager = gameShipManagerInstance;
        fittingPanel.FitDataInstance = dataManagerInstance;

        inventoryPanel.SetPlayerModuleSlots();
        inventoryPanel.AddEventListeners();

        fittingPanel.AddAllListeners();
        gameShipManagerInstance.SetActiveShipSlot();
    }


    }
