using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using SNCore;
using UnityEngine.Pool;

public class SSDataManager : MonoBehaviour
{
    public bool IsTesting;
    [SerializeField] private const string playerItemDataString = "Assets/data/playerstuff.txt";
    [SerializeField] private const string gameWeaponDataString = "Assets/data/gameweapons.txt";
    [SerializeField] private const string gameShipDataString = "Assets/data/gameships.txt";
    

    //public GameObject GameDataPersistentObject; //This is the persistent (session) singleton game object in the load data scene
    //[SerializeField] SSAssetManager assetManagerInstance;

    //public SSAssetManager AssetManagerInstance
    //{
    //    get { return assetManagerInstance; }
    //    set { assetManagerInstance = value; }
    //}

    #region SINGLETON

    public static SSDataManager DataManagerInstance { get; private set;}

    public void Awake()
    {
        if(!DataManagerInstance)
        {
            DataManagerInstance = this;
        }
        else
        {
            if(DataManagerInstance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] SSGameShipManager gameShipManagerInstance;
    public SSGameShipManager GameShipManagerInstance
    {
        get { return gameShipManagerInstance; }
        set { gameShipManagerInstance = value; }
    }

    #endregion SINGLETON

    public List<SDPlayerManifestItemData> PlayerItems = new List<SDPlayerManifestItemData>();
    public List<SDGameWeaponData> GameWeaponData = new List<SDGameWeaponData>();
    public List<SDGameShipData> GameShipData = new List<SDGameShipData>();
    public SOItemDatabase GameItemDB;
    public List<SOModule> PlayerModuleSOs = new List<SOModule>();
    public List<SOShip> PlayerShipSOs = new List<SOShip>();
    public List<int> currentIntSlotCheck = new List<int>();
    public List<SECurrentSlot> currentSlotsCheck = new List<SECurrentSlot>();

    
    
    private void Start()
    {
       
        LoadShipData();
        LoadWeaponData();
        LoadPlayerItemData();
        PopulateShipDataPoints();
        PopulateWeaponDataPoints();
        InstantiatePlayerInventoryModuleSOs();
        InstantiatePlayerShipSOs();
        if (IsTesting == false)
        {
            StartCoroutine(LoadMainUI());
        }
        else
            LoadTestingSceneComponents();
    }


    public void LoadTestingSceneComponents()
    {
        
    }

    public IEnumerator LoadMainUI()  //Once we begin saving data, this will need to load/save the current scene
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
        //gameShipManagerInstance = SSGameShipManager.GameShipManagerInstance;
        ////InitializeActiveShipSO();
        //gameShipManagerInstance.SetActiveShipSlot();
    }

    public void LoadShipData()
    {
        string path = gameShipDataString;
        string jsonShipObjectDataString = File.ReadAllText(path);
        GameShipData = JsonConvert.DeserializeObject<List<SDGameShipData>>(jsonShipObjectDataString);
        Debug.Log("Game Ship Data Load Complete");

        SaveShipData();
        Debug.Log("Initial Game Ship Data load / save complete");
    }

    public void LoadWeaponData()
    {
        string path = gameWeaponDataString;
        string jsonWeaponObjectDataString = File.ReadAllText(path);
        GameWeaponData = JsonConvert.DeserializeObject<List<SDGameWeaponData>>(jsonWeaponObjectDataString);
        Debug.Log("Game Weapon Data Load Complete");

        SaveWeaponData();
        Debug.Log("Initial Game Weapon Data load / save complete");

    }

    public void SaveShipData()
    {
        string jsonWritesShipDataStr = JsonConvert.SerializeObject(GameShipData, Formatting.Indented);
        File.WriteAllText(gameShipDataString, jsonWritesShipDataStr);
    }

    public void SaveWeaponData()
    {
        string jsonWritesWeaponDataStr = JsonConvert.SerializeObject(GameWeaponData, Formatting.Indented);
        File.WriteAllText(gameWeaponDataString, jsonWritesWeaponDataStr);
    }

    public void LoadPlayerItemData()
    {
        string path = playerItemDataString;
        string jsonPlayerItemObjectDataString = File.ReadAllText(path);
        PlayerItems = JsonConvert.DeserializeObject<List<SDPlayerManifestItemData>>(jsonPlayerItemObjectDataString);
        Debug.Log("Player Item Data Load Complete");

        // Need an Authentication Function here to check data before we overwrite the previous Player Data File

        SavePlayerItemData();
        Debug.Log("Initial Load / Save complete");
    }

    public void SavePlayerItemData()
    {
        string jsonWritePlayerItemsDataString = JsonConvert.SerializeObject(PlayerItems, Formatting.Indented);
        File.WriteAllText(playerItemDataString, jsonWritePlayerItemsDataString);
    }

    [System.Serializable]
    public class SDPlayerManifestItemData
    {
        public string PlayerItemID;
        public string PlayerItemDisplayName;
        public int PlayerItemLevel;
        public int PlayerItemProgress;
        public int PlayerItemSlotID;
    }

    [System.Serializable]
    public class SDGameWeaponData
    {
        public string GameWeaponID;
        public string GameWeaponDisplayName;
        public float GameWeaponRange;
        public float GameWeaponDamage;
        public int GameWeaponCapacity;
        public float GameWeaponAimSpeed;
        public float GameWeaponFireRate;
        public float GameWeaponFireDelay;
        public int GameWeaponBurstVolume;
    }

    [System.Serializable]
    public class SDGameShipData
    {
        public string GameShipID;
        public string GameShipDisplayName;
        public float GameShipTargetingRange;
        public float GameShipMaxSpeed;
        public float GameShipTurnSpeed;
        public float GameShipTacticalShieldAmount;
        public float GameShipIndustrialShieldAmount;
        public float GameShipOperationsShieldAmount;
        public float GameShipEngineeringShieldAmount;
        public float GameShipTacticalHullAmount;
        public float GameShipIndustrialHullAmount;
        public float GameShipOperationsHullAmount;
        public float GameShipEngineeringHullAmount;
        public int GameShipTacticalSlots;
        public int GameShipIndustrialSlots;
        public int GameShipOperationsSlots;
        public int GameShipEngineeringSlots;
    }

    #region SESSION DATA INITIALIZATION

    public void PopulateWeaponDataPoints()
    {
        for (int i = 0; i < GameItemDB.AllSOWeapons.Length; i++)
        {
            GameItemDB.AllSOWeapons[i].DisplayName = GameWeaponData[i].GameWeaponDisplayName;
            //The ID is just in the Data file for reference - but this might be used for validation later
            GameItemDB.AllSOWeapons[i].BaseRange = GameWeaponData[i].GameWeaponRange;
            GameItemDB.AllSOWeapons[i].BaseRange = GameWeaponData[i].GameWeaponRange;
            GameItemDB.AllSOWeapons[i].BaseDamage = GameWeaponData[i].GameWeaponDamage;
            GameItemDB.AllSOWeapons[i].BaseCapacity = GameWeaponData[i].GameWeaponCapacity;
            GameItemDB.AllSOWeapons[i].BaseAimSpeed = GameWeaponData[i].GameWeaponAimSpeed;
            GameItemDB.AllSOWeapons[i].BaseFireRate = GameWeaponData[i].GameWeaponFireRate;
            GameItemDB.AllSOWeapons[i].FireDelay = GameWeaponData[i].GameWeaponFireDelay;
            GameItemDB.AllSOWeapons[i].BurstVolume = GameWeaponData[i].GameWeaponBurstVolume;
        }
    }

    public void PopulateShipDataPoints()
    {
        for (int i = 0; i < GameItemDB.AllSOShips.Length; i++)
        {
            GameItemDB.AllSOShips[i].DisplayName = GameShipData[i].GameShipDisplayName;
            //The ID is just in the Data file for reference - but this might be used for validation later
            GameItemDB.AllSOShips[i].TargetingRange = GameShipData[i].GameShipTargetingRange;
            GameItemDB.AllSOShips[i].ShipMaxSpeed = GameShipData[i].GameShipMaxSpeed;
            GameItemDB.AllSOShips[i].ShipTurnSpeed = GameShipData[i].GameShipTurnSpeed; ;
            GameItemDB.AllSOShips[i].TacticalShieldAmount = GameShipData[i].GameShipTacticalShieldAmount;
            GameItemDB.AllSOShips[i].IndustrialShieldAmount = GameShipData[i].GameShipIndustrialShieldAmount;
            GameItemDB.AllSOShips[i].OperationsShieldAmount = GameShipData[i].GameShipOperationsShieldAmount;
            GameItemDB.AllSOShips[i].EngineeringShieldAmount = GameShipData[i].GameShipEngineeringShieldAmount;
            GameItemDB.AllSOShips[i].TacticalHullAmount = GameShipData[i].GameShipTacticalHullAmount;
            GameItemDB.AllSOShips[i].IndustrialHullAmount = GameShipData[i].GameShipIndustrialHullAmount;
            GameItemDB.AllSOShips[i].OperationsHullAmount = GameShipData[i].GameShipOperationsHullAmount;
            GameItemDB.AllSOShips[i].EngineeringHullAmount = GameShipData[i].GameShipEngineeringHullAmount;
            GameItemDB.AllSOShips[i].TacticalSlots = GameShipData[i].GameShipTacticalSlots;
            GameItemDB.AllSOShips[i].IndustrialSlots = GameShipData[i].GameShipIndustrialSlots;
            GameItemDB.AllSOShips[i].OperationsSlots = GameShipData[i].GameShipOperationsSlots;
            GameItemDB.AllSOShips[i].EngineeringSlots = GameShipData[i].GameShipEngineeringSlots;

        }
    }

    public void InstantiatePlayerInventoryModuleSOs()
    {
        foreach(SDPlayerManifestItemData item in PlayerItems)
        {
            for(int i = 0; i < GameItemDB.AllSOModules.Length; i++)
            {
                if(GameItemDB.AllSOModules[i].ID == item.PlayerItemID)
                {
                    SOModule temp = Instantiate(GameItemDB.AllSOModules[i]);
                    PlayerModuleSOs.Add(temp);
                    temp.IsInstantiated = true;
                    temp.DisplayName = GameItemDB.AllSOModules[i].DisplayName;
                    temp.ItemLevel = item.PlayerItemLevel;
                    temp.ItemProgress = item.PlayerItemProgress;
                    temp.ItemCurrentSlot = (SECurrentSlot)item.PlayerItemSlotID;
                    currentSlotsCheck.Add((SECurrentSlot)item.PlayerItemSlotID);
                    currentIntSlotCheck.Add(item.PlayerItemSlotID);
                }

            }
        }
    }

    public void InstantiatePlayerShipSOs()
    {
        foreach (SDPlayerManifestItemData item in PlayerItems)
        {
            for (int i = 0; i < GameItemDB.AllSOShips.Length; i++)
            {
                if (GameItemDB.AllSOShips[i].ID == item.PlayerItemID)
                {
                    SOShip temp = Instantiate(GameItemDB.AllSOShips[i]);
                    PlayerShipSOs.Add(temp);
                    temp.IsInstantiated = true;
                    temp.DisplayName = GameItemDB.AllSOShips[i].DisplayName;
                    temp.ItemLevel = item.PlayerItemLevel;
                    temp.ItemProgress = item.PlayerItemProgress;
                    temp.ItemCurrentSlot = (SECurrentSlot)item.PlayerItemSlotID;
                }

            }
        }
    }

    #endregion SESSION DATA INITIALIZATION

    #region SESSION GSM INITIALIZATION

    public void InitializeActiveShipSO() //Find the ship object that is current the active ship
    {
        for (int i = 0; i < PlayerShipSOs.Count; i++)
        {
            if (PlayerShipSOs[i].ItemCurrentSlot == SECurrentSlot.ActiveShip)
            {
                Debug.Log(PlayerShipSOs[i]);
                //gameShipManagerInstance.GSMActiveShipSO = PlayerShipSOs[i];
                gameShipManagerInstance.InitializeActiveShipFromDataManager(PlayerShipSOs[i]);
                //gameShipManagerInstance.InitializeAndSetActivePlayerShip(PlayerShipSOs[i]);
                //GameObject shipPrefab;
                //shipPrefab = Instantiate(dataManagerInstance.PlayerShipSOs[i].itemPrefab, activeShipContainer.transform);
                //shipPrefab.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                //shipPrefab.transform.localPosition = new Vector3(0f, 0f, 0f);
                //SXPlayerShip activePlayerShip = shipPrefab.GetComponent<SXPlayerShip>();

                //if (CheckCurrentScene() == 1 || dataManagerInstance.IsTesting == true)
                //{
                //    activePlayerShip.IsDocked = true;
                //}
                //else
                //    activePlayerShip.IsDocked = false;


                //shipLoadOutInstance.playerShipSO = activeShipSO;
                //shipLoadOutInstance.playerShip = activePlayerShip;
            }
        }
    }

    #endregion SESSION GSM INITIALIZATION

}
