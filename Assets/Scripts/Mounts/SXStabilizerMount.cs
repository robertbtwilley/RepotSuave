using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXStabilizerMount : SXMount
{
    [SerializeField] SXShip stabShip;
    [SerializeField] SMStabilizer stabSX;

    public override void InitializeMount(SXShip ship, SOModule module)
    {
        base.InitializeMount(ship, module);
        stabShip = ship;
        MountedGameObject = InstantiateGO(module.itemPrefab, MountContainer.transform);
        stabSX = MountedGameObject.GetComponent<SMStabilizer>();
        stabSX.AddMountableModule(ship, module, this);

    }

    

    public override void UnloadMount(SXShip ship, SOModule module)
    {
        base.UnloadMount(ship, module);
        stabSX.StopAllCoroutines();
        Destroy(MountedGameObject);
    }
}
