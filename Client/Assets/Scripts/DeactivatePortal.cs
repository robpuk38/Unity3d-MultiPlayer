using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePortal : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "X_Bot")
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
}
