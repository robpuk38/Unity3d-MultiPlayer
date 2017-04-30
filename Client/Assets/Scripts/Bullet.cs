using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    
    public float bForce;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

  
    void Start()
    { 
        rb.AddForce(transform.forward * bForce, ForceMode.Impulse);
        Destroy(gameObject, 5.0f);
    }

}
