using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXMinerMount : SXMount
{
    [SerializeField] SXShip minerShip;
    [SerializeField] SMFixedMiner minerSX;

    public override void InitializeMount(SXShip ship, SOModule module)
    {
        base.InitializeMount(ship, module);
        minerShip = ship;
        MountedGameObject = InstantiateGO(module.itemPrefab, MountContainer.transform);
        minerSX = MountedGameObject.GetComponent<SMFixedMiner>();
        minerSX.AddMountableModule(ship, module, this);

    }

    public override void UnloadMount(SXShip ship, SOModule module)
    {
        base.UnloadMount(ship, module);
        minerSX.StopAllCoroutines();
        Destroy(MountedGameObject);

    }


    

}
