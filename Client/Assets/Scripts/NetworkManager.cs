using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using SocketIO;

public class NetworkManager : MonoBehaviour {

    private static NetworkManager instance;
    public static NetworkManager Instance { get { return instance; } }

    public bool AmILogin { get; set; }
    public bool load_once { get; set; }
    public bool has_loaded { get; set; }
    public bool has_Joined { get; set; }
    public GameObject LoadedSystem;
    public GameObject MainCamera;
    public GameObject ClientsContainor;
    public string Ipaddress = "127.0.0.1";
    public int port = 3000;
    static string MyUserID;
    GameObject MyContatiner;
    GameObject ThemContatiner;
  

    public SocketIOComponent socket;
    

    private void Start()
    {
        instance = this;
        CheckServerStatus();
    }
    int checkCountTime = 0;
    bool recheck = false;
    private void Update()
    {
        checkCountTime++;
        if (checkCountTime >50)
        {
            CheckServerStatus();
            checkCountTime = 0;
            recheck = false;
        }
        //
        //Debug.Log("WHAT IS MY USERS STATE "+ DataManager.Instance.GetUserState());
       // Debug.Log("WHAT IS MY hasloaded " + FacebookManager.Instance.hasloaded);
        //Debug.Log("WHAT IS MY loadingtime " + FacebookManager.Instance.loadingtime);
      //  Debug.Log("WHAT IS MY IsclientLoginIn " + FacebookManager.Instance.IsclientLoginIn);
       
       
        //bool IsStateActive = bool.Parse();

        if (has_Joined == true && checkCountTime > 40 && recheck == false)
        {
            socket.Emit("OnConnection", new JSONObject(GetPlayerJSONData()));
            recheck = true;
           // Debug.Log("HAS THE PLAYER JOINED? " + has_Joined + "TIME CHECK? " + checkCountTime);
        }

        if (has_loaded == true && load_once == false && has_Joined == true)
        {
            load_once = true;
            Load_Core();
            
        }
       // Debug.Log("WTF IS HAPPENING?? AmILogin  " + AmILogin);
       // Debug.Log("FacebookManager.Instance.IsclientLoginIn  " + FacebookManager.Instance.IsclientLoginIn);
        if (MyContatiner != null && AmILogin == true && FacebookManager.Instance.IsclientLoginIn == true && DataManager.Instance != null && DataManager.Instance.GetUserId() == MyUserID)
        {
            //Debug.Log("THE CLIENT IS LOGIN BUT WHAT CLIENT IS LOGIN IN??");
            MyContatiner.SetActive(true);
            MainCamera.SetActive(false);
            MyContatiner.transform.GetChild(0).gameObject.SetActive(true);
           
        }
        
    }
    
    private void CheckServerStatus()
    {
        TcpClient tcpClient = new TcpClient();
        try
        {
            tcpClient.Connect(Ipaddress, port);
            if(tcpClient.Connected)
            {
               // Debug.Log("WE ARE CONNECTED");
                LoadedSystem.SetActive(true);
                has_loaded = true;
            }
        }
        catch(Exception ex)
        {
            Debug.Log("NO SOCKET ON PORT OR IP: " + ex.Message);
            //LoadedSystem.SetActive(false);
            has_loaded = false;
        }
    }


    private string GetPlayerJSONData()
    {
        PlayerJSON playerJSON = new PlayerJSON(
           DataManager.Instance.GetUserId(),
           DataManager.Instance.GetUserXPos(),
           DataManager.Instance.GetUserYPos(),
           DataManager.Instance.GetUserZPos(),
           DataManager.Instance.GetUserXRot(),
           DataManager.Instance.GetUserYRot(),
           DataManager.Instance.GetUserZRot(),
           DataManager.Instance.GetUserGpsX(),
           DataManager.Instance.GetUserGpsY(),
           DataManager.Instance.GetUserGpsZ(),
           DataManager.Instance.GetUserisMoving(),
           DataManager.Instance.GetUserAnimationStatus(),
           DataManager.Instance.GetUserFirstTimeLogin(),
           DataManager.Instance.GetUserIdelTime(),
           DataManager.Instance.GetUserState()
           );
        //Debug.Log("IS THIS GETTING SENT " + DataManager.Instance.GetUserisMoving());
        string data = JsonUtility.ToJson(playerJSON);
        return data;
    }

    bool SendOnce = false;
    public void CMDPlayersAction()
    {
       if (DataManager.Instance.GetUserisMoving() == "True")
        {
            
            SendOnce = false;
            socket.Emit("OnPlayerActions", new JSONObject(GetPlayerJSONData()));
        }
        if (DataManager.Instance.GetUserisMoving() == "False" && SendOnce == false)
        {
            
            SendOnce = true;
            socket.Emit("OnPlayerActions", new JSONObject(GetPlayerJSONData()));
            
        }

        


    }

    public void CMDOnPlayerDisconnect()
    {
        //to do we want to save the player and their data now

       // Debug.Log("WE ARE LOGGING OUT SEND TO SERVER");
        socket.Emit("OnPlayerDisconnect", new JSONObject(GetPlayerJSONData()));
    }

    public void JoinGame()
    {
        socket.Connect();
        socket.Emit("OnConnection", new JSONObject(GetPlayerJSONData()));
        has_Joined = true;
        //DataManager.Instance.SetUserState("1");
    }

 

    private void Load_Core()
    {
       // Debug.Log("WE ONLY WANT TO SEE THIS ONE TIME");

        socket.On("OnPlayerConnected",_OnPlayerConnected);
        socket.On("OnPlayerActions", _OnPlayerActions);
        socket.On("OnPlayerDisconnect",_OnPlayerDisconnect);

    }

   
    private void _OnPlayerActions(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(data);


        float xpos;
        float ypos;
        float zpos;
        float.TryParse(playerJSON.Xpos, out xpos);
        float.TryParse(playerJSON.Ypos, out ypos);
        float.TryParse(playerJSON.Zpos, out zpos);
        float xrot;
        float yrot;
        float zrot;
        float.TryParse(playerJSON.Xrot, out xrot);
        float.TryParse(playerJSON.Yrot, out yrot);
        float.TryParse(playerJSON.Zrot, out zrot);


        Vector3 InClientsPosition = new Vector3(xpos,ypos,zpos);
        Vector3 InClientsRotation = new Vector3(xrot, yrot, zrot);

        Quaternion CurrentClientsRotation = Quaternion.Euler(InClientsRotation);

        GameObject FindThemPlayerMoving = GameObject.Find("ClientsContainer_"+ playerJSON.UserID).gameObject;
        GameObject ThemPlayer = FindThemPlayerMoving.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

        Animator animation;
        animation = ThemPlayer.GetComponent<Animator>();
        if(playerJSON.AnimationStatus == "standing_idle") 
        {
            animation.SetBool("standing_idle", true);
            animation.SetBool("forward_walking", false);
            animation.SetBool("backward_walking", false);
            animation.SetBool("forward_running", false);
        }
        if (playerJSON.AnimationStatus == "forward_walking")
        {
            animation.SetBool("forward_walking", true);
            animation.SetBool("standing_idle", false);
            animation.SetBool("backward_walking", false);
            animation.SetBool("forward_running", false);
        }
        if (playerJSON.AnimationStatus == "backward_walking")
        {
            animation.SetBool("forward_walking", false);
            animation.SetBool("standing_idle", false);
            animation.SetBool("backward_walking", true);
            animation.SetBool("forward_running", false);
        }
        if (playerJSON.AnimationStatus == "forward_running")
        {
            animation.SetBool("forward_walking", false);
            animation.SetBool("standing_idle", false);
            animation.SetBool("backward_walking", false);
            animation.SetBool("forward_running", true);
        }



        ThemPlayer.transform.position = InClientsPosition;
        ThemPlayer.transform.rotation = CurrentClientsRotation;

        //Debug.Log(" _OnPlayerMove: " + data);

    }
    private void _OnPlayerDisconnect(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(data);

        GameObject FindThemPlayer= GameObject.Find("ClientsContainer_" + playerJSON.UserID).gameObject;

        if (playerJSON.UserID == DataManager.Instance.GetUserId())
        {
            Debug.Log("WE HAVE BEEN LOGGED OUT FROM SOME OTHER DEVICE" + data);
            AmILogin = false;

            load_once = false;
            has_loaded = false;
            has_Joined = false;
            MainCamera.SetActive(true);
            FacebookManager.Instance.FacebookLogout();
           
            socket.Close();
        }
        
        Debug.Log("WE ARE LOGGING OUT SEND TO SERVER SERVER SAID THIS BACK"+ data);
        Destroy(FindThemPlayer);
    }
    //bool instance.AmILogin = false;
    private void _OnPlayerConnected(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
       
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(data);

       // Debug.Log("WE MADE IT IN == :" + playerJSON.UserID);




        // we can instanate our player into the world..
        // inside here we will create our player 
        if (this.transform.Find("ClientsContainer_" + playerJSON.UserID) == null && playerJSON.UserID == DataManager.Instance.GetUserId())
            {

                Debug.Log("I AM AS MYSELF PLAYERJSON DATA == :" + playerJSON.UserID);


                MyContatiner = Instantiate(ClientsContainor, new Vector3(0, 0, 0), Quaternion.Euler(Vector3.zero)) as GameObject;
                MyContatiner.name = "ClientsContainer_" + playerJSON.UserID;
                MyContatiner.transform.parent = this.transform;
                MyContatiner.SetActive(false);

                MyUserID = playerJSON.UserID;

                //AmILogin = true;

                

                // WE WANT TO KNOW IF THE PLAYER IS A FIRST TIME LOGIN SO THAT WE CAN SET THEIR POSTION TO THE LOBBY SERVER LOCATION WHERE ALL FRINEDS CAN MEET.
                // IF THEY ARE NOT A FIRST TIME LOGIN WE WANT TO LET THEM LOGIN WHERE EVER THEY LEFT OFF.

               // Debug.Log("HEY HERE AM I AM I A FIRST TIME USER LOGIN?  " + DataManager.Instance.GetUserFirstTimeLogin());

                GameObject ClientsWorldSpawns = MyContatiner.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
                float clientsworldPostionX = float.Parse(DataManager.Instance.GetUserGpsX());
                float clientsworldPostionY = float.Parse(DataManager.Instance.GetUserGpsY());
                float clientsworldPostionZ = float.Parse(DataManager.Instance.GetUserGpsZ());


                Vector3 clientsworldPostion = new Vector3(clientsworldPostionX, clientsworldPostionY, clientsworldPostionZ);
                ClientsWorldSpawns.transform.position = clientsworldPostion;

                if (DataManager.Instance.GetUserFirstTimeLogin() == "0")
                {
                    // WE KNOW THIS USER IS A FIRST TIME LOGIN UERS 
                    // Debug.Log("I AM A FIRST IME LOGIN USER  ");
                    //ClientsWorldSpawns.SetActive(false);



                }
            }
           else if(this.transform.Find("ClientsContainer_"+ playerJSON.UserID) == null && playerJSON.UserID != DataManager.Instance.GetUserId())
                {
                   
                    Debug.Log("I AM AS NEW PLAYER TO MY CLIENT PLAYERJSON DATA == :" + playerJSON.UserID);
                    ThemContatiner = Instantiate(ClientsContainor, new Vector3(0, 0, 0), Quaternion.Euler(Vector3.zero)) as GameObject;
                    ThemContatiner.name = "ClientsContainer_" + playerJSON.UserID;
                    ThemContatiner.transform.parent = this.transform;
                    ThemContatiner.SetActive(true);
                    ThemContatiner.transform.GetChild(0).gameObject.SetActive(false);
                    ThemContatiner.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<ClientsPlayerManager>().enabled = false;
                    //ThemContatiner.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    ThemContatiner.transform.GetChild(1).gameObject.SetActive(false);

            GameObject ClientsWorldSpawns = ThemContatiner.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
            float clientsworldPostionX = float.Parse(playerJSON.GpsX);
            float clientsworldPostionY = float.Parse(playerJSON.GpsY);
            float clientsworldPostionZ = float.Parse(playerJSON.GpsZ);


            Vector3 clientsworldPostion = new Vector3(clientsworldPostionX, clientsworldPostionY, clientsworldPostionZ);
            ClientsWorldSpawns.transform.position = clientsworldPostion;

            if (playerJSON.FirstTimeLogin == "0")
            {
                // WE KNOW THIS USER IS A FIRST TIME LOGIN UERS 
                // Debug.Log("I AM A FIRST IME LOGIN USER  ");
                //ClientsWorldSpawns.SetActive(false);



            }
            else
            {

            }
            // WE WANT TO MAKE SURE THAT THERE IS ONLY ONE CONTROLLER FOR EACH CLIENT AND ALSO WE WANT TO MAKE SURE THAT IF THE CLIENT IS NOT NEAR THE CLIENT WE DO NOT SHOW
            //AN WE WANT TO MAKE SURE THE OTHER CLIENTS WORLD IS NOT SPAWNED OR VISABLE UNLESS CLIEANT A WITH CLIENT B
        }
      
           
                
            
               
        

        //Debug.Log("_OnPlayerConnected: "+ data);
    }



    [Serializable]
    public class PlayerJSON
    {
        public string UserID;
        public string Xpos;
        public string Ypos;
        public string Zpos;
        public string Xrot;
        public string Yrot;
        public string Zrot;
        public string GpsX;
        public string GpsY;
        public string GpsZ;
        public string isMoving;
        public string AnimationStatus;
        public string FirstTimeLogin;
        public string IdelTime;
        public string UsersState;






        public PlayerJSON( 
            string _UserId, 
            string _xpos , 
            string _ypos, 
            string _zpos, 
            string _xrot, 
            string _yrot, 
            string _zrot, 
            string _GpsX, 
            string _GpsY, 
            string _GpsZ, 
            string _isMoving, 
            string _AnimationStatus,
            string _FirstTimeLogin,
            string _IdelTime,
            string _UsersState)
        {
            UserID = _UserId;
            Xpos = _xpos;
            Ypos = _ypos;
            Zpos = _zpos;
            Xrot = _xrot;
            Yrot = _yrot;
            Zrot = _zrot;
            GpsX = _GpsX;
            GpsY = _GpsY;
            GpsZ = _GpsZ;
            isMoving = _isMoving;
            AnimationStatus = _AnimationStatus;
            FirstTimeLogin = _FirstTimeLogin;
            IdelTime = _IdelTime;
            UsersState = _UsersState;



        }

        public static PlayerJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<PlayerJSON>(data);
        }

    }

}


