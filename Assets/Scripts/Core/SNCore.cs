using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace SNCore
{
    #region Enums

    public enum SEItemType { Ship, Module, CrewMate, Ore, Drone }
    public enum SEModuleType { None, FixedTurret, TorpedoTube, PointDefenseTurret, MainTurret, FixedMiner, MainMiner, TacticalBooster, ReactorBooster, IndustrialBooster, ShieldBooster, MovementBooster } //list weapon mods first
    //public enum SEWeaponType { NA, Turret, Launcher }
    //public enum SETurretType { NA, FixedTurret, PointDefenseTurret, MainTurret, FixedMiner, MainMiner }

    public enum SEHardPointLocation { None, Abstract, Lower_Bow, LowerFWD_SBPT, Bow, Forward, ForwardSB, ForwardPT, ForwardSBPT, Deck, DeckSB, DeckPT, DeckSBPT, Flight, Aft, AftSB, AftPT, AftSBPT, Stern }
    //public enum SEHardPointType { FixedTurretPoint, PointDefenseTurretGroup, MainTurretPoint, FixedMinerPoint, MainMinerPoint, TacticalBoosterPoint, ReactorDrivePoint, IndustrialBoosterPoint }

    public enum SEHullPoint { none, Tactical, Industrial, Operations, Engineering}
    public enum SEBoosterType { none, Shield, Tactical, Movement, Reactor, Industrial}

    public enum SEShipType { NA, Combat, Industrial, Capital, Transport }
    public enum SEShipClass { NA, Frigate, Destroyer, Cruiser, BattleCruiser, Battleship, Barge, Highliner, Industrial }
    public enum SECurrentSlot { NA, Hangar, ActiveShip, TacticalTop, TacticalMid, TacticalBot, IndustrialTop, IndustrialMid, IndustrialBot, OperationsTop, OperationsMid, OperationsBot, EngineeringTop, EngineeringMid, EngineeringBot}


    #endregion Enums

    #region Abstract Scriptable Objects

    //Base Class for all SOs
    public abstract class SOItem : ScriptableObject 
    {
        public bool IsInstantiated; //just to test
        [SerializeField] SEItemType itemSOType;
        [SerializeField] string id;
        [SerializeField] string displayName;
        [SerializeField] int itemLevel;
        [SerializeField] int itemProgress;
        [SerializeField] SECurrentSlot itemCurrentSlot;

        public Sprite itemIcon;
        public GameObject itemPrefab;

        //Accessors
        public string ID { get { return id; } }
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        public SEItemType ItemSOType
        {
            get { return itemSOType; }
            set { itemSOType = value; }
        }
        public int ItemLevel
        {
            get { return itemLevel; }
            set { itemLevel = value; }
        }
        public int ItemProgress
        {
            get { return itemProgress; }
            set { itemProgress = value; }
        }
        public SECurrentSlot ItemCurrentSlot
        {
            get { return itemCurrentSlot; }
            set { itemCurrentSlot = value; }
        }

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);

        }
    }

    //Base Class for all Equippable Items
    public abstract class SOModule : SOItem
    {
        
        [SerializeField] SEModuleType moduleSOType;

        public SEModuleType ModuleSOType
        {
            get { return moduleSOType; }
            set { moduleSOType = value; }
        }
    }

    //Base Class for Turrets and Launchers
    public abstract class SOWeapon : SOModule
    {
        [SerializeField] float baseRange;
        [SerializeField] float baseDamage;
        [SerializeField] int baseCapacity;
        [SerializeField] float baseAimSpeed;
        [SerializeField] float baseFireRate;
        [SerializeField] float fireDelay;
        [SerializeField] int burstVolume; //Amount of bullets or missiles fired per cycle
        [Space]
        public ParticleSystem FireFX;

        public float BaseRange { get { return baseRange; } set { baseRange = value; } }
        public float BaseDamage { get { return baseDamage; } set { baseDamage = value; } }
        public int BaseCapacity { get { return baseCapacity; } set { baseCapacity = value; } }
        public float BaseAimSpeed { get { return baseAimSpeed; } set { baseAimSpeed = value; } }
        public float BaseFireRate { get { return baseFireRate; } set { baseFireRate = value; } }
        public float FireDelay { get { return fireDelay; } set { fireDelay = value; } }
        public int BurstVolume { get { return burstVolume; } set { burstVolume = value; } }
    }



    #endregion Abstract Scriptable Objects

    #region Interfaces

    public interface IMountable
    {
        void AddMountableModule(SXShip ship, SOModule module, SXMount mount);

        void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount);
        
    }

    #endregion Interfaces

    #region Abstract Ship Points and Systems

    public abstract class SXHardPoint : MonoBehaviour
    {
        //[SerializeField] IEquippable equippableModule;
        [SerializeField] SEHardPointLocation hardpointLocation;
        [SerializeField] SEModuleType hardpointModuleType;
        //[SerializeField] SXShip thisShip;
        //public SXMount[] Mounts;

        //public IEquippable EquippableModule { get { return equippableModule; } set { equippableModule = value; } }
        public SEHardPointLocation HardpointLocation { get { return hardpointLocation; } }
        public SEModuleType HardpointModuleType
        {
            get { return hardpointModuleType; }
            set { hardpointModuleType = value; }
        }
        
        //public SOModule HardPointModuleSORef
        //{
        //    get { return hardpointModuleSORef; }
        //    set { hardpointModuleSORef = value; }
        //}

    }

    public abstract class SMModule: MonoBehaviour
    {
        [SerializeField] SXShip moduleShip;
        public SXShip ModuleShip { get { return moduleShip; } set { moduleShip = value; } }
    }

    public abstract class SMWeapon: SMModule, IMountable
    {
        //SO Attributes
        [SerializeField] float weaponRange;
        [SerializeField] int ammoCapacity;
        [SerializeField] float fireDelay;
        [SerializeField] float aimSpeed;
        [SerializeField] float fireRate;
        [SerializeField] int burstVolume;

        //Setters

        public SXMount WeaponMount;

        public float WeaponRange
        {
            get { return weaponRange; }
            set { weaponRange = value; }
        }
        public int AmmoCapacity
        {
            get { return ammoCapacity; }
            set { ammoCapacity = value; }
        }
        public float FireDelay
        {
            get { return fireDelay; }
            set { fireDelay = value; }
        }
        public float AimSpeed
        {
            get { return aimSpeed; }
            set { aimSpeed = value; }
        }
        public float FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }
        public int BurstVolume
        {
            get { return burstVolume; }
            set { burstVolume = value; }
        }

        

        public void AddMountableModule(SXShip ship, SOModule module, SXMount mount)
        {
            ModuleShip = ship;
            InitializeModuleFromMountable(ModuleShip, module, mount);

        }

        public virtual void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount)
        {

        }


    }
    public abstract class SXMount : MonoBehaviour
    {
        [SerializeField] SEModuleType mountModuleType;
        [SerializeField] SOModule mountModuleSORef; //should be set to null if removed and left empty;
        public Transform MountContainer;
        public GameObject MountedGameObject;

        public SEModuleType MountModuleType
        {
            get { return mountModuleType; }
            set { mountModuleType = value; }
        }
        public SOModule MountModuleSORef
        {
            get { return mountModuleSORef; }
            set { mountModuleSORef = value; }
        }

        public virtual void InitializeMount(SXShip ship, SOModule module) //Stage 1a Initialization
        {
            

            //switch (module.ModuleSOType)
            //{
            //    case SEModuleType.PointDefenseTurret:
            //        SMTurretPDC turretPDCSX = MountedGameObject.GetComponent<SMTurretPDC>();
            //        turretPDCSX.AddMountableModule(ship, module, this);
            //        break;

            //    case SEModuleType.FixedTurret:
            //        SMFixedWeapon fixexedTurretSX = MountedGameObject.GetComponent<SMFixedWeapon>();
            //        fixexedTurretSX.AddMountableModule(ship, module, this);
                    
            //        break;

            //    case SEModuleType.FixedMiner:
            //        SMFixedMiner fixedMinerSX = MountedGameObject.GetComponent<SMFixedMiner>();
            //        fixedMinerSX.AddMountableModule(ship, module, this);
            //        break;

            //    case SEModuleType.IndustrialBooster:
            //        SMStabilizer stabilizerSX = MountedGameObject.GetComponent<SMStabilizer>();
            //        stabilizerSX.AddMountableModule(ship, module, this);
            //        break;
            //}
        }

        public virtual void UnloadMount(SXShip ship, SOModule module) //Stage 1a Initialization
        {
            //switch (module.ModuleSOType)
            //{
                
            //    case SEModuleType.PointDefenseTurret:
            //        SMTurretPDC turretPDCSX = MountedGameObject.GetComponent<SMTurretPDC>();
            //        MountStopAllCoroutines();
            //        turretPDCSX.StopAllCoroutines();
            //        break;

            //    case SEModuleType.FixedTurret:
            //        SMFixedWeapon fixexedTurretSX = MountedGameObject.GetComponent<SMFixedWeapon>();
            //        MountStopAllCoroutines();
            //        fixexedTurretSX.StopAllCoroutines();
            //        break;

            //    case SEModuleType.FixedMiner:
            //        SMFixedMiner fixedMinerSX = MountedGameObject.GetComponent<SMFixedMiner>();
            //        MountStopAllCoroutines();
            //        fixedMinerSX.StopAllCoroutines();
            //        break;

            //    case SEModuleType.IndustrialBooster:
            //        SMStabilizer stabilizerSX = MountedGameObject.GetComponent<SMStabilizer>();
            //        MountStopAllCoroutines();
            //        stabilizerSX.StopAllCoroutines();
            //        break;
            //}


            
        }






        public GameObject InstantiateGO(GameObject prefab, Transform parent) //Stage 1b Initialization
        {
            GameObject newGO = Instantiate(prefab, parent);
            newGO.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            newGO.transform.localPosition = new Vector3(0f, 0f, 0f);
            return newGO;
        }

        public virtual void MountStopAllCoroutines()
        {

        }
    }

    #endregion Abstract Ship Points and Systems

    

    #region Statics

    public static class SetParentTransform
    {
        public static void SetTransformParent(Transform transformToSet, Transform transformParent)
        {
            transformToSet.SetParent(transformParent);
            transformToSet.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            transformToSet.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    #endregion Statics

}

