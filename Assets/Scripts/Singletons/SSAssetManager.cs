using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using PygmyMonkey.ColorPalette;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class SSAssetManager : MonoBehaviour
{
    #region Singleton

    public static SSAssetManager AssetManagerInstance { get; private set; }

    public void Awake()
    {
        if (!AssetManagerInstance)
        {
            AssetManagerInstance = this;
        }
        else
        {
            if (AssetManagerInstance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion Singleton

    #region INSPECTOR VARIABLES

    [SerializeField] SOShip activeShipSO;
    [SerializeField] SUInventoryPanel inventoryPanel;
    [SerializeField] SUFittingPanel fittingPanel;
    [SerializeField] SSDataManager dataManagerInstance;
    [SerializeField] SSGameShipManager shipLoadOutInstance; //Set in Scene
    [SerializeField] SXActiveShipContainer activeShipContainer;

    /*  -- Accessors -- */
    public SUInventoryPanel InventoryPanel { get { return inventoryPanel; } }
    public SSDataManager DataManagerInstance { get { return dataManagerInstance; } }
    public SSGameShipManager ShipLoadOutInstance { get { return shipLoadOutInstance; } }

    #endregion INSPECTOR VARIABLES

    //private void Start()
    //{
    //    if (!dataManagerInstance)
    //        dataManagerInstance = SSDataManager.DataManagerInstance;

    //    if (!dataManagerInstance.AssetManagerInstance)
    //    {
    //        dataManagerInstance.AssetManagerInstance = this;
    //    }

    //    if (!shipLoadOutInstance)
    //        shipLoadOutInstance = SSShipLoadOut.ShipLoadOutInstance;

    //    if (!shipLoadOutInstance.AssetManagerInstance)
    //        shipLoadOutInstance.AssetManagerInstance = this;

    //    Debug.Log("TestStart");
    //    VerifySceneInfo();
    //}


    //public void VerifySceneInfo()
    //{
    //    Scene scene = SceneManager.GetActiveScene();
    //    int sceneInt = scene.buildIndex;
    //    Debug.Log(sceneInt);

    //    if(sceneInt == 1 || dataManagerInstance.IsTesting == true)
    //    {
    //        LoadStationUIComponents();
    //    }

    //    else
    //    {
    //        //Initialize UI elements for Space
    //    }
    //}
    //public void LoadStationUIComponents()
    //{
    //    activeShipContainer = FindObjectOfType<SXActiveShipContainer>();
    //    LoadInventoryPanel();
    //    LoadFittingPanel();
    //    inventoryPanel.AddEventListeners();
    //    LoadActiveShipPrefab();
        
    //}
    //public void LoadInventoryPanel()
    //{
    //    inventoryPanel.DataInstance = dataManagerInstance;
    //    inventoryPanel.AddEventListeners();
    //    inventoryPanel.SetPlayerModuleSlots();
    //}

    //public void LoadFittingPanel()
    //{
    //    shipLoadOutInstance.InitializeStationShipLoadOut();


    //}
    //public void LoadActiveShipPrefab()
    //{
    //    for (int i = 0; i < dataManagerInstance.PlayerShipSOs.Count; i++)
    //    {
    //        if (dataManagerInstance.PlayerShipSOs[i].ItemCurrentSlot == SECurrentSlot.ActiveShip)
    //        {
    //            activeShipSO = dataManagerInstance.PlayerShipSOs[i];
    //            GameObject shipPrefab;
    //            shipPrefab = Instantiate(dataManagerInstance.PlayerShipSOs[i].itemPrefab, activeShipContainer.transform);
    //            shipPrefab.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    //            shipPrefab.transform.localPosition = new Vector3(0f, 0f, 0f);
    //            SXPlayerShip activePlayerShip = shipPrefab.GetComponent<SXPlayerShip>();

    //            if (CheckCurrentScene() == 1 || dataManagerInstance.IsTesting == true)
    //            {
    //                activePlayerShip.IsDocked = true;
    //            }
    //            else
    //                activePlayerShip.IsDocked = false;


    //            shipLoadOutInstance.playerShipSO = activeShipSO;
    //            shipLoadOutInstance.playerShip = activePlayerShip;
    //        }
    //    }
    //}

    //public int CheckCurrentScene()
    //{
    //    Scene scene = SceneManager.GetActiveScene();
    //    int sceneInt = scene.buildIndex;
    //    return sceneInt;
    //}

    //public void Undock()
    //{
    //    SceneManager.LoadScene(2);

        
        
    //}

    //public void Dock()
    //{
    //    SceneManager.LoadScene(1);
    //}

    


}
