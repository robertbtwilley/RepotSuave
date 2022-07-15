using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXModuleHardPoint : SXHardPoint
{
    public SEModuleType[] AcceptableModuleTypes;
    [SerializeField] SXShip thisShip;

    public SXMount[] Mounts;

    public SXShip ThisShip
    {
        get { return thisShip; }
        set { thisShip = value; }
    }

    public void InitializeMount(SXShip ship, SOModule module)
    {
        thisShip = ship;
        foreach(SXMount mount in Mounts)
        {
            mount.InitializeMount(ship, module);
        }
    }

    public void UnloadMount(SXShip ship, SOModule module)
    {
        foreach (SXMount mount in Mounts)
        {
            
            mount.UnloadMount(ship, module);
        }
    }
}
