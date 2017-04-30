using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : MonoBehaviour {

    private static BulletSpawnPoint instance;
    public static BulletSpawnPoint Instance { get { return instance; }}
    public GameObject Player;
    public GameObject PlayerWeaponContainer;
    public GameObject[] Bullet;
    int type = 0;
   										
private float RotationSpeed = 0;						
		
public GameObject TurentWeapon;		

    private void Awake()
    {
        instance = this;
    }

    int countme = 0;
    private void Update()
    {
        if (instance != null)
        {
            if (Player.activeSelf == false)
            {

                // we are in a vehical 
                //Debug.Log("WE ARE IN VEHICAL DO A EFFECT");
                instance.type = 6;
                instance.Settype(instance.type);
                if (instance.countme < 25 && instance.TurrentWeaponFire == true)
                {
                    instance.countme++;
                    instance.RotationSpeed++;
                    instance.TurentWeapon.transform.Rotate(0, 0, instance.RotationSpeed);
                    instance.WeaponsFire(type);


                }
                if (instance.countme > 20 && instance.TurrentWeaponFire == true)
                {
                   // Debug.Log("WE ARE IN VEHICAL DO ROATION ");
                    instance.TurrentWeaponFire = false;
                    instance.countme = 0;

                }

            }
            else if (instance.Player.activeSelf == true)
            {
                instance.TurrentWeaponFire = false;
                //Debug.Log("WE ARE ACTIVE");
                if (instance.PlayerWeaponContainer != null && instance.PlayerWeaponContainer.transform.childCount > 0)
                {
                    if (instance.PlayerWeaponContainer.transform.GetChild(0).gameObject.activeSelf == true)
                    {

                        //Debug.Log("WE ARE FIRE SLEET DO A EFFECT");
                        instance.type = 1;
                        instance.Settype(type);

                    }
                    else if (instance.PlayerWeaponContainer.transform.GetChild(1).gameObject.activeSelf == true)
                    {

                        //  Debug.Log("WE ARE ARCHTRONIC DO A EFFECT");
                        instance.type = 2;
                        instance.Settype(type);
                    }
                    else if (instance.PlayerWeaponContainer.transform.GetChild(2).gameObject.activeSelf == true)
                    {

                        // Debug.Log("WE ARE GRIMBRAND DO A EFFECT");
                        instance.type = 3;
                        instance.Settype(type);

                    }
                    else if (instance.PlayerWeaponContainer.transform.GetChild(3).gameObject.activeSelf == true)
                    {

                        // Debug.Log("WE ARE HELLWAILER DO A EFFECT");
                        instance.type = 4;
                        instance.Settype(type);

                    }
                    else if (instance.PlayerWeaponContainer.transform.GetChild(4).gameObject.activeSelf == true)
                    {

                        //  Debug.Log("WE ARE MAULAR DO A EFFECT");
                        instance.type = 5;
                        instance.Settype(type);

                    }


                }
            }
        }
       
    }

    public int Settype(int _type)
    {
        if (instance != null)
        {
            instance.type = _type;
            return instance.type;
        }
        else
        {
            return 0;
        }

    
    }

    public int Gettype()
    {
        if (instance != null)
        {
            return instance.type;
        }
        else
        {
            return 0;
        }
    }

    private bool TurrentWeaponFire = false;
    public void WeaponsFire(int type)
    {
        if (instance != null)
        {
            if (instance.type == 6)
            {
                // fire from vehical
               // Debug.Log("WEAPONS FIRE TYPE 0");

                instance.TurrentWeaponFire = true;
                instance.FiretureentWeapon();


            }
            else if (instance.type == 1)
            {
                //WeaponsFire type 1
                //Debug.Log("WEAPONS FIRE TYPE 1");
                instance.FireWeaponSingleShot();
            }
            else if (instance.type == 2)
            {
                //Debug.Log("WEAPONS FIRE TYPE 2");
                instance.FireWeaponDoubleShot();
            }
            else if (instance.type == 3)
            {
                //Debug.Log("WEAPONS FIRE TYPE 3");
                instance.FireWeaponMutiShot();
            }
            else if (instance.type == 4)
            {
               // Debug.Log("WEAPONS FIRE TYPE 4");
                instance.FireWeaponSingleShot();
            }
            else if (instance.type == 5)
            {
                //Debug.Log("WEAPONS FIRE TYPE 5");
                instance.FireWeaponDoubleShot();
            }
        }
    }


    private void FireWeaponSingleShot()
    {
        Instantiate(Bullet[0], transform.position, transform.rotation);
    }
    private void FireWeaponDoubleShot()
    {
       
        Instantiate(Bullet[0], transform.position, transform.rotation);
       
    }
    private void FireWeaponMutiShot()
    {
       
        Instantiate(Bullet[0], transform.position, transform.rotation);
      
    }


    private void FiretureentWeapon()
    {
      
        Instantiate(Bullet[0], transform.position,transform.rotation);
       

    }
    // if the weapon type is grande launcher then do this effect for shooting ect 


}
