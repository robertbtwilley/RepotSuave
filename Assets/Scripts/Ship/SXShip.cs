using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXShip : MonoBehaviour
{
    //Station Scene and Initialization
    public SXHull[] hulls;
    public SXModuleHardPoint[] moduleHardPoints;
    public bool IsDocked;
    //public SULoadOutSlot[] shipLoadOutSlots; //should correspond with the fittingpanel, same order

    //Combat Scene

    public float targetingISpeed; //exposed to test effectiveness of COROUTINE
    [SerializeField] Rigidbody shipRigidbody;
    [Space]
    //public List<SOTurret> turretSOs;
    [SerializeField] string friendlyTag;
    [SerializeField] float targetingRange;
    [SerializeField] Transform currentTarget;
    [SerializeField] bool readyAlert;
    [SerializeField] MeshCollider[] meshColliders;
    

    public List<Transform> qualifiedTargets;


    /* - ACCESSORS -*/
    public float TargetingRange { get { return TargetingRange; } set { TargetingRange = value; } }   
    public bool ReadyAlert { get { return readyAlert; } set { readyAlert = value; } }
    public Transform CurrentTarget { get { return currentTarget; } }

    public void InitializeShip()
    {
        //foreach (SXWeaponPoint point in weaponPoints)
        //{
        //    point.ThisShip = this;
        //    point.InitializeWeaponPoint();
        //}
    }

    //public void InitializeTurretPoints()
    //{
    //    foreach (SXWeaponPoint point in weaponPoints)
    //    {
    //        if (point != null)
    //        {
    //            point.InitializeWeaponPoint(point.TurretSOCopy, this);
    //        }
    //    }
    //}

    //public void ReadyAlertShip()
    //{
    //    readyAlert = true;

    //    foreach (SXWeaponPoint point in weaponPoints)
    //    {
    //        point.ReadyAlertWeaponPoint();
    //    }

    //    StartCoroutine(CheckForNewTargets());

    //}

    //public void UnReadyAlertShip()
    //{
    //    foreach (SXWeaponPoint point in weaponPoints)
    //    {
    //        point.UnReadyAlertWeaponPoint();
    //    }
    //    readyAlert = false;
    //}

    //public IEnumerator CheckForNewTargets()
    //{
    //    currentTarget = GetCurrentTarget();

    //    if(currentTarget)
    //    {
    //        foreach (SXWeaponPoint point in weaponPoints)
    //        {
    //            point.SetWeaponPointTarget(currentTarget);
    //        }
    //        yield return new WaitForSeconds(targetingISpeed);
    //    }


        
    //}

    //public Transform GetCurrentTarget()
    //{
    //    for (int i = 0; i < qualifiedTargets.Count; i++)
    //    {
    //        if (currentTarget)
    //        {
    //            Transform compareShip = qualifiedTargets[i];
    //            Vector3 distanceToCompare = compareShip.transform.position - transform.position;
    //            Vector3 distanceToCurrent = currentTarget.transform.position - transform.position;

    //            if (distanceToCompare.magnitude < distanceToCurrent.magnitude)
    //            {
    //                currentTarget = compareShip;
    //                return currentTarget;
    //            }
    //            else
    //                return currentTarget;
    //        }
    //        else
    //            currentTarget = qualifiedTargets[i];
    //    }
    //    return currentTarget;
    //}

    //public void SetTurretPointCurrentTargets(Transform target)
    //{
    //    foreach(SXWeaponPoint point in weaponPoints)
    //    {
    //        point.SetWeaponPointTarget(target);

    //    }
    //}

}
