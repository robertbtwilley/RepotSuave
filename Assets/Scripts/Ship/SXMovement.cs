using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class SXMovement : MonoBehaviour
{

    // - Inputs - //

    [SerializeField] Vector3 inputVector;
    //[SerializeField] Vector3 inputVector2;
    //[SerializeField] SIJoystick joystick;
    [SerializeField] SITouchfield touchfield;
    //[SerializeField] SIJoystick joystick2;

    [SerializeField] SIJoystick joystick1;
    //[SerializeField] SIJoystick joystick2;
    //[SerializeField] CinemachineFreeLook cineFL;
    [SerializeField] float rotationSpeed;
    [SerializeField] float turnSmoothTime;
    [SerializeField] float turnSmoothVelocity;
    [SerializeField] SXCameraRig cameraRig;


    // - Character Components - //

    [SerializeField] Rigidbody shipRigidbody;

    // - Character Settings - //

    [SerializeField] float shipSpeed;

    // - Level Components - //

    //[SerializeField] SXCameraRig cameraRig;
    //[SerializeField] ZASLevelComponents levelInstance;

    // - Movement Controller Settings - //

    private float CameraAngleY;
    [SerializeField] float CameraAngleSpeed = 0.1f;
    private float CameraPosY;



    private void Start()
    {
        {
            //levelInstance = ZASLevelComponents.LevelInstance;
            //joystick = levelInstance.levelHUD.hudJoystick;
            //touchfield = levelInstance.levelHUD.hudTouchfield;
            //cameraRig = levelInstance.levelCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //inputVector = new Vector3(joystick1.InputVector.x, 0, joystick1.InputVector.y); //Moved from Update to Fixed Update
        inputVector = new Vector3(0, 0, joystick1.InputVector.y); //Moved from Update to Fixed Update


        //inputVector2 = new Vector3(0, 0, joystick2.InputVector.y); //Moved from Update to Fixed Update

        //if(inputVector.magnitude > 0f && inputVector.z > 0)
        //{
        //    float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //}

        //inputVector = new Vector3(joystick1.Horizontal, 0, joystick1.Vertical); //Moved from Update to Fixed Update
        //inputVector2 = new Vector3(0, 0, joystick2.Vertical);





        //cineFL.m_XAxis.Value = joystick2.Vertical * rotationSpeed;



    }




    private void FixedUpdate()
    {

        var vel = Quaternion.AngleAxis(CameraAngleY, Vector3.up) * inputVector * shipSpeed;
        shipRigidbody.velocity = new Vector3(vel.x, shipRigidbody.velocity.y, vel.z);

        //transform.position = new Vector3(transform.position.x + JoystickLeft.positionX / 10, transform.position.y, transform.position.z + JoystickLeft.positionY / 10);
        //transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickRight.angle + 180, Vector3.up);

        //transform.position = new Vector3(transform.position.x + JoystickLeft.positionX / 10, transform.position.y, transform.position.z + JoystickLeft.positionY / 10);
        //transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickLeft.angle + 180, Vector3.up);

        //if (Vars.option == 1)
        //{
        //    transform.position = new Vector3(transform.position.x + JoystickLeft.positionX / 10, transform.position.y, transform.position.z + JoystickLeft.positionY / 10);
        //    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickRight.angle + 180, Vector3.up);
        //}
        //else if (Vars.option == 2)
        //{
        //    transform.position = new Vector3(transform.position.x + JoystickRight.positionX / 10, transform.position.y, transform.position.z + JoystickRight.positionY / 10);
        //    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickLeft.angle + 180, Vector3.up);
        //}
        //else if (Vars.option == 3)
        //{
        //    transform.position = new Vector3(transform.position.x + JoystickLeft.positionX / 10, transform.position.y, transform.position.z + JoystickLeft.positionY / 10);
        //    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickLeft.angle + 180, Vector3.up);
        //}
        //else if (Vars.option == 4)
        //{
        //    transform.position = new Vector3(transform.position.x + JoystickRight.positionX / 10, transform.position.y, transform.position.z + JoystickRight.positionY / 10);
        //    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickRight.angle + 180, Vector3.up);
        //}



        transform.rotation = Quaternion.AngleAxis(CameraAngleY, Vector3.up);

        CameraAngleY += touchfield.TouchDist.x * CameraAngleSpeed;

        cameraRig.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        cameraRig.transform.rotation = transform.rotation;
    }



}

