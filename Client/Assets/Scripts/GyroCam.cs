using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCam : MonoBehaviour {

    private Gyroscope Gyro;
    private GameObject CamConatiner;
    private Quaternion Roatation;
    private bool isGyroReady = false;

    private void Start()
    {
        if(!SystemInfo.supportsGyroscope)
        {
            return;
        }

        CamConatiner = new GameObject("Camera Container");
        CamConatiner.transform.position = this.transform.position;
        this.transform.SetParent(CamConatiner.transform);

        Gyro = Input.gyro;
        Gyro.enabled = true;

        CamConatiner.transform.rotation = Quaternion.Euler(90f, 0, 0);
        Roatation = new Quaternion(0,0,1,0);

        isGyroReady = true;

    }

    private void Update()
    {
        if (isGyroReady == true)
        {
            this.transform.localRotation = Gyro.attitude * Roatation;
        }
    }


}
