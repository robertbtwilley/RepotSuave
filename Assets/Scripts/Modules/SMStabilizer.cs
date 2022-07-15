using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using DG.Tweening;

public class SMStabilizer : SMBooster, IMountable
{
    [SerializeField] SXStabilizerMount stabMountSX;

    public void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount)
    {
        stabMountSX = mount.GetComponent<SXStabilizerMount>();
        InitializeStabilizer();
    }

    public void AddMountableModule(SXShip ship, SOModule module, SXMount mount)
    {
        ModuleShip = ship;
        InitializeModuleFromMountable(ModuleShip, module, mount);

    }


    //public override void InitializeModule(SXShip ship, Transform mountT)
    //{
    //    base.InitializeModule(ship, mountT);

    //    ModuleShip = ship;
    //    InitializeStabilizer();
    //}

    public void InitializeStabilizer()
    {
        StartCoroutine(InitialStabilizerDGTween());
    }

    public IEnumerator InitialStabilizerDGTween()
    {
        Vector3 spin1 = new Vector3(0.0f, 0.0f, -270.0f);
        Vector3 spin2 = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.transform.DOLocalMoveZ(8.0f, 0.25f);
        gameObject.transform.DOLocalRotate(spin1, 0.75f);
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.DOLocalRotate(spin2, 0.75f);
        yield return new WaitForSeconds(0.75f);
        gameObject.transform.DOLocalMoveZ(0.0f, 0.5f);
        StopCoroutine(InitialStabilizerDGTween());
    }

    




}
