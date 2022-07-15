using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SNCore;

public class SXTurretBay : SXMount
{
    [SerializeField] GameObject aftDoorGO;
    [SerializeField] GameObject fwdDoorGO;
    [SerializeField] GameObject aftDoorEdgeGO;
    [SerializeField] GameObject fwdDoorEdgeGO;
    [SerializeField] SMTurretPDC turretSX;
    public Transform PDCMount;

    public SMTurretPDC TurretSX
    {
        get { return turretSX; }
        set { turretSX = value; }
    }

   
    public void OpenBayDoors()
    {
        StartCoroutine(OpenBayDoorsDGTween());
    }

    public IEnumerator OpenBayDoorsDGTween()
    {

        fwdDoorGO.transform.DOLocalMoveX(-4.75f, 0.5f);
        aftDoorGO.transform.DOLocalMoveX(-4.75f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        aftDoorGO.transform.DOLocalMoveZ(-6.5f, 1.0f);
        aftDoorGO.transform.DOScaleZ(0.65f, 1.0f);
        fwdDoorGO.transform.DOLocalMoveZ(6.5f, 1.0f);
        fwdDoorGO.transform.DOScaleZ(0.65f, 1.0f);
    }

    public void CloseBayDoors()
    {
        aftDoorGO.transform.DOLocalMoveZ(0.0f, 0.5f);
        aftDoorGO.transform.DOScaleZ(1f, 0.5f);
        fwdDoorGO.transform.DOLocalMoveZ(0.0f, 0.5f);
        fwdDoorGO.transform.DOScaleZ(1f, 0.5f);
        fwdDoorEdgeGO.transform.DOLocalMoveZ(0.0f, 0.5f);
        aftDoorEdgeGO.transform.DOLocalMoveZ(0.0f, 0.5f);
    }

    public override void InitializeMount(SXShip ship, SOModule module)
    {
        base.InitializeMount(ship, module);
        MountedGameObject = InstantiateGO(module.itemPrefab, MountContainer.transform);
        turretSX = MountedGameObject.GetComponent<SMTurretPDC>();
        turretSX.AddMountableModule(ship, module, this);
    }


    public override void UnloadMount(SXShip ship, SOModule module)
    {
        base.UnloadMount(ship, module);
        turretSX.StopAllCoroutines();
        StopAllCoroutines();
        Destroy(MountedGameObject);
    }

}
