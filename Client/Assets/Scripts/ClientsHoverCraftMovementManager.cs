using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsHoverCraftMovementManager : MonoBehaviour {

    public GameObject Clients_Camera;
    public GameObject Player;
    public ClientsControlManager CameraMovementJoystick;
    public ClientsControlManager PlayerMovementJoystick;
    float Xaxis = 0;
    float Zaxis = 0;
    float currentRotation = 0.0f;
    public float MovementSpeed = 1f;
    public float roationSpeed = 1f;
    public float Xadjustment = 0f;
    public float Yadjustment = 2f;
    public float Zadjustment = -4.0f;
    Vector3 Camoffset;
    public float DistanceDamp = 0f;
    public float rotationalDamp = 0f;

    private void Update()
    {

        if (Player.activeSelf == false)
        {
            LeftJoystickPlayerMovement();
            RightJoysticCameraMovement();
        }

          


    }

    private void LateUpdate()
    {
        if (Player.activeSelf == false)
        {
            LookAtTarget();
        }
    }

    private void LookAtTarget()
    {
        Vector3 toPos = Vector3.zero;
        Vector3 pos = Vector3.zero;
        Vector3 curPos = Vector3.zero;

        Quaternion toRot = Quaternion.identity;
        Quaternion curRot = Quaternion.identity;

        Camoffset = new Vector3(Xadjustment,Yadjustment,Zadjustment);
        toPos = this.transform.position + (this.transform.rotation * Camoffset);
        pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        curPos = Vector3.Lerp(Clients_Camera.transform.position, toPos, DistanceDamp);
        Clients_Camera.transform.position = curPos;


        toRot = Quaternion.LookRotation(pos - Clients_Camera.transform.position, transform.up);
        curRot = Quaternion.Slerp(Clients_Camera.transform.rotation, toRot, rotationalDamp);
        Clients_Camera.transform.rotation = curRot;



    }

    private void RightJoysticCameraMovement()
    {
        if (CameraMovementJoystick != null && CameraMovementJoystick.Direction == Vector3.zero)
        {
            // the player is not moving the joystick
        }
        if (CameraMovementJoystick != null && CameraMovementJoystick.Direction != Vector3.zero)
        {
            // the player is  moving the joystick

        currentRotation = CameraMovementJoystick.Direction.normalized.x;
            this.transform.Rotate(0,currentRotation * roationSpeed,0);

        }
    }

    private void LeftJoystickPlayerMovement()
    {
        if(PlayerMovementJoystick != null && PlayerMovementJoystick.Direction == Vector3.zero)
        {
            // the player is not moving the joystick

            Zaxis = 0;
            Xaxis = 0;
        }
        if (PlayerMovementJoystick != null && PlayerMovementJoystick.Direction != Vector3.zero)
        {
            // the player is  moving the joystick
            Zaxis = PlayerMovementJoystick.Direction.normalized.z;
            Xaxis = PlayerMovementJoystick.Direction.normalized.x;
            if(Zaxis > 0)
            {
                this.transform.position += this.transform.forward * MovementSpeed * Zaxis;
            }
            else if(Zaxis <0 )
            {
                this.transform.position += -this.transform.forward * MovementSpeed * Zaxis;
            }
            if(Xaxis > 0)
            {
                this.transform.position += this.transform.forward * MovementSpeed * Xaxis;
            }
            else if(Xaxis < 0)
            {
                this.transform.position += -this.transform.forward * MovementSpeed * Xaxis;
            }
            this.transform.Rotate(0,Xaxis * roationSpeed , 0);


        }
    }





}
