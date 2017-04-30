using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysqlManager : MonoBehaviour {

    private static MysqlManager instance;
    public static MysqlManager Instance {get{return instance;}}
    public ModelsManager modelsManager;
    private IEnumerator getthedata;
    private IEnumerator postthedata;
    private IEnumerator savethedata;
    private IEnumerator postthecommand;
    private string AppKey = "appidkeyiswhatwesayitis";

    private void Awake()
    {
        instance = this;
    }

    public void GetUsersData(string UserId, string UserAccessToken)
    {
        getthedata = GetUserData(UserId, UserAccessToken);
        StartCoroutine(getthedata);
    }

    private IEnumerator GetUserData(string UserId, string UserAccessToken)
    {
        
        WWW getData = new WWW("http://www.projectclickthrough.com/server/getusersdata.php?UserId="+UserId+"&UserAccessToken="+UserAccessToken+ "&AppKey="+AppKey);
        yield return getData;

        if(getData.isDone)
        {
            string GetTheData = getData.text;

            string[] aData = GetTheData.Split('|');
            for (int i = 0; i < aData.Length - 1; i++)
            {
                if(aData[i] == "Id")
                {
                    DataManager.Instance.SetId(aData[i+1]);
                   // Debug.Log("Id: " + aData[i + 1]);
                }
                if (aData[i] == "UserId")
                {
                    DataManager.Instance.SetUserId(aData[i + 1]);
                }
                if (aData[i] == "UserName")
                {
                    DataManager.Instance.SetUserName(aData[i + 1]);
                }
                if (aData[i] == "UserPic")
                {
                    DataManager.Instance.SetUserPic(aData[i + 1]);
                }
                if (aData[i] == "UserFirstName")
                {
                    DataManager.Instance.SetUserFirstName(aData[i + 1]);
                }
                if (aData[i] == "UserLastName")
                {
                    DataManager.Instance.SetUserLastName(aData[i + 1]);
                }
                if (aData[i] == "UserState")
                {
                    //DataManager.Instance.SetUserState(aData[i + 1]);
                }
                if (aData[i] == "UserAccess")
                {
                   // Debug.Log("WHAT IS MY ACCESS LEVEL? "+ aData[i + 1]);
                    DataManager.Instance.SetUserAccess(aData[i + 1]);
                }

                if (aData[i] == "UserCredits")
                {
                    DataManager.Instance.SetUserCredits(aData[i + 1]);
                }
                if (aData[i] == "UserLevel")
                {
                    DataManager.Instance.SetUserLevel(aData[i + 1]);
                }
                if (aData[i] == "UserMana")
                {
                    DataManager.Instance.SetUserMana(aData[i + 1]);
                }
                if (aData[i] == "UserHealth")
                {
                    DataManager.Instance.SetUserHealth(aData[i + 1]);
                }
                if (aData[i] == "UserExp")
                {
                    DataManager.Instance.SetUserExp(aData[i + 1]);
                }


                if (aData[i] == "UserXPos")
                {
                    DataManager.Instance.SetUserXPos(aData[i + 1]);
                }
                if (aData[i] == "UserYPos")
                {
                    DataManager.Instance.SetUserYPos(aData[i + 1]);
                }
                if (aData[i] == "UserZPos")
                {
                    DataManager.Instance.SetUserZPos(aData[i + 1]);
                }

                if (aData[i] == "UserXRot")
                {
                    DataManager.Instance.SetUserXRot(aData[i + 1]);
                }
                if(aData[i] == "UserYRot")
                {
                    DataManager.Instance.SetUserYRot(aData[i + 1]);
                }
                if(aData[i] == "UserZRot")
                {
                    DataManager.Instance.SetUserZRot(aData[i + 1]);
                }

                if (aData[i] == "UserGpsX")
                {
                    DataManager.Instance.SetUserGpsX(aData[i + 1]);
                }
                if (aData[i] == "UserGpsY")
                {
                    DataManager.Instance.SetUserGpsY(aData[i + 1]);
                }
                if (aData[i] == "UserGpsZ")
                {
                    DataManager.Instance.SetUserGpsZ(aData[i + 1]);
                }
                if (aData[i] == "FirstTimeLogin")
                {
                    DataManager.Instance.SetUserFirstTimeLogin(aData[i + 1]);
                }

                if (aData[i] == "Error")
                {
                    // there is no user found lets create a new user
                    Debug.Log("Data Manager Error No User");
                   // FacebookManager.Instance.NoUserFound();
                    yield break;
                }
            }
        }

        FacebookManager.Instance.MemoryData();

        yield return null;
    }


    public void PostTheGmCommand(string CMD, string xpos, string ypos, string zpos, string xrot, string yrot, string zrot)
    {
        postthecommand = PostTheGmCommands( CMD,  xpos, ypos,  zpos,  xrot,  yrot, zrot);
        StartCoroutine(postthecommand);

    }
    private IEnumerator PostTheGmCommands(string CMD, string xpos, string ypos, string zpos, string xrot, string yrot, string zrot)
    {
        if(CMD.Contains(".spawnobject"))
        {
            string c  = CMD.Replace(".spawnobject", "");
            int outval = 0;
            bool value = int.TryParse(c, out outval);
            if (value != false && outval != 0 && modelsManager.Models.Length > outval)
            {
               // Debug.Log("COMMAND PREP: " + outval + " xpos " + xpos + " ypos " + ypos + " zpos " + zpos + " xrot " + xrot + " yrot " + yrot + " zrot " + zrot);

                WWWForm postCMDData = new WWWForm();
                postCMDData.AddField("CMD", outval);
                postCMDData.AddField("AppKey", AppKey);
                postCMDData.AddField("xpos", xpos);
                postCMDData.AddField("ypos", ypos);
                postCMDData.AddField("zpos", zpos);
                postCMDData.AddField("xrot", xrot);
                postCMDData.AddField("yrot", yrot);
                postCMDData.AddField("zrot", zrot);
               

                WWW connection = new WWW("http://www.projectclickthrough.com/server/postcommanddata.php", postCMDData);
                yield return connection;

                if (connection.isDone)
                {
                    Debug.Log("COMMAND PROSSECCED: " + outval+ " xpos "+ xpos  + " ypos "+ ypos  + " zpos "+ zpos + " xrot "+ xrot + " yrot "+ yrot + " zrot "+ zrot);
                }
                   
            }
        }
       
        yield return null;
    }
    public void PostUsersData(string UserId, 
        string UserPic, 
        string UserAccessToken,
        string UserName, 
        string UserFirstName, 
        string UserLastName, 
        string UserState , 
        string UserGpsX, 
        string UserGpsY, 
        string UserGpsZ, 
        string FirstTimeLogin)
    {
        postthedata = PostUserData(UserId, UserPic,  UserAccessToken,  UserName,  UserFirstName,  UserLastName, UserState, UserGpsX,  UserGpsY,  UserGpsZ, FirstTimeLogin);
        StartCoroutine(postthedata);
    }

    private IEnumerator PostUserData(string UserId, 
        string UserPic, 
        string UserAccessToken, 
        string UserName, 
        string UserFirstName, 
        string UserLastName, 
        string UserState, 
        string UserGpsX, 
        string UserGpsY, 
        string UserGpsZ, 
        string FirstTimeLogin)
    {

        
          WWWForm postData = new WWWForm();
          postData.AddField("UserId", UserId);
          postData.AddField("UserPic", UserPic);
          postData.AddField("UserAccessToken", UserAccessToken);
          postData.AddField("UserName", UserName);
          postData.AddField("UserFirstName", UserFirstName);
          postData.AddField("UserLastName", UserLastName);
          postData.AddField("UserState", UserState);
          postData.AddField("UserGpsX", UserGpsX);
          postData.AddField("UserGpsY", UserGpsY);
          postData.AddField("UserGpsZ", UserGpsZ);
          postData.AddField("FirstTimeLogin", FirstTimeLogin);

        WWW connection = new WWW("http://www.projectclickthrough.com/server/postusersdata.php", postData);
          yield return connection;

          if(connection.isDone)
          {

              Debug.Log("WE HAVE INSERTED THE DATA");
              GetUsersData(UserId, UserAccessToken);
          }

        yield return null;
    }



    public void SaveUsersData(string UserId, string UserAccessToken, string UserCredits, string UserLevel , string UserMana, string UserHealth, string UserExp,string UserState)
    {

        // TODO save the playes GPS AND POS AND ROT AND FIRST TIME LOGIN
        savethedata = SaveUserData(UserId, UserAccessToken, UserCredits,  UserLevel,  UserMana,  UserHealth, UserExp, UserState);
        StartCoroutine(savethedata);
    }

    private IEnumerator SaveUserData(string UserId, string UserAccessToken, string UserCredits, string UserLevel, string UserMana, string UserHealth, string UserExp,string UserState)
    {
       
        WWWForm postData = new WWWForm();
        postData.AddField("UserId", UserId);
        postData.AddField("UserAccessToken", UserAccessToken);
        postData.AddField("UserCredits", UserCredits);
        postData.AddField("UserLevel", UserLevel);
        postData.AddField("UserMana", UserMana);
        postData.AddField("UserHealth", UserHealth);
        postData.AddField("UserExp", UserExp);
        postData.AddField("UserState", UserState);

        WWW connection = new WWW("http://www.projectclickthrough.com/server/saveusersdata.php", postData);
        yield return connection;

        if (connection.isDone)
        {

           // Debug.Log("WE HAVE SAVED THE DATA");

            //todo is user is logging out dont do this.
          //  if (FacebookManager.Instance.hasLogout != true)
           // {
              //  GetUsersData(UserId, UserAccessToken);
          //  }

        }

        yield return null;

    }
}
