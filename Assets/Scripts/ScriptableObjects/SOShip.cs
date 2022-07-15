
using UnityEngine;
using SNCore;

[CreateAssetMenu]
public class SOShip : SOItem
{
    [SerializeField] SEShipClass shipClass;
    [SerializeField] float totalShieldAmount;
    [SerializeField] float totalHullAmount;
    [Space]
    [SerializeField] float targetingRange;
    [SerializeField] float shipMaxSpeed;
    [SerializeField] float shipTurnSpeed;
    [Space]
    [SerializeField] float tacticalShieldAmount;
    [SerializeField] float industrialShieldAmount;
    [SerializeField] float operationsShieldAmount;
    [SerializeField] float engineeringShieldAmount;
    [Space]
    [SerializeField] float tacticalHullAmount;
    [SerializeField] float industrialHullAmount;
    [SerializeField] float operationsHullAmount;
    [SerializeField] float engineeringHullAmount;
    [Space]
    [SerializeField] int tacticalSlots;
    [SerializeField] int industrialSlots;
    [SerializeField] int operationsSlots;
    [SerializeField] int engineeringSlots;
    [Space]
    public GameObject ShipHudUICanvas;
    public GameObject ShipFittingCanvas;
    public GameObject ShipLoadCard; //Links to a prefab in project folder



    public SEShipClass ShipClass
    {
        get { return shipClass; }
        set { shipClass = value; }
    }
    public float TotalShieldAmount
    {
        get { return totalShieldAmount; }
        set { totalShieldAmount = value; }
    }
    public float TotalHullAmount
    {
        get { return totalHullAmount; }
        set { totalHullAmount = value; }
    }
    public float TargetingRange
    {
        get { return targetingRange; }
        set { targetingRange = value; }
    }
    public float ShipMaxSpeed
    {
        get { return shipMaxSpeed; }
        set { shipMaxSpeed = value; }
    }
    public float ShipTurnSpeed
    {
        get { return shipTurnSpeed; }
        set { shipTurnSpeed = value; }
    }
    public float TacticalShieldAmount
    {
        get { return tacticalShieldAmount; }
        set { tacticalShieldAmount = value; }
    }
    public float IndustrialShieldAmount
    {
        get { return industrialShieldAmount; }
        set { industrialShieldAmount = value; }
    }
    public float OperationsShieldAmount
    {
        get { return operationsShieldAmount; }
        set { operationsShieldAmount = value; }
    }
    public float EngineeringShieldAmount
    {
        get { return engineeringShieldAmount; }
        set { totalHullAmount = value; }
    }
    public float TacticalHullAmount
    {
        get { return tacticalHullAmount; }
        set { tacticalHullAmount = value; }
    }
    public float IndustrialHullAmount
    {
        get { return industrialHullAmount; }
        set { industrialHullAmount = value; }
    }
    public float OperationsHullAmount
    {
        get { return operationsHullAmount; }
        set { operationsHullAmount = value; }
    }
    public float EngineeringHullAmount
    {
        get { return engineeringHullAmount; }
        set { engineeringHullAmount = value; }
    }
    public int TacticalSlots
    {
        get { return tacticalSlots; }
        set { tacticalSlots = value; }
    }
    public int IndustrialSlots
    {
        get { return industrialSlots; }
        set { industrialSlots = value; }
    }
    public int OperationsSlots
    {
        get { return operationsSlots; }
        set { operationsSlots = value; }
    }
    public int EngineeringSlots
    {
        get { return engineeringSlots; }
        set { engineeringSlots = value; }
    }




}
