using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using SNCore;

public class SMTurretPDC : SMWeapon
{
    public bool IsConcealed;
    public bool ReadyToFire;
    public bool InFireSequence;
    [SerializeField] LayerMask obstacle;
    [SerializeField] GameObject turretLift; //turret C1 parent
    [SerializeField] GameObject turretStand; //turret C1
    [SerializeField] GameObject turretHousing; //turret C2
    [SerializeField] GameObject housingPivot; //turret C2 parent
    [Space]
    [SerializeField] SXBarrel[] turretBarrels;
    [Space]
    [SerializeField] Transform turretTargetT;
    [SerializeField] ObjectPool<ParticleSystem> turretFXPool;
    [SerializeField] SXTurretBay bayMount;

    [Range(0, 180f)] public float maxAngle1 = 45f;
    [Range(0, 180f)] public float maxAngle2 = 60f;
    public float maxTurnSpeed = 90f;
    [SerializeField] bool outOfFiringArc1;
    //[SerializeField] bool outOfFiringArc2;
    [SerializeField] bool aimedAtTarget;
    [SerializeField] int fireIterations;

    

    #region SCRIPTABLE OBJECT ATTRIBUTES

    

    #endregion SCRIPTABLE OBJECT ATTRIBUTES

    #region ACCESSORS

    public Transform TurretTargetT { get { return turretTargetT; } set { turretTargetT = value; } }

    #endregion ACCESSORS

    #region INITIALIZATION


    public override void InitializeModuleFromMountable(SXShip ship, SOModule module, SXMount mount)
    {
        base.InitializeModuleFromMountable(ship, module, mount);

        ModuleShip = ship;
        bayMount = mount.GetComponent<SXTurretBay>();

        if(IsConcealed)
        {
            RevealTurret();
        }
    }

    public void LoadTurretAttributes(SOTurret turret)
    {
        WeaponRange = turret.BaseRange;
        AmmoCapacity = turret.BaseCapacity;
        BurstVolume = turret.BurstVolume;
        FireRate = turret.BaseFireRate;
        FireDelay = turret.FireDelay;
        AimSpeed = turret.BaseAimSpeed;

    }

    public void LoadFX()
    {
        foreach (SXBarrel barrel in turretBarrels)
        {
            //barrel.BarrelFX = InstantiateFX(TurretSO.FireFX.gameObject, barrel, barrel.TurretMuzzle.transform);
        }
    }

    /* - I believe it will be better to set these on the Prefab - */
    public ParticleSystem InstantiateFX(GameObject tempGO, SXBarrel barrel, Transform parent)
    {
        GameObject tempTempGO;
        tempTempGO = Instantiate(tempGO, parent);
        tempTempGO.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        tempTempGO.transform.localPosition = new Vector3(0f, 0f, 0f);
        barrel.BarrelFX = tempTempGO.GetComponent<ParticleSystem>();
        return barrel.BarrelFX;
    }

    #endregion INITIALIZATION

    #region DOTWEEN

    public IEnumerator ConcealDGTween()
    {
        Vector3 concealStandRotation = new Vector3(0.0f, -90.0f, 0.0f);
        Vector3 concealHousingRotation = new Vector3(-90.0f, 0.0f, 0f);

        ReadyToFire = false;
        foreach (SXBarrel barrel in turretBarrels)
        {
            barrel.transform.DOLocalMoveZ(-1.0f, 1.0f);
        }
        housingPivot.transform.DOLocalRotate(concealHousingRotation, 0.5f);
        turretStand.transform.DOLocalRotate(concealStandRotation, 0.5f);
        yield return new WaitForSeconds(0.1f);
        housingPivot.transform.DOLocalMoveY(2, 1.0f);
        turretLift.transform.DOLocalMoveY(0f, 0.75f);
        IsConcealed = true;
        ReadyToFire = false;
        StopCoroutine(ConcealDGTween());
    }

    public IEnumerator RevealDGTween()
    {

        bayMount.OpenBayDoors();
        yield return new WaitForSeconds(0.5f);
        Vector3 revealStandRotation = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 revealHousingRotation = new Vector3(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        turretLift.transform.DOLocalMoveY(5.5f, 1.5f);
        housingPivot.transform.DOLocalMoveY(4.5f, 1.5f);
        yield return new WaitForSeconds(2.0f);
        turretStand.transform.DOLocalRotate(revealStandRotation, 1.5f);
        housingPivot.transform.DOLocalRotate(revealHousingRotation, 2.0f);
        foreach (SXBarrel barrel in turretBarrels)
        {
            barrel.transform.DOLocalMoveZ(1.0f, 2.5f);
        }
        IsConcealed = false;
        ReadyToFire = true;
        StopCoroutine(RevealDGTween());
    }

    public void RevealTurret()
    {
        StartCoroutine(RevealDGTween());
    }

    public void ConcealTurret()
    {
        StartCoroutine(ConcealDGTween());
    }



    #endregion DOTWEEN

    #region TARGET TRACKING AND AIMING

    public void SetCurrentTurretTarget(Transform turretT)
    {
        turretTargetT = turretT;
    }

    public IEnumerator AimTrack()
    {
        if (ReadyToFire && turretTargetT)
        {
            AimTrackTarget(turretTargetT.position);
        }

        yield return new WaitForSeconds(1);
    }


    public void AimTrackTarget(Vector3 targetPosition)
    {
        //var turretC1 = turretStand.transform;
        //var basePoint1 = turretC1.parent;

        //var turretC2 = turretHousing.transform;
        //var basePoint2 = turretC2.parent;

        //var direction1 = targetPosition - turretC1.position;
        //direction1 = Vector3.ProjectOnPlane(direction1, basePoint1.up);
        //var signedAngle1 = Vector3.SignedAngle(basePoint1.forward, direction1, basePoint1.up);

        //var direction2 = targetPosition - turretC2.position;
        //direction2 = Vector3.ProjectOnPlane(direction2, basePoint2.up);
        //var signedAngle2 = Vector3.SignedAngle(basePoint2.forward, direction2, basePoint2.up);

        //if (Mathf.Abs(signedAngle1) > maxAngle1 && Mathf.Abs(signedAngle2) > maxAngle2)
        //{
        //    outOfFiringArc1 = true;
        //    direction1 = basePoint1.rotation * Quaternion.Euler(Mathf.Clamp(-maxAngle1, signedAngle1, maxAngle1), 0, 0) * Vector3.forward;
        //    direction2 = basePoint2.rotation * Quaternion.Euler(0, Mathf.Clamp(signedAngle2, -maxAngle2, maxAngle2), 0) * Vector3.forward;
        //}
        //else
        //    outOfFiringArc1 = false;

        //var rotation1 = Quaternion.LookRotation(direction1, basePoint1.up);
        //var rotation2 = Quaternion.LookRotation(direction2, basePoint2.up);

        //if (rotation1 == turretC1.rotation && rotation2 == turretC2.rotation! && !outOfFiringArc1)
        //{
        //    aimedAtTarget = true;
        //    InitializeFireTurretWeapon(); //STARTS COROUTINE
        //}
        //else
        //{
        //    aimedAtTarget = false;
        //    InterruptFireSequence(); //STOPS COROUTINE
        //}
            

        //turretC1.rotation = Quaternion.RotateTowards(turretC1.rotation, rotation1, maxTurnSpeed * Time.deltaTime);
        //turretC2.rotation = Quaternion.RotateTowards(turretC2.rotation, rotation2, maxTurnSpeed * Time.deltaTime);

    }

    #endregion TARGET TRACKING AND AIMING



    #region WEAPON SHOOTING & SHOOTING COROUTINE

    //public void InitializeFireTurretWeapon()
    //{
    //    if (!InFireSequence && ReadyToFire && turretTargetT)
    //    {
    //        StartCoroutine(FireTurretWeapon());
    //    }
    //}

    //public void FireTurret()
    //{
    //    foreach (SXBarrel barrel in turretBarrels)
    //    {
    //        ProcessRayCast(barrel.TurretMuzzle.transform);
    //    }
    //}

    //public void ProcessRayCast(Transform muzzle)
    //{
    //    Ray trajectory = new Ray(muzzle.position, muzzle.forward);
    //    RaycastHit hit;

    //    ProcessFireFX();

    //    if (Physics.Raycast(trajectory, out hit, TurretSO.BaseRange, 10))
    //    {
    //        Debug.Log("I hit this" + hit.transform.name);
    //        Debug.DrawRay(muzzle.position, muzzle.forward, Color.blue, 50);
    //    }
    //    else
    //    {
    //        Debug.Log("I hit NOTHING");
    //        Debug.DrawRay(muzzle.position, muzzle.forward, Color.red, 50);
    //    }
    //}

    //public void ProcessFireFX()
    //{
    //    foreach (SXBarrel barrel in turretBarrels)
    //    {
    //        barrel.BarrelFX.Play(true);
    //    }
    //}

    //public IEnumerator FireTurretWeapon()
    //{
    //    InFireSequence = true;
    //    yield return new WaitForSeconds(AimSpeed);

    //    int currentAmmo = AmmoCapacity;

    //    for (int i = 0; i < currentAmmo; i++)
    //    {
    //        for (int j = 0; j < BurstVolume; j++)
    //        {
    //            if (turretTargetT && AmmoCapacity > 0 && ReadyToFire && fireIterations > 0 && !outOfFiringArc1 && aimedAtTarget)
    //            {
    //                FireTurret();
    //                AmmoCapacity--;
    //                fireIterations--;
    //                yield return new WaitForSeconds(FireRate);
    //                Debug.Log("Waited for fireRate");
    //            }
    //        }
    //        yield return new WaitForSeconds(FireDelay);
    //        Debug.Log("Waited for fireDelay");
    //    }
    //    //Weapon currentAmmo is now dry - need to !ReadyToFire
    //}

    //public void InterruptFireSequence()
    //{
    //    StopCoroutine(FireTurretWeapon());
    //    Debug.Log("interrupt");
    //}

    #endregion WEAPON SHOOTING & SHOOTING COROUTINE

    private void CheckTargetLOS()
    {
        if (ModuleShip.CurrentTarget)
        {
            Vector3 directionToCheckTarget = ModuleShip.CurrentTarget.position - transform.position;
            float distanceToCheckTarget = directionToCheckTarget.magnitude;

            if (!Physics.Raycast(transform.position, directionToCheckTarget, distanceToCheckTarget, obstacle))
            {
                turretTargetT = ModuleShip.CurrentTarget;
            }
            else
            {
                turretTargetT = null;
            }
                

        }
        else
        {
            turretTargetT = null;
        }
            

    }


    private void Update()
    {
        if(IsConcealed == false || ModuleShip!=null && ModuleShip.IsDocked == false)
        {
            CheckTargetLOS();

            if (turretTargetT)
            {
                Debug.Log("has Target");
                AimTrackTarget(turretTargetT.position);
            }
            else
            {
                Debug.Log("no Target");
            }
        }


        
    }


        //private IEnumerator CheckForTargetTrackAim()
        //{
        //    if (thisTurretShip.CurrentTarget)
        //    {
        //        Debug.Log("has Target");
        //        turretTargetT = thisTurretShip.CurrentTarget;
        //        AimTrackTarget(turretTargetT.position);
        //        yield return new WaitForSeconds(1);


        //    }
        //    else
        //    {
        //        Debug.Log("no Target");
        //        turretTargetT = null;
        //        yield return new WaitForSeconds(1);
        //    }


        //}

        #region GIZMOS

        //void OnDrawGizmos()
        //{
        //    var turretC1 = turretStand.transform;
        //    var turretC1Pos = turretStand.transform.position;
        //    var turretC2 = turretHousing.transform;
        //    var turretC2Pos = turretHousing.transform.position;

        //    var right1 = new Ray(turretC1Pos, turretLift.transform.rotation * Quaternion.Euler(0, maxAngle1, 0) * Vector3.forward);
        //    var left1 = new Ray(turretC1Pos, turretLift.transform.rotation * Quaternion.Euler(0, -maxAngle1, 0) * Vector3.forward);
        //    //Gizmos.DrawLine(turretC1Pos, right1.GetPoint(turretRange));
        //    //Gizmos.DrawLine(turretC1Pos, left1.GetPoint(turretRange));
        //    //Gizmos.DrawLine(right1.GetPoint(turretRange), left1.GetPoint(turretRange));

        //    var right2 = new Ray(turretC2Pos, housingPivot.transform.rotation * Quaternion.Euler(0, maxAngle2, 0) * Vector3.forward);
        //    var left2 = new Ray(turretC2Pos, housingPivot.transform.rotation * Quaternion.Euler(0, -maxAngle2, 0) * Vector3.forward);
        //    //Gizmos.DrawLine(turretC2Pos, right2.GetPoint(turretRange));
        //    //Gizmos.DrawLine(turretC2Pos, left2.GetPoint(turretRange));
        //    //Gizmos.DrawLine(right2.GetPoint(turretRange), left2.GetPoint(turretRange));

        //    //Gizmos.DrawLine(turretC1Pos, turretC1Pos + turretC1.forward * turretRange);
        //    //Gizmos.DrawLine(turretC2Pos, turretC2Pos + turretC2.forward * turretRange);

        //    if (!targetT) return;

        //    var projectionC1 = Vector3.ProjectOnPlane(targetT.position - turretC1Pos, turretLift.transform.up);
        //    //Gizmos.DrawLine(targetT.position, turretC1Pos + projectionC1);
        //    //Gizmos.DrawSphere(turretC1Pos + projectionC1, .5f);

        //    var projectionC2 = Vector3.ProjectOnPlane(targetT.position - turretC2Pos, housingPivot.transform.up);
        //    //Gizmos.DrawLine(targetT.position, turretC2Pos + projectionC2);
        //    //Gizmos.DrawSphere(turretC2Pos + projectionC2, .5f);

        //}

        #endregion GIZMOS


    }


