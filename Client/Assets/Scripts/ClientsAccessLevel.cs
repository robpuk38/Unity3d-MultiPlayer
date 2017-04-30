using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientsAccessLevel : MonoBehaviour {

    public GameObject[] HiddenTools;
    /*REFERANCE 
        0 = GM BTN
        1 = PANLE
        2 = INPUT FIELD
        3 = SEND BNT
        4 = COMMAND TEXT*/

    private void Start()
    {
        HiddenTools[0].SetActive(false);
        HiddenTools[1].SetActive(false);
        //Debug.Log("ON START HERE");
        if (DataManager.Instance != null)
        {
            Debug.Log("WE MADE IT");
            int accesslevel = int.Parse(DataManager.Instance.GetUserAccess());

            if (accesslevel == 0)
            {
                //Debug.Log("WE KNOW THIS IS PLAYERS ACCOUNT");
            }
            if (accesslevel == 1)
            {
                //Debug.Log("WE KNOW THIS IS GM ACCOUNT");
                HiddenTools[0].SetActive(true);
                
            }
            if (accesslevel == 2)
            {
                //Debug.Log("WE KNOW THIS IS ADMIN ACCOUNT");
                HiddenTools[0].SetActive(true);
               
            }

        }
    }

    private void Update()
    {

        if (HiddenTools[4].GetComponent<Text>() != null && HiddenTools[2].GetComponent<InputField>() != null)
        {
            HiddenTools[4].GetComponent<Text>().text = HiddenTools[2].GetComponent<InputField>().text;
        }
    }

    public void ToggleHiddlePanel()
    {
        if(HiddenTools[1].activeSelf == false)
        {
            HiddenTools[1].SetActive(true);
        }
        else if (HiddenTools[1].activeSelf == true)
        {
            HiddenTools[1].SetActive(false);
        }
    }

    public void CMDSubmit()
    {
       // Debug.Log("COMMAND CLICKED");
        if(HiddenTools[4].GetComponent<Text>() != null )
        {
            

            if (HiddenTools[4].GetComponent<Text>().text != "")
            {
                // OK WE CAN SUBMIT THE STRING TEXT 
                string CMD = HiddenTools[4].GetComponent<Text>().text;
                string xpos = HiddenTools[5].transform.position.x.ToString();
                string ypos = HiddenTools[5].transform.position.y.ToString();
                string zpos = HiddenTools[5].transform.position.z.ToString();
                string xrot = HiddenTools[5].transform.rotation.eulerAngles.x.ToString();
                string yrot = HiddenTools[5].transform.rotation.eulerAngles.y.ToString();
                string zrot = HiddenTools[5].transform.rotation.eulerAngles.z.ToString();
                MysqlManager.Instance.PostTheGmCommand(CMD, xpos, ypos, zpos, xrot, yrot, zrot);
            }
            else
            {
              
            }
           
        }

    }

    


}
