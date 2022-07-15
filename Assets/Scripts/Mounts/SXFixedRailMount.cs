using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using DG.Tweening;

public class SXFixedRailMount : SXMount
{
    public GameObject clampFWDPT;
    public GameObject clampFWDSB;
    public GameObject clampAFTSB;
    public GameObject clampAFTPT;
    public SMFixedWeapon fixedWeaponSX;

    

    public void OpenClamps()
    {
        //StartCoroutine(OpenClampsDGTween());

        Vector3 OpenRotation = new Vector3(0.0f, 0.0f, -60.0f);
        clampFWDPT.transform.DOLocalRotate(OpenRotation, 1.0f);
        clampFWDSB.transform.DOLocalRotate(OpenRotation, 1.0f);
        clampAFTSB.transform.DOLocalRotate(OpenRotation, 1.0f);
        clampAFTPT.transform.DOLocalRotate(OpenRotation, 1.0f);
        //StopCoroutine(OpenClampsDGTween());
    }

    public void CloseClamps()
    {
        //StartCoroutine(CloseClampsDGTween());
        Vector3 CloseRotation = new Vector3(0.0f, 0.0f, -90.0f);
        clampFWDPT.transform.DOLocalRotate(CloseRotation, 1.0f);
        clampFWDSB.transform.DOLocalRotate(CloseRotation, 1.0f);
        clampAFTSB.transform.DOLocalRotate(CloseRotation, 1.0f);
        clampAFTPT.transform.DOLocalRotate(CloseRotation, 1.0f);
        //StopCoroutine(CloseClampsDGTween());
    }

    public override void InitializeMount(SXShip ship, SOModule module)
    {
        base.InitializeMount(ship, module);
        MountedGameObject = InstantiateGO(module.itemPrefab, MountContainer.transform);
        fixedWeaponSX = MountedGameObject.GetComponent<SMFixedWeapon>();
        fixedWeaponSX.AddMountableModule(ship, module, this);
    }

    public override void UnloadMount(SXShip ship, SOModule module)
    {
        base.UnloadMount(ship, module);
        StopAllCoroutines();
        fixedWeaponSX.StopAllCoroutines();
        Destroy(MountedGameObject);
    }

}
