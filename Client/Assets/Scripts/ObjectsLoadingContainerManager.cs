using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsLoadingContainerManager : MonoBehaviour {
    private IEnumerator gettheobjectsdata;
    public GameObject ObjectsContainor;

    private void Start()
    {
        GetObjectsData();
    }

    public void GetObjectsData()
    {
        gettheobjectsdata = GetObjectData();
        StartCoroutine(gettheobjectsdata);
        
    }
    private IEnumerator GetObjectData()
    {

        WWW getData = new WWW("http://www.projectclickthrough.com/server/getobjectsdata.php");
        yield return getData;
        if (getData.isDone)
        {
            string GetObjectData = getData.text;

            string[] aData = GetObjectData.Split('|');
            for (int i = 0; i < aData.Length - 1; i++)
            {
                //Debug.Log("OBJECTDATA: "+aData[i]);
                if (aData[i] == "Id")
                {
                    
                   // Debug.Log("OBJECT ID: " + aData[i + 1]);
                    ObjectsContainor = Instantiate(ObjectsContainor, new Vector3(0, 0, 0), Quaternion.Euler(Vector3.zero)) as GameObject;
                    ObjectsContainor.name = 
                        "ID|"+aData[i + 1]+
                        "|RELATEDID|"+ aData[i + 3]+
                        "|NAME|"+ aData[i + 5]+
                        "|MODELID|"+ aData[i + 7]+
                        "|SIZE|"+ aData[i + 9] +
                        "|XPOS|" + aData[i + 11] +
                        "|YPOS|" + aData[i + 13] +
                        "|ZPOS|" + aData[i + 15] +
                        "|XROT|" + aData[i + 17] +
                        "|YROT|" + aData[i + 19] +
                        "|ZROT|" + aData[i + 21];
                    ObjectsContainor.transform.parent = transform;
                }
              
            }
        }
        yield return null;
    }
 }
