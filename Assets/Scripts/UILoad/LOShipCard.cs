using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class LOShipCard : MonoBehaviour
{
    [SerializeField] SXShip playerShipCardSX;
    [SerializeField] SOShip playerShipCardSO;
    [SerializeField] GameObject playerShipCardGO;
    [Space]
    public LOFittingSlot[] ShipFittingLOs;

    public SOShip PlayerShipCardSO
    {
        get { return playerShipCardSO; }
        set { playerShipCardSO = value; }
    }
    public SXShip PlayerShipCardSX
    {
        get { return playerShipCardSX; }
        set { playerShipCardSX = value; }
    }

}



[System.Serializable]
public class LOFittingSlot
{
    public SUFittingSlot LoadFitSlotRef;
    public SXModuleHardPoint LoadOutHardSlotRef;
    public SOModule ActiveFittingSO;
    public SEModuleType ActiveFittingType;
    public SEHardPointLocation ActiveHardLocation;
}
