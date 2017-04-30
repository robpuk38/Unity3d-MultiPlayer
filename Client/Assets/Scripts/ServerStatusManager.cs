using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;



public class ServerStatusManager : MonoBehaviour {

    public GameObject NetworkManager;
    public Text Message;
    public GameObject BG;

    public string ServerUrl = "http://www.projectclickthrough.com";
   
   

    private bool ServerStatus = false;
    int countme = 0;
  
    private void Awake()
    {

        CheckServerStatus(ServerUrl);


    }
    private void Update()
    {

        if(countme < 360)
        {
            countme++;
        }
        if (countme > 340)
        {

            CheckServerStatus(ServerUrl);
            countme = 0;
        }
        if (ServerStatus == false)
        {
            Message.text = "Server Offline";
            BG.SetActive(true);
           // NetworkManager.SetActive(false);
        }
        else
        {
            Message.text = "Server Online";
            BG.SetActive(false);
            NetworkManager.SetActive(true);
        }
    }

  


  
    private bool CheckServerStatus(string Url)
    {

       // Debug.Log("MY PLATFORM IS == "+ Application.platform.ToString());

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            
        }



            try
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);
            request.Timeout = 3000;
            request.AllowAutoRedirect = false;
            request.Method = "HEAD";
            using (var responce = request.GetResponse())
            {
                ServerStatus = true;
               
                return true;
            }
        }
        catch
        {
            ServerStatus = false;
           
            return false;
        }

        
    }

}
