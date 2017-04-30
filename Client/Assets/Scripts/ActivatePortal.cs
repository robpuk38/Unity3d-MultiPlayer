using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortal : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "X_Bot")
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<CapsuleCollider>().isTrigger = true;
            GameObject MyTeleportListCanvas = other.transform.parent.transform.parent.transform.parent.transform.GetChild(4).gameObject;
            

           if (MyTeleportListCanvas.activeSelf == true)
            {
                MyTeleportListCanvas.SetActive(false);
            }
        }
    }
}
