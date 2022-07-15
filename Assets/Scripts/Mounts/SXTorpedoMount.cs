using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXTorpedoMount : SXMount
{
    [SerializeField] SMTorpedoTube torpSX;


    public override void InitializeMount(SXShip ship, SOModule module)
    {
        base.InitializeMount(ship, module);
        MountedGameObject = InstantiateGO(module.itemPrefab, MountContainer.transform);
        torpSX = MountedGameObject.GetComponent<SMTorpedoTube>();
        torpSX.AddMountableModule(ship, module, this);
    }

    public override void UnloadMount(SXShip ship, SOModule module)
    {
        base.UnloadMount(ship, module);
        StopAllCoroutines();
        torpSX.StopAllCoroutines();
        Destroy(MountedGameObject);
    }



    
}
