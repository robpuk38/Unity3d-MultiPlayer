using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWaypointManager : MonoBehaviour {

  
    float timeme = 0;
    bool starttime = false;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I AM THE CREATURE I HOPE " + other.name);
        
        starttime = true;
    }
    void Update()
    {
        if (timeme < 20 && starttime == true)
        {
            timeme++;
            this.gameObject.SetActive(true);
        }
        if(timeme > 18 && starttime == true)
        {
            this.gameObject.SetActive(false);
           // Destroy(this.gameObject);
            starttime = false;
            timeme = 0;
        }
        
    }
}
