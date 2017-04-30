using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHoverInterreactWith : MonoBehaviour {

    public GameObject MyClientsContainer;
    public GameObject MyHoverCraft;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        // if this ID is releated to this objects ID
        //OBJECTONE__ID
        //if(ID==_ID)
        //  {

        //  }

        Debug.Log(MyHoverCraft.GetComponent<Transform>().GetChild(0).GetComponent<MeshCollider>().isTrigger = true);

        if(MyHoverCraft.GetComponent<Transform>().GetChild(0).GetComponent<MeshCollider>().isTrigger == false)
        {
            MyHoverCraft.GetComponent<Transform>().GetChild(0).GetComponent<MeshCollider>().isTrigger = true;
        }



    }



}
