using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SMTorpedoTube : SMWeapon
{
    [SerializeField] SXShip torpShipSX;
    [SerializeField] SXTorpedoMount torpedoMountSX;

    public override void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount)
    {
        base.InitializeModuleFromMountable(ship, module, mount);
        torpShipSX = ship;
        torpedoMountSX = mount.GetComponent<SXTorpedoMount>();
        
    }

    


}
