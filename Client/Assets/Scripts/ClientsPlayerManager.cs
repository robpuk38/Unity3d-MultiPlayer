using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsPlayerManager : MonoBehaviour
{
    //private static ClientsPlayerManager instance;
   // public static ClientsPlayerManager Instance { get { return instance; } }
   // public GameObject MainCamera;
    public GameObject Clients_Camera;
  
    public ClientsControlManager CameraMovementJoystick;
    public ClientsControlManager PlayerMovementJoystick;
    public Transform Player;
    public Transform LookAtPoint;
    //public Transform WeaponsCamerLookAtRot;
    private Transform Character;
    Animator anim;
    // public GameObject WeaponContainer;
    // public Transform MyHoverCraft;
    float Xaxis = 0;
    float Zaxis = 0;
    float currentRotation = 0.0f;
    public float MovementSpeed = 1f;
    public float roationSpeed = 1f;

    public float DistanceDamp = 0.1f;
     public float rotationalDamp = 0.1f;
    public float Zadjustment = 0;
    public float Xadjustment = 0;
    public float Yadjustment = 0;
    //public float ControllerRotDamp = 0.1f;
    //public float MovementSpeed = 0.1f;


    //public bool WeaponsState = false;

    //private bool DisableAutoFollowCamera;


    // float XAxis = 0;
    // float ZAxis = 0;

    //private bool isWalking = false;
    //private bool isRunning = false;

    //private bool isCrouch = false;

        /*

    private bool forward = false;
     private bool backward = false;
     private bool left = false;
     private bool right = false;
     private bool forwardleft = false;
     private bool forwardright = false;
     private bool backwardleft = false;
     private bool backwardright = false;*/

    private bool isMoving = false;


    private void Start()
    {
        // instance = this;
        //  MainCamera.SetActive(false);

        anim = Player.GetChild(0).GetComponent<Animator>();
        Character = Player.GetChild(0).GetComponent<Transform>();

    }
    int checkme = 0;
    private void Update()
    {

        if (DataManager.Instance != null && Player != null)
        {

            DataManager.Instance.SetUserXPos(Character.transform.position.x.ToString());
            DataManager.Instance.SetUserYPos(Character.transform.position.y.ToString());
            DataManager.Instance.SetUserZPos(Character.transform.position.z.ToString());

            DataManager.Instance.SetUserXRot(Character.transform.rotation.eulerAngles.x.ToString());
            DataManager.Instance.SetUserYRot(Character.transform.rotation.eulerAngles.y.ToString());
            DataManager.Instance.SetUserZRot(Character.transform.rotation.eulerAngles.z.ToString());
            DataManager.Instance.SetUserisMoving(isMoving.ToString());
            DataManager.Instance.SetUserIdelTime("0");
            if (checkme < 100)
            {
                checkme++;
                isMoving = false;
            }
            else
            {
                isMoving = true;
                checkme = 0;
                
            }
            //Debug.Log("CHECK ME "+ isMoving);
            NetworkManager.Instance.CMDPlayersAction();
            
        }
      /*  if (Player.gameObject.activeSelf == false)
        {
            Character.position = MyHoverCraft.position;
            Character.rotation = MyHoverCraft.rotation;
            BulletSpawnPoint.Instance.Settype(6);
           // anim.SetBool("isShooting", false);
        }*/

       // if (Player.gameObject.activeSelf == true)
       // {

         /*   if (shootme < 25 && anim.GetBool("isShooting") == true)
            {
                shootme++;
               // Debug.Log("WE ARE SHOOTING NOW"+shootme);
               // anim.SetBool("isShooting", true);
            }
            if (shootme > 20 && anim.GetBool("isShooting") == true)
            {
               // anim.SetBool("isShooting", false);
               // Debug.Log("WE ARE DONE SHOOTING NOW" + shootme);
                fireWeaponShot();
                shootme = 0;
                
            }*/
            RightJoysticCameraMovement();
            LeftJoystickPlayerMovment();

           
            

            //to do we need to save the players weapons state so the system remebers if the player has a weapon or not
          /*  if (WeaponsState == false)
            {
                anim.SetBool("WeaponState", false);
                if (WeaponsState == false && isRunning == false)
                {
                    DistanceDamp = 0.07f;
                    rotationalDamp = 0.15f;
                    Zadjustment = -2f;
                    Xadjustment = 0;
                    Yadjustment = -0.5f;

                }
                else if (WeaponsState == false && isRunning == true)
                {
                    DistanceDamp = 0.2f;
                    rotationalDamp = 0.3f;
                    Zadjustment = -1.5f;
                    Xadjustment = 0;
                    Yadjustment = 0f;
                }


            }
            else if (WeaponsState == true)
            {
                if (WeaponsState == true && isRunning == false && isCrouch == false)
                {
                    DistanceDamp = 0.08f;
                    rotationalDamp = 0.8f;
                    Zadjustment = -1.5f;
                    Xadjustment = 0;
                    Yadjustment = -0.5f;

                }
                else if (WeaponsState == true && isRunning == true && isCrouch == false)
                {
                    DistanceDamp = 0.3f;
                    rotationalDamp = 0.3f;
                    Zadjustment = -1.5f;
                    Xadjustment = 0;
                    Yadjustment = -0.5f;
                }
                else if (WeaponsState == true && isRunning == true && isCrouch == true)
                {
                    DistanceDamp = 0.08f;
                    rotationalDamp = 0.3f;
                    Zadjustment = -2.5f;
                    Xadjustment = 0;
                    Yadjustment = -0.5f;
                }
                else if (WeaponsState == true && isRunning == false && isCrouch == true)
                {
                    DistanceDamp = 0.08f;
                    rotationalDamp = 0.3f;
                    Zadjustment = -2f;
                    Xadjustment = 0;
                    Yadjustment = -0.5f;
                }
              //  Debug.Log("THE PLAYER HAS A WEAPON");
                anim.SetBool("WeaponState", true);


            }*/

           /* ForwardLeft();
            Left();
            BackwardLeft();
            Backward();
            ForwardRight();
            Forward();
            Right();
            BackwardRight();*/
       // }
    }



    private void LateUpdate()
    {
        //if (Player.gameObject.activeSelf == true)
      //  {
            LookAtTarget();
       // }
    }

    //public Vector3 WeaponCamOffest;
    private void LookAtTarget()
    {





       
            Vector3 Camoffset = new Vector3(Xadjustment, Yadjustment, Zadjustment);
            Vector3 toPos = LookAtPoint.position + (LookAtPoint.rotation * Camoffset);
            Vector3 curPos = Vector3.LerpUnclamped(Clients_Camera.transform.position, toPos, DistanceDamp);
            Clients_Camera.transform.position = curPos;
            Quaternion toRot = Quaternion.LookRotation(LookAtPoint.position - Clients_Camera.transform.position, transform.up);
            Quaternion curRot = Quaternion.Slerp(Clients_Camera.transform.rotation, toRot, rotationalDamp);
            Clients_Camera.transform.rotation = curRot;
       

        /*
                if (DisableAutoFollowCamera == false)
                {




                   /* if (WeaponsState == false)
                    {*/
        /*        


          /*   }
             else if (WeaponsState == true)
             {
                 Camoffset = new Vector3(Xadjustment, Yadjustment, Zadjustment);
                 toPos = WeaponsCamerLookAtRot.position + (WeaponsCamerLookAtRot.rotation * Camoffset);

                 pos = new Vector3(WeaponsCamerLookAtRot.position.x, WeaponsCamerLookAtRot.position.y, WeaponsCamerLookAtRot.position.z);

             }*/
        /*   


       }
       /*else
       {
          /* if (isMoving == false)
           {

               if (WeaponsState == false)
               {
                   Clients_Camera.transform.rotation = LookAtPoint.transform.rotation;
                   Clients_Camera.transform.position = LookAtPoint.transform.position;
               }
               else if (WeaponsState == true)
               {
                   Quaternion tonewRot = Quaternion.LookRotation(WeaponCamOffest - Clients_Camera.transform.position, transform.up);
                   Clients_Camera.transform.rotation = Quaternion.Slerp(WeaponsCamerLookAtRot.transform.rotation, tonewRot, rotationalDamp);



                   Clients_Camera.transform.position = WeaponsCamerLookAtRot.transform.position;
               }
           }*/

        //  }


    }
   

    private void RightJoysticCameraMovement()
    {


        if (CameraMovementJoystick != null && CameraMovementJoystick.Direction == Vector3.zero)
        {
           // DisableAutoFollowCamera = false;

           // LookAtPoint.transform.rotation = Character.transform.rotation;



        }

        if (CameraMovementJoystick != null && CameraMovementJoystick.Direction != Vector3.zero)
        {
           // DisableAutoFollowCamera = true;
            currentRotation = CameraMovementJoystick.Direction.normalized.x;

            LookAtPoint.transform.Rotate(0, currentRotation * roationSpeed, 0);

            // LookAtPoint.rotation = Quaternion.identity * Quaternion.AngleAxis(currentRotation, Vector3.up);
            // Character.rotation = Quaternion.identity * Quaternion.AngleAxis(currentRotation, Vector3.up);



            // Vector3 pos = new Vector3(LookAtPoint.position.x, LookAtPoint.position.y, LookAtPoint.position.z);
            // Quaternion toRot = Quaternion.LookRotation(pos, transform.up);
            // Quaternion curRot = Quaternion.Slerp(Clients_Camera.transform.rotation, toRot, ControllerRotDamp);
            // Clients_Camera.transform.rotation = curRot;




        }


    }

    private void LeftJoystickPlayerMovment()
    {
        if (PlayerMovementJoystick != null && PlayerMovementJoystick.Direction == Vector3.zero)
        {

            // We are not moving the joystick
            //  anim.SetFloat("ZAxis", 0);
            // anim.SetFloat("XAxis", 0);

            // XAxis = 0;
            // ZAxis = 0;

            Zaxis = 0;
            Xaxis = 0;
            //  isWalking = false;
            Zadjustment = -2f;
            anim.SetBool("standing_idle", true);
            anim.SetBool("forward_walking", false);
            anim.SetBool("backward_walking", false);
            anim.SetBool("backward_running", false);
            anim.SetBool("forward_running", false);
           
            DataManager.Instance.SetUserAnimationStatus("standing_idle");


        }

        if (PlayerMovementJoystick != null && PlayerMovementJoystick.Direction != Vector3.zero)
        {
            
            isMoving = true;
            anim.SetBool("standing_idle", false);
        


            Zaxis = PlayerMovementJoystick.Direction.normalized.z;
            Xaxis = PlayerMovementJoystick.Direction.normalized.x;
            if (Zaxis < 0)
            {
                Character.transform.position += Character.transform.forward * MovementSpeed * Zaxis;
                if (anim.GetBool("RunState") == true)
                {
                    anim.SetBool("backward_running", true);
                    anim.SetBool("backward_walking", false);
                    anim.SetBool("forward_walking", false);
                    anim.SetBool("forward_running", false);
                }
                else
                {
                    Zadjustment = -3f;
                    anim.SetBool("backward_walking", true);
                    anim.SetBool("forward_walking", false);
                    anim.SetBool("backward_running", false);
                    anim.SetBool("forward_running", false);
                }
                DataManager.Instance.SetUserAnimationStatus("backward_walking");
            }
            if (Zaxis > 0 )
            {
                Character.transform.position += Character.transform.forward * MovementSpeed * Zaxis;
                if (anim.GetBool("RunState") == true)
                {
                    anim.SetBool("forward_running", true);
                    anim.SetBool("backward_running", false);
                    anim.SetBool("backward_walking", false);
                    anim.SetBool("forward_walking", false);
                }
                else
                {
                    Zadjustment = -2f;
                    anim.SetBool("forward_walking", true);
                    anim.SetBool("backward_walking", false);
                    anim.SetBool("backward_running", false);
                    anim.SetBool("forward_running", false);
                }
                DataManager.Instance.SetUserAnimationStatus("forward_walking");
            }

            if (Xaxis > 0 )
            {
                Character.transform.position += Character.transform.forward * MovementSpeed * Xaxis;
                Character.transform.position += Character.transform.right * MovementSpeed * Xaxis;
               
            }
            if (Xaxis < 0)
            {
                Character.transform.position += Character.transform.forward * MovementSpeed * Xaxis;
                Character.transform.position += Character.transform.right * MovementSpeed * Xaxis;

            }

            Character.transform.Rotate(0, Xaxis * roationSpeed, 0);

           







            if (PlayerMovementJoystick.Direction.normalized.x < 0.0f && PlayerMovementJoystick.Direction.normalized.z > 0.0f)
            {

                if (PlayerMovementJoystick.Direction.normalized.z > 0.1f)
                {


                  /*  if (anim.GetFloat("ZAxis") < 0.9f)
                    {

                        anim.SetFloat("ZAxis", 0.9f);

                    }*/
                    // ForwardLeft();


                   /* forward = false;
                    backward = false;
                    left = false;
                    right = false;
                    forwardleft = true;
                    forwardright = false;
                    backwardleft = false;
                    backwardright = false;*/



                }
                else if (PlayerMovementJoystick.Direction.normalized.x < 0.1f)
                {


                    // Left();

                  /*  forward = false;
                    backward = false;
                    left = true;
                    right = false;
                    forwardleft = false;
                    forwardright = false;
                    backwardleft = false;
                    backwardright = false;*/

                }



            }
            else if (PlayerMovementJoystick.Direction.normalized.x < 0.0f && PlayerMovementJoystick.Direction.normalized.z < 0.0f)
            {
                if (-PlayerMovementJoystick.Direction.normalized.z < 0.9f)
                {

                  /*  if (anim.GetFloat("ZAxis") < -0.0f)
                    {

                        anim.SetFloat("ZAxis", -0.9f);

                    }*/
                    // BackwardLeft();

                   /* forward = false;
                    backward = false;
                    left = false;
                    right = false;
                    forwardleft = false;
                    forwardright = false;
                    backwardleft = true;
                    backwardright = false;
                    */


                }
                else
                {

                    //Backward();

                   /* forward = false;
                    backward = true;
                    left = false;
                    right = false;
                    forwardleft = false;
                    forwardright = false;
                    backwardleft = false;
                    backwardright = false;
                    */
                }

            }
            else if (PlayerMovementJoystick.Direction.normalized.x > 0.0f && PlayerMovementJoystick.Direction.normalized.z > 0.0f)
            {
                if (PlayerMovementJoystick.Direction.normalized.x > 0.1f)
                {

                   /* if (anim.GetFloat("ZAxis") < 0.9f)
                    {

                        anim.SetFloat("ZAxis", 0.9f);

                    }*/

                    // ForwardRight();

                   /* forward = false;
                    backward = false;
                    left = false;
                    right = false;
                    forwardleft = false;
                    forwardright = true;
                    backwardleft = false;
                    backwardright = false;*/



                }
                else if (PlayerMovementJoystick.Direction.normalized.x < 0.1f)
                {

                   /* if (anim.GetFloat("ZAxis") == 0 && anim.GetFloat("XAxis") == 0)
                    {
                        anim.SetFloat("ZAxis", 0.9f);
                    }*/

                    //  Forward();

                   /* forward = true;
                    backward = false;
                    left = false;
                    right = false;
                    forwardleft = false;
                    forwardright = false;
                    backwardleft = false;
                    backwardright = false;*/



                }

            }
            else if (PlayerMovementJoystick.Direction.normalized.x > 0.0f && PlayerMovementJoystick.Direction.normalized.z < 1.0f)
            {
                if (PlayerMovementJoystick.Direction.normalized.x > 0.9f)
                {



                    //Right();

                   /* forward = false;
                    backward = false;
                    left = false;
                    right = true;
                    forwardleft = false;
                    forwardright = false;
                    backwardleft = false;
                    backwardright = false;*/



                }
                else if (PlayerMovementJoystick.Direction.normalized.x < 1.0f)
                {

                    /*if (anim.GetFloat("ZAxis") < 0.0f)
                    {

                        anim.SetFloat("ZAxis", -0.9f);

                    }*/


                    //BackwardRight();
                   /* forward = false;
                    backward = false;
                    left = false;
                    right = false;
                    forwardleft = false;
                    forwardright = false;
                    backwardleft = false;
                    backwardright = true;*/

                }



            }




        }
    }

    public void ABtn()
    {
        Debug.Log("BUTTON A WAS PRESSED");
        /* if (Player.gameObject.activeSelf == false)
         {

             Vector3 Pos = new Vector3(MyHoverCraft.position.x - 0.1f, 0.5f, MyHoverCraft.position.z - 4.0f);

             // we need to move the player away from the hovercrafts POS to exit.
             Character.position = Pos;
             //We are in a contaeer that flys or moves other then the player.. and we want to get out of the ship or car or whatever.
             Player.gameObject.SetActive(true);

         }
         else if (anim.GetBool("CrouchState") == true)
         {
             anim.SetBool("CrouchState", false);
             isCrouch = false;

         }
         else
         {

             anim.SetBool("CrouchState", true);
             isCrouch = true;
         }*/



    }
    public void BBtn()
    {

        //Debug.Log("BUTTON B WAS PRESSED");
       if (anim.GetBool("RunState") == true)
        {
            anim.SetBool("RunState", false);
            //isRunning = false;
           

        }
        else
        {

            anim.SetBool("RunState", true);
           // isRunning = true;
        }


    }
    public void XBtn()
    {
        Debug.Log("BUTTON X WAS PRESSED");
        //anim.SetBool("PickupState", true);
    }
    private void fireWeaponShot()
    {
        BulletSpawnPoint.Instance.WeaponsFire(BulletSpawnPoint.Instance.Gettype());
    }
    public void YBtn()
    {
        Debug.Log("BUTTON Y WAS PRESSED");
        // Debug.Log("WEAPONS STATE BUTTON");

        /*  if (anim.GetBool("WeaponState") == true)
          {
              anim.SetBool("isShooting", true);

              if (Player.gameObject.activeSelf == false)
              {
                  //Debug.Log("WE CAN STILL FIRE IN A SHIP ");
                  BulletSpawnPoint.Instance.Settype(6);
                  BulletSpawnPoint.Instance.WeaponsFire(BulletSpawnPoint.Instance.Gettype());
              }






          }
          else
          {
              anim.SetBool("isShooting", false);
              if (Player.gameObject.activeSelf == false)
              {
                  //Debug.Log("WE CAN STILL FIRE IN A SHIP 2");
                  BulletSpawnPoint.Instance.Settype(6);
                  BulletSpawnPoint.Instance.WeaponsFire(BulletSpawnPoint.Instance.Gettype());
              }
              else
              {
                  Debug.Log("WE HAVE NO WEAPON WE HAVE NOTHING TO FIRE FROM OPEN THE WEAPONS MENU?? ");
              }
          }*/
    }


    public void WeaponContatiner(string WeaponName)
    {

       // Debug.Log("WEAPON NAME IS " + WeaponName);
       /* if (WeaponName == "fire_sleet")
        {
            WeaponContainer.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
            // Debug.Log("WE ARE  " + WeaponName);


        }
        if (WeaponName == "archtronic")
        {
            WeaponContainer.GetComponent<Transform>().GetChild(1).gameObject.SetActive(true);
            //  Debug.Log("WE ARE  " + WeaponName);


        }
        if (WeaponName == "grimbrand")
        {
            WeaponContainer.GetComponent<Transform>().GetChild(2).gameObject.SetActive(true);
            // Debug.Log("WE ARE  " + WeaponName);


        }
        if (WeaponName == "hellwailer")
        {
            WeaponContainer.GetComponent<Transform>().GetChild(3).gameObject.SetActive(true);
            //  Debug.Log("WE ARE  " + WeaponName);


        }
        if (WeaponName == "mauler")
        {
            WeaponContainer.GetComponent<Transform>().GetChild(4).gameObject.SetActive(true);
            // Debug.Log("WE ARE  " + WeaponName);


        }*/

    }
    /*
    private void Forward()
    {
       // anim.SetBool("Forward_Idle_Walking", forward);
       // anim.SetBool("isMoving", isMoving);
    }
    private void Backward()
    {

    }
    private void Left()
    {
       // anim.SetBool("Left_Idle_Walking", left);
       // anim.SetBool("isMoving", isMoving);
    }
    private void Right()
    {
      //  anim.SetBool("Right_Idle_Walking", right);
      //  anim.SetBool("isMoving", isMoving);
    }
    private void ForwardLeft()
    {
      //  anim.SetBool("Forward_Left_Idle_Walking", forwardleft);
       // anim.SetBool("isMoving", isMoving);
    }
    private void ForwardRight()
    {
       // anim.SetBool("Forward_Right_Idle_Walking", forwardright);
       // anim.SetBool("isMoving", isMoving);
    }
    private void BackwardLeft()
    {

    }
    private void BackwardRight()
    {

    }*/

}
