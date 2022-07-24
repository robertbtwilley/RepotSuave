using UnityEngine;
using System.Collections;
using System;
using SNCore;

namespace FORGE3D
{
    public class F3DTurret : MonoBehaviour
    {
        [HideInInspector] public bool destroyIt;

        public enum TurretTrackingType
        {
            Step,
            Smooth,
        }

        public SOWeapon ThisTurretSORef;


        public Transform PassedInShipTarget;  //The ship will pass the turet a target, and the turret will do the remaining checks to see if it is prioritized, or queued.  I have to come back to this.

        //public bool ReadyToFire;
        //public bool InFireSequence;
        //public bool TrackingATarget;
        //public int fireIterations;
        public Transform turretTargetT;

        public Transform trackTargetT;

        public LayerMask obstacle;

        public TurretTrackingType TrackingType;

        public GameObject Mount;
        public GameObject Swivel;
        public GameObject TurretBase;

        public float maxAngle1;
        public float maxAngle2;
        public float maxTurnSpeed;
        public bool outOfFiringArc1;
        public bool aimedAtTarget;

        public float targetPriortyValue;

        private Vector3 defaultDir;
        private Quaternion defaultRot;

        private Transform headTransform;
        private Transform barrelTransform;

        public float HeadingTrackingSpeed = 2f;
        public float ElevationTrackingSpeed = 2f;

        private Vector3 targetPos;
        [HideInInspector] public Vector3 headingVetor;

        private float curHeadingAngle;
        private float curElevationAngle;

        public Vector2 HeadingLimit;
        public Vector2 ElevationLimit;

        public F3DFXController TurretController;


        [SerializeField] float turretRange;
        [SerializeField] int turretCapacity;
        [SerializeField] float turretFireRate;
        [SerializeField] float turretFireDelay;
        [SerializeField] float turretAimSpeed;
        [SerializeField] int turretBurstVolume;




        public bool DebugDraw;

        public Transform DebugTarget;

        private bool fullAccess;
        public Animator[] Animators;


        private void Awake()
        {
            headTransform = Swivel.GetComponent<Transform>();
            barrelTransform = Mount.GetComponent<Transform>();
        }

        private void Start()
        {
            targetPos = headTransform.transform.position + headTransform.transform.forward * 100f;
            defaultDir = Swivel.transform.forward;
            defaultRot = Quaternion.FromToRotation(transform.forward, defaultDir);
            if (HeadingLimit.y - HeadingLimit.x >= 359.9f)
                fullAccess = true;
            StopAnimation();
        }

        // Autotrack
        public void SetNewTarget(Vector3 _targetPos)
        {
            targetPos = _targetPos;
        }

        // Angle between mount and target
        public float GetAngleToTarget()
        {
            return Vector3.Angle(Mount.transform.forward, targetPos - Mount.transform.position);
        }

        private void Update()
        {
            if (PassedInShipTarget != null && TurretController.InFireSequence == false && TurretController.ReadyToFire == true)
            {
                turretTargetT = PassedInShipTarget;
                //CheckTargetLOS(PassedInShipTarget);

                
                AimTrackTarget(turretTargetT.position);

                //CheckTargetLOS(PassedInShipTarget);
            }

            //else if (!PassedInShipTarget && turretTargetT != null)
            //{
            //    CheckTargetLOS(turretTargetT);
            //}

            //return;
            //if (DebugTarget != null)
            //    targetPos = DebugTarget.transform.position;

                //if (TrackingType == TurretTrackingType.Step)
                //{
                //    if (barrelTransform != null)
                //    {
                //        /////// Heading
                //        headingVetor =
                //            Vector3.Normalize(F3DMath.ProjectVectorOnPlane(headTransform.up,
                //                targetPos - headTransform.position));
                //        float headingAngle =
                //            F3DMath.SignedVectorAngle(headTransform.forward, headingVetor, headTransform.up);
                //        float turretDefaultToTargetAngle = F3DMath.SignedVectorAngle(defaultRot * headTransform.forward,
                //            headingVetor, headTransform.up);
                //        float turretHeading = F3DMath.SignedVectorAngle(defaultRot * headTransform.forward,
                //            headTransform.forward, headTransform.up);

                //        float headingStep = HeadingTrackingSpeed * Time.deltaTime;

                //        // Heading step and correction
                //        // Full rotation
                //        if (HeadingLimit.x <= -180f && HeadingLimit.y >= 180f)
                //            headingStep *= Mathf.Sign(headingAngle);
                //        else // Limited rotation
                //            headingStep *= Mathf.Sign(turretDefaultToTargetAngle - turretHeading);

                // Hard stop on reach no overshooting
                //if (Mathf.Abs(headingStep) > Mathf.Abs(headingAngle))
                //    headingStep = headingAngle;

                //// Heading limits
                //if (curHeadingAngle + headingStep > HeadingLimit.x &&
                //    curHeadingAngle + headingStep < HeadingLimit.y ||
                //    HeadingLimit.x <= -180f && HeadingLimit.y >= 180f || fullAccess)
                //{
                //    curHeadingAngle += headingStep;
                //    headTransform.rotation = headTransform.rotation * Quaternion.Euler(0f, headingStep, 0f);
                //}

                ///////// Elevation
                //Vector3 elevationVector =
                //    Vector3.Normalize(F3DMath.ProjectVectorOnPlane(headTransform.right,
                //        targetPos - barrelTransform.position));
                //float elevationAngle =
                //    F3DMath.SignedVectorAngle(barrelTransform.forward, elevationVector, headTransform.right);

                //// Elevation step and correction
                //float elevationStep = Mathf.Sign(elevationAngle) * ElevationTrackingSpeed * Time.deltaTime;
                //if (Mathf.Abs(elevationStep) > Mathf.Abs(elevationAngle))
                //    elevationStep = elevationAngle;

                // Elevation limits
                //    if (curElevationAngle + elevationStep < ElevationLimit.y &&
                //        curElevationAngle + elevationStep > ElevationLimit.x)
                //    {
                //        curElevationAngle += elevationStep;
                //        barrelTransform.rotation = barrelTransform.rotation * Quaternion.Euler(elevationStep, 0f, 0f);
                //    }
                //}

                //else if (TrackingType == TurretTrackingType.Smooth)
                //{
                //Transform barrelX = barrelTransform;
                //Transform barrelY = Swivel.transform;

                ////finding position for turning just for X axis (down-up)
                //Vector3 targetX = targetPos - barrelX.transform.position;
                //Quaternion targetRotationX = Quaternion.LookRotation(targetX, headTransform.up);

                //barrelX.transform.rotation = Quaternion.Slerp(barrelX.transform.rotation, targetRotationX,
                //    HeadingTrackingSpeed * Time.deltaTime);
                //barrelX.transform.localEulerAngles = new Vector3(barrelX.transform.localEulerAngles.x, 0f, 0f);

                ////checking for turning up too much
                //if (barrelX.transform.localEulerAngles.x >= 180f &&
                //    barrelX.transform.localEulerAngles.x < (360f - ElevationLimit.y))
                //{
                //    barrelX.transform.localEulerAngles = new Vector3(360f - ElevationLimit.y, 0f, 0f);
                //}

                ////down
                //else if (barrelX.transform.localEulerAngles.x < 180f &&
                //         barrelX.transform.localEulerAngles.x > -ElevationLimit.x)
                //{
                //    barrelX.transform.localEulerAngles = new Vector3(-ElevationLimit.x, 0f, 0f);
                //}

                ////finding position for turning just for Y axis
                //Vector3 targetY = targetPos;
                //targetY.y = barrelY.position.y;

                //Quaternion targetRotationY = Quaternion.LookRotation(targetY - barrelY.position, barrelY.transform.up);

                //    barrelY.transform.rotation = Quaternion.Slerp(barrelY.transform.rotation, targetRotationY,
                //        ElevationTrackingSpeed * Time.deltaTime);
                //    barrelY.transform.localEulerAngles = new Vector3(0f, barrelY.transform.localEulerAngles.y, 0f);

                //    if (!fullAccess)
                //    {
                //        //checking for turning left
                //        if (barrelY.transform.localEulerAngles.y >= 180f &&
                //            barrelY.transform.localEulerAngles.y < (360f - HeadingLimit.y))
                //        {
                //            barrelY.transform.localEulerAngles = new Vector3(0f, 360f - HeadingLimit.y, 0f);
                //        }

                //        //right
                //        else if (barrelY.transform.localEulerAngles.y < 180f &&
                //                 barrelY.transform.localEulerAngles.y > -HeadingLimit.x)
                //        {
                //            barrelY.transform.localEulerAngles = new Vector3(0f, -HeadingLimit.x, 0f);
                //        }
                //    }
                //}

                //if (DebugDraw)
                //    Debug.DrawLine(barrelTransform.position,
                //        barrelTransform.position +
                //        barrelTransform.forward * Vector3.Distance(barrelTransform.position, targetPos), Color.red);
        }


        private void CheckTargetLOS(Transform checkTargetT)
        {
            Vector3 directionToCheckTarget = checkTargetT.position - transform.position;
            float distanceToCheckTarget = directionToCheckTarget.magnitude;

            if (turretTargetT != null && checkTargetT != turretTargetT)
            {
                Vector3 directionToTurretTargetT = turretTargetT.position - transform.position;
                float distanceToTurretTargetT = directionToTurretTargetT.magnitude;

                if (distanceToCheckTarget < TurretController.rangeValue && distanceToCheckTarget < distanceToTurretTargetT - targetPriortyValue)
                {
                    if (!Physics.Raycast(transform.position, directionToCheckTarget, distanceToCheckTarget, obstacle))
                    {
                        turretTargetT = checkTargetT;
                        
                        TurretAction(SETurretAction.AimTrackSwitchTarget);
                    }
                    else
                        TurretAction(SETurretAction.AimTrackCurretTarget);
                }
            }

            else if (!turretTargetT)
            {
                if (distanceToCheckTarget < TurretController.rangeValue)
                {
                    if (!Physics.Raycast(transform.position, directionToCheckTarget, distanceToCheckTarget, obstacle))
                    {
                        turretTargetT = checkTargetT;

                        TurretAction(SETurretAction.AimTrackNewTarget);




                        
                    }
                    //else
                    //    TurretAction(SETurretAction.IdleTurret);

                }
                else
                    TurretAction(SETurretAction.IdleTurret);
            }

            else if (turretTargetT != null && checkTargetT == turretTargetT) //Looks goofy, but we are just rechecking LOS and target fidelity;
            {
                if (distanceToCheckTarget < TurretController.rangeValue)
                {
                    if (!Physics.Raycast(transform.position, directionToCheckTarget, distanceToCheckTarget, obstacle))
                    {
                       
                        TurretAction(SETurretAction.AimTrackCurretTarget);
                    }
                    //else
                    //    TurretAction(SETurretAction.IdleTurret);

                }
                //TurretAction(SETurretAction.IdleTurret);
            }

            //else
            //    TurretAction(SETurretAction.IdleTurret);
        }

        private void TurretAction(SETurretAction turretAction)
        {
            switch (turretAction)
            {
                case SETurretAction.IdleTurret:
                    //turretTargetT = null;
                    break;

                case SETurretAction.AimTrackNewTarget:
                    AimTrackTarget(turretTargetT.position);
                    break;

                case SETurretAction.AimTrackSwitchTarget:
                    AimTrackTarget(turretTargetT.position);
                    break;

                case SETurretAction.AimTrackCurretTarget:
                    break;
            }

        }

        //public IEnumerator AimTrack()
        //{
        //    //if (ReadyToFire && turretTargetT)
        //    //{
        //    //    AimTrackTarget(turretTargetT.position);
        //    //}
        //    Debug.Log("AimTracked");
        //    AimTrackTarget(turretTargetT.position);

        //    yield return new WaitForSeconds(0.5f);
        //}


        public void AimTrackTarget(Vector3 targetPosition)
        {
            Debug.Log("AimTrackedTarget");
            var turretRotationH = Swivel.transform; //Swivel
            var pivotRotationH = TurretBase.transform;  //Base

            var turretRotationV = Mount.transform;  //Mount
            var pivotRotationV = turretRotationV.parent;  //Socket

            var direction1 = targetPosition - turretRotationH.position;
            direction1 = Vector3.ProjectOnPlane(direction1, pivotRotationH.up);
            var signedAngle1 = Vector3.SignedAngle(pivotRotationH.forward, direction1, pivotRotationH.up);

            var direction2 = targetPosition - turretRotationV.position;
            direction2 = Vector3.ProjectOnPlane(direction2, pivotRotationV.up);
            var signedAngle2 = Vector3.SignedAngle(pivotRotationV.forward, direction2, pivotRotationV.up);

            if (Mathf.Abs(signedAngle1) > maxAngle1 && Mathf.Abs(signedAngle2) > maxAngle2)
            {
                outOfFiringArc1 = true;
                direction1 = pivotRotationH.rotation * Quaternion.Euler(Mathf.Clamp(-maxAngle1, signedAngle1, maxAngle1), 0, 0) * Vector3.forward;
                direction2 = pivotRotationV.rotation * Quaternion.Euler(0, Mathf.Clamp(signedAngle2, -maxAngle2, maxAngle2), 0) * Vector3.forward;
            }
            else
                outOfFiringArc1 = false;

            var rotation1 = Quaternion.LookRotation(direction1, pivotRotationH.up);
            var rotation2 = Quaternion.LookRotation(direction2, pivotRotationV.up);

            Debug.Log("Last if");

            if (rotation1 == turretRotationH.rotation && rotation2 == turretRotationV.rotation! && !outOfFiringArc1)
            {
                aimedAtTarget = true;
                Debug.Log("last last");
                if(TurretController.InFireSequence == false && TurretController.ReadyToFire == true)
                {
                    TurretController.InitializeFireTurretWeapon(); //STARTS COROUTINE
                }

                
            }
            else
            {
                aimedAtTarget = false;
                Debug.Log("nope");
                //InterruptFireSequence(); //STOPS COROUTINE
                TurretController.Stop();
            }

            turretRotationH.rotation = Quaternion.RotateTowards(turretRotationH.rotation, rotation1, maxTurnSpeed * Time.deltaTime);
            turretRotationV.rotation = Quaternion.RotateTowards(turretRotationV.rotation, rotation2, maxTurnSpeed * Time.deltaTime);

        }
        //public IEnumerator FireTurretWeapon(SOWeapon turret)
        //{
        //    InFireSequence = true;
        //    yield return new WaitForSeconds(turret.BaseAimSpeed);

        //    int currentAmmo = turret.BaseCapacity;

        //    for (int i = 0; i < currentAmmo; i++)
        //    {
        //        for (int j = 0; j < turret.BurstVolume; j++)
        //        {
        //            if (turretTargetT && turret.BaseCapacity > 0 && ReadyToFire && fireIterations > 0 && !outOfFiringArc1 && aimedAtTarget)
        //            {
        //                //FireTurret();
        //                turret.BaseCapacity--;
        //                fireIterations--;
        //                yield return new WaitForSeconds(turret.BaseFireRate);
        //                Debug.Log("Waited for fireRate");
        //            }
        //        }
        //        yield return new WaitForSeconds(turret.FireDelay);
        //        Debug.Log("Waited for fireDelay");
        //    }
        //    //Weapon currentAmmo is now dry - need to !ReadyToFire
        //}

        //public void FireTurret()
        //{
        //    foreach (SXBarrel barrel in turretBarrels)
        //    {
        //        ProcessRayCast(barrel.TurretMuzzle.transform);
        //    }
        //}

        //public void InterruptFireSequence()
        //{

        //}

        public void PlayAnimation()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetTrigger("FireTrigger");
        }

        public void PlayAnimationLoop()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetBool("FireLoopBool", true);
        }

        public void StopAnimation()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetBool("FireLoopBool", false);
        }

        // Use this for initialization


        private Vector3 PreviousTargetPosition = Vector3.zero;





    }
        
}