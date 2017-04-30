using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureWaypointSearch : MonoBehaviour {

    public float Distance = 10f;
    public float MovementSpeed = 0.1f;


   

    void Update () {

        Debug.DrawRay(transform.position, Vector3.forward * Distance, Color.blue);
        Debug.DrawRay(transform.position, -Vector3.forward * Distance, Color.blue);
        Debug.DrawRay(transform.position, Vector3.right * Distance, Color.blue);
        Debug.DrawRay(transform.position, -Vector3.right * Distance, Color.blue);

        Debug.DrawRay(transform.position, Vector3.right * Distance + Vector3.forward * Distance, Color.blue);
        Debug.DrawRay(transform.position, -Vector3.right * Distance + Vector3.forward * Distance, Color.blue);
        Debug.DrawRay(transform.position, -Vector3.right * Distance + -Vector3.forward * Distance, Color.blue);
        Debug.DrawRay(transform.position, Vector3.right * Distance + -Vector3.forward * Distance, Color.blue);
        Ray forward = new Ray(transform.position, Vector3.forward);
        Ray backward = new Ray(transform.position, -Vector3.forward);
        Ray right = new Ray(transform.position, Vector3.right);
        Ray left = new Ray(transform.position, -Vector3.right);
        Ray forwardright = new Ray(transform.position, Vector3.right + Vector3.forward);
        Ray forwardleft = new Ray(transform.position, -Vector3.right + Vector3.forward);
        Ray backwardleft = new Ray(transform.position, -Vector3.right + -Vector3.forward);
        Ray backwardright = new Ray(transform.position, Vector3.right + -Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(forward, out hit, Distance))
        {
            
                //Debug.Log("Creature Hit Something Name: " + hit.transform.name);
               // Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);

        }
        if (Physics.Raycast(backward, out hit, Distance))
        {
           
               // Debug.Log("Creature Hit Something Name: " + hit.transform.name);
               // Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);

        }
        if (Physics.Raycast(left, out hit, Distance))
        {
            
               // Debug.Log("Creature Hit Something Name: " + hit.transform.name);
               // Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);


        }
        if (Physics.Raycast(right, out hit, Distance))
        {
           
               // Debug.Log("Creature Hit Something Name: " + hit.transform.name);
               // Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);

        }

        if (Physics.Raycast(forwardright, out hit, Distance))
        {
            
              //  Debug.Log("Creature Hit Something Name: " + hit.transform.name);
               // Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);

        }
        if (Physics.Raycast(forwardleft, out hit, Distance))
        {
           
               // Debug.Log("Creature Hit Something Name: " + hit.transform.name);
                //Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);

        }
        if (Physics.Raycast(backwardleft, out hit, Distance))
        {
            
               // Debug.Log("Creature Hit Something Name: " + hit.transform.name);
                //Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name, hit.distance, hit.transform.position, hit.transform.rotation);

        }
        if (Physics.Raycast(backwardright, out hit, Distance))
        {
            
               // Debug.Log("Creature Hit Something Name: " + hit.transform.name);
               // Debug.Log("Creature Hit Something Distance: " + hit.distance);
            MovementDirection(hit.transform.name,hit.distance, hit.transform.position, hit.transform.rotation);
        }

       

    }
   // float DistanceDamp = 0.1f;
    float rotationalDamp = 0.1f;
    private void MovementDirection(string name ,float distance, Vector3 position, Quaternion rotation )
    {


        if (name == "ObjectWaypointManager")
        {


          
            float step = MovementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, position, step);

            Quaternion currotation  = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp);
            transform.rotation = currotation;
        }

      
      
    }
}
