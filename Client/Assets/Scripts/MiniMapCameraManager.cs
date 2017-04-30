using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraManager : MonoBehaviour
{

    public GameObject Character;
    public float DistanceDamp = 0.1f;
    public float rotationalDamp = 0.1f;
    public float Zadjustment = 0;
    public float Xadjustment = 0;
    public float Yadjustment = 0;

    private void LateUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 Camoffset = new Vector3(Xadjustment, Yadjustment, Zadjustment);
        Vector3 toPos = Character.transform.position + (Character.transform.rotation * Camoffset);
        Vector3 curPos = Vector3.LerpUnclamped(this.transform.position, toPos, DistanceDamp);
        this.transform.position = curPos;
        Quaternion toRot = Quaternion.LookRotation(Character.transform.position - this.transform.position, transform.up);
        Quaternion curRot = Quaternion.Slerp(this.transform.rotation, toRot, rotationalDamp);
        this.transform.rotation = curRot;
    }
}
