using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using DG.Tweening;

public class SMFixedWeapon : SMWeapon //Fixed Railgun and TorpedoTubes use this for now
{
    [SerializeField] SXBarrel barrel;
    [SerializeField] SXMuzzle muzzle;
    [SerializeField] GameObject TopHousingGO;
    [SerializeField] SXFixedRailMount weaponMountSX;

    public override void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount)
    {
        base.InitializeModuleFromMountable(ship, module, mount);

        ModuleShip = ship;

        weaponMountSX = mount.GetComponent<SXFixedRailMount>();

        weaponMountSX.OpenClamps();

        StartCoroutine(SlideRailEffect());
    }

    public IEnumerator SlideRailEffect()
    {
        Vector3 topHousingRecoil = new Vector3(-20.0f, 0.0f, 0.0f);
        Vector3 topHousingReady = new Vector3(-10.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        barrel.gameObject.transform.DOLocalMoveZ(8.0f, 0.25f);
        TopHousingGO.transform.DOLocalMoveZ(-6.0f, 0.5f);
        TopHousingGO.transform.DOLocalRotate(topHousingRecoil, 0.4f);
        yield return new WaitForSeconds(1f);
        barrel.gameObject.transform.DOLocalMoveZ(14.5f, 1.0f);
        TopHousingGO.transform.DOLocalMoveZ(-5.0f, 1.5f);
        TopHousingGO.transform.DOLocalRotate(topHousingReady, 1.25f);
        yield return new WaitForSeconds(1.5f);
        weaponMountSX.CloseClamps();
        StopCoroutine(SlideRailEffect());
    }

    


}
