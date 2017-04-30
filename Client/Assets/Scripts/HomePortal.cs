using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePortal : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.name == "X_Bot")
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
             other.GetComponent<CapsuleCollider>().isTrigger = false;



            GameObject MyTeleportListCanvas = other.transform.parent.transform.parent.transform.parent.transform.GetChild(4).gameObject;
            Debug.Log("WHAT AM I:   "+ MyTeleportListCanvas.name);

            // GameObject Clients_Camera = other.transform.parent.transform.parent.transform.parent.transform.GetChild(0).gameObject;
            // GameObject MyWorld = other.transform.parent.transform.parent.transform.GetChild(1).gameObject;

             if (MyTeleportListCanvas.activeSelf == false)
             {
                 MyTeleportListCanvas.SetActive(true);


                // other.transform.position = MyWorld.transform.position;
                // Clients_Camera.transform.position = other.transform.position;
             }
             else if (MyTeleportListCanvas.activeSelf == true)
             {
                 MyTeleportListCanvas.SetActive(false);
                // other.transform.position = new Vector3(0, 0, 0);
                // Clients_Camera.transform.position = other.transform.position;
             }


        }
    }
}
