using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

    private static DataManager instance;
    public static DataManager Instance {get { return instance; } }

    public Text Id;
    public Text UserId;
    public Text UserName;
    public Text UserPic;
    public Text UserFirstName;
    public Text UserLastName;
    public Text UserAccessToken;
    public Text UserState;
    public Text UserAccess;
    public Text UserCredits;
    public Text UserLevel;
    public Text UserMana;
    public Text UserHealth;
    public Text UserExp;

    public Text UserXPos;
    public Text UserYPos;
    public Text UserZPos;
    public Text UserXRot;
    public Text UserYRot;
    public Text UserZRot;
    public Text UserGpsX;
    public Text UserGpsY;
    public Text UserGpsZ;
    public Text  UserFirstTimeLogin;

    public string isMoving = "False";
    public string AnimationStatus = "standing_idle";
    public string IdelTime = "0";


    private void Awake()
    {
        instance = this;
    }
    public void SetId(string set)
    {
       Id.text = set;
    }

    public string GetId()
    {
       return Id.text;
    }
    public void SetUserId(string set)
    {
        PlayerPrefs.SetString("UserId", set);
        UserId.text = set;
    }

    public string GetUserId()
    {
        UserId.text = PlayerPrefs.GetString("UserId");
        return UserId.text;
    }

    public void SetUserName(string set)
    {
       
        UserName.text = set;
    }

    public string GetUserName()
    {
        return UserName.text;
    }


    public void SetUserPic(string set)
    {
        
        UserPic.text = set;
    }

    public string GetUserPic()
    {
        return UserPic.text;
    }

    public void SetUserFirstName(string set)
    {
       
        UserFirstName.text = set;
    }

    public string GetUserFirstName()
    {
        return UserFirstName.text;
    }

    public void SetUserLastName(string set)
    {
        
        UserLastName.text = set;
    }

    public string GetUserLastName()
    {
        return UserLastName.text;
    }

    public void SetUserAccessToken(string set)
    {
        PlayerPrefs.SetString("UserAccessToken", set);
        UserAccessToken.text = set;
    }

    public string GetUserAccessToken()
    {
        UserAccessToken.text = PlayerPrefs.GetString("UserAccessToken");
        return UserAccessToken.text;
    }

    public void SetUserState(string set)
    {
        
        UserState.text = set;
    }

    public string GetUserState()
    {
        return UserState.text;
    }

    public void SetUserAccess(string set)
    {
       
        UserAccess.text = set;
    }

    public string GetUserAccess()
    {
        return UserAccess.text;
    }

    public void SetUserCredits(string set)
    {

        UserCredits.text = set;
        
    }

    public string GetUserCredits()
    {
        return UserCredits.text;
    }

    public void SetUserLevel(string set)
    {

        UserLevel.text = set;
       
    }

    public string GetUserLevel()
    {
        return UserLevel.text;
    }

    public void SetUserMana(string set)
    {

        UserMana.text = set;
       
    }

    public string GetUserMana()
    {
        return UserMana.text;
    }

    public void SetUserHealth(string set)
    {

        UserHealth.text = set;
       
    }

    public string GetUserHealth()
    {
        return UserHealth.text;
    }

    public void SetUserExp(string set)
    {

        UserExp.text = set;
    }

    public string GetUserExp()
    {
        return UserExp.text;
    }


    public void SetUserXPos(string set)
    {

        UserXPos.text = set;
    }

    public string GetUserXPos()
    {
        return UserXPos.text;
    }

    public void SetUserYPos(string set)
    {

        UserYPos.text = set;
    }

    public string GetUserYPos()
    {
        return UserYPos.text;
    }

    public void SetUserZPos(string set)
    {

        UserZPos.text = set;
    }

    public string GetUserZPos()
    {
        return UserZPos.text;
    }

    public void SetUserXRot(string set)
    {

        UserXRot.text = set;
    }

    public string GetUserXRot()
    {
        return UserXRot.text;
    }
    public void SetUserYRot(string set)
    {

        UserYRot.text = set;
    }

    public string GetUserYRot()
    {
        return UserYRot.text;
    }
    public void SetUserZRot(string set)
    {

        UserZRot.text = set;
    }

    public string GetUserZRot()
    {
        return UserZRot.text;
    }

    public void SetUserGpsX(string set)
    {

        UserGpsX.text = set;
    }

    public string GetUserGpsX()
    {
        return UserGpsX.text;
    }

    public void SetUserGpsY(string set)
    {

        UserGpsY.text = set;
    }

    public string GetUserGpsY()
    {
        return UserGpsY.text;
    }
    public void SetUserGpsZ(string set)
    {

        UserGpsZ.text = set;
    }

    public string GetUserGpsZ()
    {
        return UserGpsZ.text;
    }

    public void SetUserFirstTimeLogin(string set)
    {

        UserFirstTimeLogin.text = set;
    }

    public string GetUserFirstTimeLogin()
    {
        return UserFirstTimeLogin.text;
    }

    public void SetUserisMoving(string set)
    {

        isMoving = set;
    }

    public string GetUserisMoving()
    {
        return isMoving;
    }

    public void SetUserAnimationStatus(string set)
    {

        AnimationStatus = set;
    }

    public string GetUserAnimationStatus()
    {
        return AnimationStatus;
    }
    public void SetUserIdelTime(string set)
    {

        IdelTime = set;
    }

    public string GetUserIdelTime()
    {
        return IdelTime;
    }




    public void SaveUsersData()
    {

        //todo make sure you save the users GPS and the user pos and rot here as well
        MysqlManager.Instance.SaveUsersData(this.GetUserId(), 
            this.GetUserAccessToken(), 
            this.GetUserCredits(), 
            this.GetUserLevel(), 
            this.GetUserMana(), 
            this.GetUserHealth(), 
            this.GetUserExp(),
            this.GetUserState());
    }



}
