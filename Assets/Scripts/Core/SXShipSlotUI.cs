using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXShipSlotUI : MonoBehaviour
{
    [SerializeField] SOModule slotModule;
    [SerializeField] SXHardPoint slotHardpoint;

    public SOModule ShipModule
    {
        get { return slotModule; }
        set { slotModule = value; }
    }

    public SXHardPoint SlotHardpoint
    {
        get { return slotHardpoint; }
    }
}
