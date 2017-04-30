using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInformationLoader : MonoBehaviour {

    public ModelsManager modelsManager;
    public Vector3 Position = Vector3.zero;
    public Vector3 Rotation = Vector3.zero;
    private bool debug = false;
    float Xpos = 0;
    float Ypos = 0;
    float Zpos = 0;
    float Xrot = 0;
    float Yrot = 0;
    float Zrot = 0;


    void Start()
    {

       // Debug.Log("WHAT AM I? " + this.transform.name);

        string GetObjectData = this.transform.name;
        
        string[] aData = GetObjectData.Split('|');
        for (int i = 0; i < aData.Length - 1; i++)
        {

            /*
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
             */

            if (aData[i] == "ID")
            {
               // Debug.Log("ID==: " + aData[i + 1]);
            }
            if (aData[i] == "RELATEDID")
            {
                //Debug.Log("RELATEDID==: " + aData[i + 1]);
            }
            if (aData[i] == "NAME")
            {
                //Debug.Log("NAME==: " + aData[i + 1]);
            }
            if (aData[i] == "MODELID")
            {
               // Debug.Log("MODELID==: " + aData[i + 1]);
                int ModelID = int.Parse(aData[i + 1]);
                if (modelsManager.Models[ModelID].GetComponent<MeshFilter>() != null)
                {
                   // Debug.Log("MODEL MESH IS == " + modelsManager.Models[ModelID].GetComponent<MeshFilter>().sharedMesh.name);
                    transform.gameObject.AddComponent<MeshFilter>().sharedMesh = modelsManager.Models[ModelID].GetComponent<MeshFilter>().sharedMesh;
                }
                if (modelsManager.Models[ModelID].GetComponent<MeshRenderer>() != null)
                {
                   // Debug.Log("MODEL ELEMENT IS == " + modelsManager.Models[ModelID].GetComponent<MeshRenderer>().sharedMaterial.name);
                    transform.gameObject.AddComponent<MeshRenderer>().sharedMaterial = modelsManager.Models[ModelID].GetComponent<MeshRenderer>().sharedMaterial;
                }

                if (modelsManager.Models[ModelID].GetComponent<BoxCollider>() != null)
                {
                   // Debug.Log("MODEL BOX COLLIDER IS == " + modelsManager.Models[ModelID].GetComponent<BoxCollider>().name);
                    transform.gameObject.AddComponent<BoxCollider>().sharedMaterial = modelsManager.Models[ModelID].GetComponent<BoxCollider>().sharedMaterial;
                }
                if (modelsManager.Models[ModelID].GetComponent<SphereCollider>() != null)
                {
                   // Debug.Log("MODEL SPHERE COLLIDER IS == " + modelsManager.Models[ModelID].GetComponent<SphereCollider>().name);
                    transform.gameObject.AddComponent<SphereCollider>().sharedMaterial = modelsManager.Models[ModelID].GetComponent<SphereCollider>().sharedMaterial;
                }
                if (modelsManager.Models[ModelID].GetComponent<CapsuleCollider>() != null)
                {
                    //Debug.Log("MODEL CAPSULE COLLIDER IS == " + modelsManager.Models[ModelID].GetComponent<CapsuleCollider>().name);
                    transform.gameObject.AddComponent<CapsuleCollider>().sharedMaterial = modelsManager.Models[ModelID].GetComponent<CapsuleCollider>().sharedMaterial;
                }
                if (modelsManager.Models[ModelID].GetComponent<MeshCollider>() != null)
                {
                   // Debug.Log("MODEL MESH COLLIDER IS == " + modelsManager.Models[ModelID].GetComponent<MeshCollider>().name);
                    transform.gameObject.AddComponent<MeshCollider>().sharedMaterial = modelsManager.Models[ModelID].GetComponent<MeshCollider>().sharedMaterial;
                }


            }
            if (aData[i] == "SIZE")
            {
               // Debug.Log("SIZE==: " + aData[i + 1]);
            }
            if (aData[i] == "XPOS")
            {
               // Debug.Log("XPOS==: " + aData[i + 1]);
                Xpos = float.Parse(aData[i + 1]);
            }
            if (aData[i] == "YPOS")
            {
               // Debug.Log("YPOS==: " + aData[i + 1]);
                Ypos = float.Parse(aData[i + 1]);
            }
            if (aData[i] == "ZPOS")
            {
               // Debug.Log("ZPOS==: " + aData[i + 1]);
                Zpos = float.Parse(aData[i + 1]);
            }
            if (aData[i] == "XROT")
            {
               // Debug.Log("XROT==: " + aData[i + 1]);
                Xrot = float.Parse(aData[i + 1]);
            }
            if (aData[i] == "YROT")
            {
               // Debug.Log("YROT==: " + aData[i + 1]);
                Yrot = float.Parse(aData[i + 1]);
            }
            if (aData[i] == "ZROT")
            {
               D("ZROT==: ",  aData[i + 1], debug);
                Zrot = float.Parse(aData[i + 1]);
            }


        }
        Position = new Vector3(Xpos,Ypos,Zpos);
        Rotation = new Vector3(Xrot, Yrot, Zrot);
        Quaternion currot = Quaternion.Euler(Rotation);
        transform.position = Position;
        transform.rotation = currot;
    }


    private void D(string h, string m , bool d)
    {
        if (d == true)
        {
            Debug.Log(h + " " + m);
        }
        
    }
}
