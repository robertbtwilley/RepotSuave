using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using DG.Tweening;

public class SMFixedMiner : SMModule, IMountable
{
    [SerializeField] SXMinerMount minerMount;



    public void AddMountableModule(SXShip ship, SOModule module, SXMount mount)
    {
        ModuleShip = ship;
        InitializeModuleFromMountable(ModuleShip, module, mount);

    }

    public void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount)
    {
        InitiallizeFixedMinerFitting();
        minerMount = mount.GetComponent<SXMinerMount>();
    }


    public void InitiallizeFixedMinerFitting()
    {
        StartCoroutine(InitialFixedMinerDGTween());

    }

    public IEnumerator InitialFixedMinerDGTween()
    {
        gameObject.transform.DOLocalMoveZ(9.0f, 0.25f);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.DOLocalMoveZ(0.0f, 1.0f);
        StopCoroutine(InitialFixedMinerDGTween());

    }

    

}
